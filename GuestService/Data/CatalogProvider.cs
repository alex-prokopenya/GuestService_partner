namespace GuestService.Data
{
    using Sm.System.Database;
    using Sm.System.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public static class CatalogProvider
    {
        private static CatalogFactory factory = new CatalogFactory();

        public static GeoCatalogObject GetGeoPointByAlias(string lang, string alias)
        {
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getGeoObject", "geopoint", new
            {
                language = lang,
                geopointalias = alias
            });
            System.Collections.Generic.List<GeoCatalogObject> list = (
                from row in ds.Tables["geopoint"].Rows.Cast<DataRow>().Take(2)
                select CatalogProvider.factory.GeoCatalogObject(row)).ToList<GeoCatalogObject>();
            GeoCatalogObject result;
            if (list.Count == 0)
            {
                result = null;
            }
            else
            {
                if (list.Count > 1)
                {
                    throw new ExceptionWithCode(201, string.Format("more then one geopoint alias '{0}' found", alias));
                }
                result = list[0];
            }
            return result;
        }

        public static int GetGeoPointIdByAlias(string alias)
        {
            GeoCatalogObject geoPointByAlias = GetGeoPointByAlias("", alias);
            if (geoPointByAlias == null)
            {
                throw new ExceptionWithCode(0xca, string.Format("geopoint alias '{0}' not found", alias));
            }
            return geoPointByAlias.id;
        }

        public static System.Collections.Generic.List<GeoCatalogObject> GetGeoPoints(string lang, string searchName)
        {
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getGeoCatalogObjects", "geopoint", new
            {
                language = lang,
                name = searchName
            });
            return (
                from DataRow row in ds.Tables["geopoint"].Rows
                select CatalogProvider.factory.GeoCatalogObject(row)).ToList<GeoCatalogObject>();
        }

        public static HotelCatalogObject GetHotelDescription(string lang, string hotel)
        {
            if (string.IsNullOrEmpty(hotel))
            {
                throw new System.ArgumentNullException("hotel");
            }
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getHotelDescription", "hotels", new
            {
                language = lang,
                hotelKey = hotel
            });
            return (
                from DataRow m in ds.Tables["hotels"].Rows
                select CatalogProvider.factory.HotelCatalogObject(m)).FirstOrDefault<HotelCatalogObject>();
        }

        public static HotelCatalogObject GetHotelDescription(string lang, int hotelId)
        {
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getHotelDescription", "hotels", new
            {
                language = lang,
                hotelId = hotelId
            });
            return (
                from DataRow m in ds.Tables["hotels"].Rows
                select CatalogProvider.factory.HotelCatalogObject(m)).FirstOrDefault<HotelCatalogObject>();
        }

        private class CatalogFactory
        {
            internal GuestService.Data.GeoCatalogObject GeoCatalogObject(DataRow row)
            {
                return new GuestService.Data.GeoCatalogObject { id = row.ReadInt("point$inc"), name = row.ReadNullableTrimmedString("point$name"), geotype = row.ReadNullableTrimmedString("pointtype$name") };
            }

            internal GuestService.Data.HotelCatalogObject HotelCatalogObject(DataRow row)
            {
                return new GuestService.Data.HotelCatalogObject { id = row.ReadInt("hotel$inc"), alias = row.ReadNullableTrimmedString("hotel$key"), name = row.ReadNullableTrimmedString("hotel$name"), star = this.HotelStar(row), town = this.Town(row), region = this.Region(row), address = row.ReadNullableTrimmedString("hotel$address"), web = row.ReadNullableTrimmedString("hotel$web"), geoposition = (row.IsNull("map$latitude") || row.IsNull("map$longitude")) ? null : new GeoLocation() };
            }

            internal GuestService.Data.HotelStar HotelStar(DataRow row)
            {
                return (row.IsNull("star$inc") ? null : new GuestService.Data.HotelStar());
            }

            internal GuestService.Data.Region Region(DataRow row)
            {
                return (row.IsNull("region$inc") ? null : new GuestService.Data.Region());
            }

            internal GuestService.Data.Town Town(DataRow row)
            {
                return (row.IsNull("town$inc") ? null : new GuestService.Data.Town());
            }
        }
    }
}

