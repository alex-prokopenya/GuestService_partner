﻿using Sm.System.Database;
using Sm.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace GuestService.Data
{
    public static class GuestProvider
    {
        private class GuestFactory
        {
            internal GuestClaim GuestClaim(DataRow row)
            {
                return new GuestClaim
                {
                    claim = row.ReadInt("claim$id"),
                    period = new DatePeriod
                    {
                        begin = new System.DateTime?(row.ReadDateTime("claim$datebeg")),
                        end = new System.DateTime?(row.ReadDateTime("claim$dateend"))
                    },
                    tourname = row.ReadNullableTrimmedString("tour$name")
                };
            }
            internal GuestOrder GuestOrder(DataRow row)
            {
                GuestOrder result = new GuestOrder();
                result.period = new DatePeriod
                {
                    begin = new System.DateTime?(row.ReadDateTime("order$datebeg")),
                    end = new System.DateTime?(row.ReadDateTime("order$dateend"))
                };
                if (!row.IsNull("hotel$id"))
                {
                    result.hotelid = row.ReadNullableInt("hotel$id");
                    result.title = row.ReadNullableTrimmedString("hotel$name");
                    result.description = row.ReadNullableTrimmedString("hotel$room");
                }
                else
                {
                    if (!row.IsNull("service$id"))
                    {
                        result.serviceid = row.ReadNullableInt("service$id");
                        result.title = row.ReadNullableTrimmedString("service$name");
                    }
                }
                return result;
            }
            internal DepartureHotel DepartureHotel(bool useNameField, DataRow row)
            {
                return new DepartureHotel
                {
                    id = row.ReadInt("transfer$hoteldep"),
                    name = row.ReadNullableTrimmedString(useNameField ? "h1$name" : "h1$lname")
                };
            }
            internal DepartureTransfer DepartureTransfer(bool useNameField, DataRow row)
            {
                return new DepartureTransfer
                {
                    id = row.ReadInt("transfer$inc"),
                    ident = row.ReadNullableTrimmedString("transfer$ident"),
                    date = row.ReadUnspecifiedDateTime("transfer$tdate"),
                    pickup = (row.IsNull("transfer$pickdate") || row.IsNull("transfer$picktime")) ? null : new System.DateTime?(row.ReadUnspecifiedDateTime("transfer$pickdate").Date.Add(row.ReadUnspecifiedDateTime("transfer$picktime").TimeOfDay)),
                    flight = (!(row.ReadNullableInt("transfer$freight") > 0)) ? null : new DepartureFlight
                    {
                        id = row.ReadInt("transfer$freight"),
                        name = row.ReadNullableTrimmedString(useNameField ? "f$name" : "f$lname"),
                        source = (!(row.ReadNullableInt("f$srcport") > 0)) ? null : new Airport
                        {
                            id = row.ReadInt("f$srcport"),
                            alias = row.ReadNullableTrimmedString("port1$alias"),
                            name = row.ReadNullableTrimmedString(useNameField ? "port1$name" : "port1$lname"),
                            town = (!(row.ReadNullableInt("st$inc") > 0)) ? null : new Town
                            {
                                id = row.ReadInt("st$inc"),
                                name = row.ReadNullableTrimmedString("st$name")
                            }
                        },
                        target = (!(row.ReadNullableInt("f$trgport") > 0)) ? null : new Airport
                        {
                            id = row.ReadInt("f$trgport"),
                            alias = row.ReadNullableTrimmedString("port2$alias"),
                            name = row.ReadNullableTrimmedString(useNameField ? "port2$name" : "port2$lname"),
                            town = (!(row.ReadNullableInt("tt$inc") > 0)) ? null : new Town
                            {
                                id = row.ReadInt("tt$inc"),
                                name = row.ReadNullableTrimmedString(useNameField ? "tt$name" : "tt$lname")
                            }
                        },
                        takeoff = (row.IsNull("transfer$fdate") || row.IsNull("f$srctime")) ? null : new System.DateTime?(row.ReadUnspecifiedDateTime("transfer$fdate").Date.Add(row.ReadUnspecifiedDateTime("f$srctime").TimeOfDay)),
                        landingtime = row.IsNull("f$trgtime") ? null : new System.TimeSpan?(row.ReadUnspecifiedDateTime("f$trgtime").TimeOfDay)
                    },
                    hotel = (!(row.ReadNullableInt("transfer$hoteldest") > 0)) ? null : new DepartureDestinationHotel
                    {
                        id = row.ReadInt("transfer$hoteldest"),
                        name = row.ReadNullableTrimmedString(useNameField ? "h2$name" : "h2$lname"),
                        town = (!(row.ReadNullableInt("t2$inc") > 0)) ? null : new Town
                        {
                            id = row.ReadInt("t2$inc"),
                            name = row.ReadNullableTrimmedString(useNameField ? "t2$name" : "t2$lname")
                        },
                        region = (!(row.ReadNullableInt("r2$inc") > 0)) ? null : new Region
                        {
                            id = row.ReadInt("r2$inc"),
                            name = row.ReadNullableTrimmedString(useNameField ? "r2$name" : "r2$lname")
                        }
                    },
                    note = row.ReadNullableTrimmedString("transfer$infonote"),
                    language = row.ReadNullableTrimmedString("language$alias"),
                    indiviadual = row.ReadNullableTrimmedString("transfer$indmark") == "IND",
                    servicename = row.ReadNullableTrimmedString(useNameField ? "s$name" : "s$lname"),
                    guide = (!(row.ReadNullableInt("transfer$guide") > 0)) ? null : new DepartureWorker
                    {
                        name = row.ReadNullableTrimmedString(useNameField ? "guide$name" : "guide$lname"),
                        phone = row.ReadNullableTrimmedString("guide$mobile")
                    },
                    guide2 = (!(row.ReadNullableInt("transfer$guide2") > 0)) ? null : new DepartureWorker
                    {
                        name = row.ReadNullableTrimmedString(useNameField ? "guide2$name" : "guide2$lname"),
                        phone = row.ReadNullableTrimmedString("guide2$mobile")
                    }
                };
            }
            internal DepartureMember DepartureMember(bool useNameField, DataRow row)
            {
                return new DepartureMember
                {
                    human = row.ReadNullableTrimmedString("transfer$human"),
                    name = row.ReadNullableTrimmedString(useNameField ? "people$name" : "people$name"),
                    claim = row.ReadNullableInt("people$claim")
                };
            }
            internal ExcursionTransfer ExcursionTransfer(DataRow row)
            {
                return new ExcursionTransfer
                {
                    claim = row.ReadNullableInt("claim"),
                    order = row.ReadNullableInt("order"),
                    exsale = row.ReadNullableInt("exsale"),
                    voucher = row.ReadNullableTrimmedString("voucher"),
                    excursion = row.ReadNullableTrimmedString("excurs$name"),
                    excursiontime = row.ReadNullableTrimmedString("excurs$time"),
                    transferident = row.ReadNullableTrimmedString("transfer$ident"),
                    transfernote = row.ReadNullableTrimmedString("transfer$note"),
                    date = row.ReadUnspecifiedDateTime("date"),
                    pickup = (!row.IsNull("picktime")) ? new System.DateTime?(row.ReadUnspecifiedDateTime("date").Date.Add(row.ReadUnspecifiedTime("picktime"))) : null,
                    pickupplace = row.ReadNullableTrimmedString("pickup$name"),
                    guide = (!row.IsNull("guide$name")) ? new DepartureWorker
                    {
                        name = row.ReadNullableTrimmedString("guide$name"),
                        phone = row.ReadNullableTrimmedString("guide$mobile")
                    } : null,
                    guide2 = (!row.IsNull("guide2$name")) ? new DepartureWorker
                    {
                        name = row.ReadNullableTrimmedString("guide2$name"),
                        phone = row.ReadNullableTrimmedString("guide2$mobile")
                    } : null
                };
            }
        }
        private static GuestProvider.GuestFactory factory = new GuestProvider.GuestFactory();
        public static System.Collections.Generic.List<GuestClaim> GetLinkedClaims(string lang, int userId)
        {
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getLinkedClaims", "claims,hotelorders", new
            {
                language = lang,
                userid = userId
            });
            System.Collections.Generic.List<GuestClaim> result = (
                from DataRow m in ds.Tables["claims"].Rows
                select GuestProvider.factory.GuestClaim(m)).ToList<GuestClaim>();

            foreach (GuestClaim claim in result)
            {
                claim.orders = (
                    from DataRow m in ds.Tables["hotelorders"].Rows
                    where m.ReadInt("order$claim") == claim.claim
                    select GuestProvider.factory.GuestOrder(m)).ToList<GuestOrder>();
            }

            return result;
        }
        public static System.Collections.Generic.List<GuestClaim> FindGuestClaims(string lang, int userId, string name, int? claim, string passport)
        {
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_findGuestClaims", "claims,hotelorders", new
            {
                language = lang,
                userid = userId,
                guestname = name,
                claim = claim,
                passportnumber = passport
            });
            System.Collections.Generic.List<GuestClaim> result = (
                from DataRow m in ds.Tables["claims"].Rows
                select GuestProvider.factory.GuestClaim(m)).ToList<GuestClaim>();
            foreach (GuestClaim guestClaim in result)
            {
                guestClaim.orders = (
                    from DataRow m in ds.Tables["hotelorders"].Rows
                    where m.ReadInt("order$claim") == guestClaim.claim
                    select GuestProvider.factory.GuestOrder(m)).ToList<GuestOrder>();
            }
            return result;
        }
        public static void LinkGuestClaim(int userId, string name, int claim)
        {
            System.Collections.Generic.List<GuestClaim> claims = GuestProvider.FindGuestClaims("", userId, name, new int?(claim), null);
            if (claims != null && claims.FirstOrDefault((GuestClaim m) => m.claim == claim) != null)
            {
                DatabaseOperationProvider.ExecuteProcedure("up_guest_linkGuestClaim", new
                {
                    userid = userId,
                    claim = claim
                });
                return;
            }
            throw new ExceptionWithCode(203, string.Format("claim {0} not found", claim));
        }
        public static void LinkGuestClaim(int userId, int claim)
        {
            DatabaseOperationProvider.ExecuteProcedure("up_guest_linkGuestClaim", new
            {
                userid = userId,
                claim = claim
            });
        }
        public static void UnlinkGuestClaim(int userId, int claim)
        {
            DatabaseOperationProvider.ExecuteProcedure("up_guest_unlinkGuestClaim", new
            {
                userid = userId,
                claim = claim
            });
        }
        public static System.Collections.Generic.List<GuestClaim> GetActiveClaims(string lang, int userId, System.DateTime date)
        {
            return GuestProvider.GetActiveClaims(lang, userId, date, date);
        }
        public static System.Collections.Generic.List<GuestClaim> GetActiveClaims(string lang, int userId, System.DateTime firstDate, System.DateTime lastDate)
        {
            System.Collections.Generic.List<GuestClaim> claims = GuestProvider.GetLinkedClaims(lang, userId);
            System.Collections.Generic.List<GuestClaim> result2;
            if (claims != null)
            {
                System.Collections.Generic.List<GuestClaim> result = (
                    from m in claims
                    where m.period != null && firstDate <= m.period.end && lastDate >= m.period.begin
                    select m).ToList<GuestClaim>();
                if (result != null && result.Count > 0)
                {
                    result2 = result;
                    return result2;
                }
            }
            result2 = null;
            return result2;
        }
        public static System.Collections.Generic.List<GuestOrder> GetActiveHotelOrders(string lang, int userId, System.DateTime date)
        {
            return GuestProvider.GetActiveHotelOrders(lang, userId, date, date);
        }
        public static System.Collections.Generic.List<GuestOrder> GetActiveHotelOrders(string lang, int userId, System.DateTime firstDate, System.DateTime lastDate)
        {
            System.Collections.Generic.List<GuestOrder> result = new System.Collections.Generic.List<GuestOrder>();
            System.Collections.Generic.List<GuestClaim> claims = GuestProvider.GetLinkedClaims(lang, userId);
            return GuestProvider.GetActiveHotelOrders(claims, firstDate, lastDate);
        }
        public static System.Collections.Generic.List<GuestOrder> GetActiveHotelOrders(System.Collections.Generic.List<GuestClaim> claims, System.DateTime firstDate, System.DateTime lastDate)
        {
            System.Collections.Generic.List<GuestOrder> result = new System.Collections.Generic.List<GuestOrder>();
            if (claims != null)
            {
                foreach (GuestClaim claim in claims)
                {
                    if (claim.orders != null)
                    {
                        foreach (GuestOrder order in claim.orders)
                        {
                            if (order.hotelid.HasValue && firstDate <= order.period.end && lastDate >= order.period.begin)
                            {
                                result.Add(order);
                            }
                        }
                    }
                }
            }
            return result;
        }
        public static System.Collections.Generic.List<DepartureHotel> GetDepartureInfo(string lang, System.DateTime dateFrom, System.DateTime dateTill, int? hotel, int? claim)
        {
            System.Collections.Generic.List<DepartureHotel> result = new System.Collections.Generic.List<DepartureHotel>();
            if (!hotel.HasValue && !claim.HasValue)
            {
                throw new System.ArgumentNullException("hotel || claim");
            }
            bool useNameField = lang == null || string.Compare(lang, "en", true) != 0;
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_TransferReports", "transfers,people", new
            {
                reportType = 1,
                trnsType = "2,3",
                dateFrom = dateFrom,
                dateTill = dateTill,
                freightTime = new System.DateTime(1900, 1, 1, 6, 0, 0),
                hotel = hotel.HasValue ? hotel.Value : 0,
                claim = claim.HasValue ? claim.Value : 0
            });
            System.Collections.Generic.IEnumerable<DataRow> dsTransfers = ds.Tables["transfers"].Rows.Cast<DataRow>();
            System.Collections.Generic.IEnumerable<DataRow> dsPeople = ds.Tables["people"].Rows.Cast<DataRow>();
            foreach (int h in (
                from row in dsTransfers
                select row.ReadInt("transfer$hoteldep")).Distinct<int>())
            {
                DataRow hotelRow = dsTransfers.FirstOrDefault((DataRow row) => row.ReadInt("transfer$hoteldep") == h);
                if (hotelRow != null)
                {
                    DepartureHotel dhotel = GuestProvider.factory.DepartureHotel(useNameField, hotelRow);
                    dhotel.transfers = (
                        from row in dsTransfers
                        where row.ReadInt("transfer$hoteldep") == dhotel.id
                        select GuestProvider.factory.DepartureTransfer(useNameField, row)).ToList<DepartureTransfer>();
                    foreach (DepartureTransfer transfer in dhotel.transfers)
                    {
                        transfer.people = (
                            from row in dsPeople
                            where row.ReadNullableInt("transfer$inc") == transfer.id
                            orderby row.ReadNullableInt("ident")
                            select GuestProvider.factory.DepartureMember(useNameField, row)).ToList<DepartureMember>();
                    }
                    result.Add(dhotel);
                }
            }
            return result;
        }
        public static System.Collections.Generic.List<ExcursionTransfer> GetExcursionTransferByClaim(string lang, int claim)
        {
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getExcursionTransferByClaim", "transfer", new
            {
                language = lang,
                claimid = claim
            });
            return (
                from DataRow row in ds.Tables["transfer"].Rows
                select GuestProvider.factory.ExcursionTransfer(row)).ToList<ExcursionTransfer>();
        }
    }
}
