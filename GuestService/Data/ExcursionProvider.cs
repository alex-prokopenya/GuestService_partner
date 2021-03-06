﻿using Sm.System.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
namespace GuestService.Data
{
    public static class ExcursionProvider
    {
        private class ExcursionFactory
        {
            internal CategoryWithGroup CategoryWithGroup(DataRow row)
            {
                return new CategoryWithGroup
                {
                    id = row.ReadInt("category$inc"),
                    name = row.ReadNullableTrimmedString("category$name"),
                    description = row.ReadNullableTrimmedString("category$description"),
                    group = row.IsNull("categorygroup$inc") ? null : new CategoryGroup
                    {
                        id = row.ReadInt("categorygroup$inc"),
                        name = row.ReadNullableTrimmedString("categorygroup$name")
                    }
                };
            }
            internal CategoryGroupWithCategories CategoryGroupWithCategories(DataRow row)
            {
                return new CategoryGroupWithCategories
                {
                    group = row.IsNull("categorygroup$inc") ? null : new CategoryGroup
                    {
                        id = row.ReadInt("categorygroup$inc"),
                        name = row.ReadNullableTrimmedString("categorygroup$name")
                    }
                };
            }
            internal Category Category(DataRow row)
            {
                return new Category
                {
                    id = row.ReadInt("category$inc"),
                    name = row.ReadNullableTrimmedString("category$name"),
                    description = row.ReadNullableTrimmedString("category$description")
                };
            }
            internal SearchGeography SearchGeography(DataRow row)
            {
                SearchGeography result = new SearchGeography
                {
                    name = row.ReadNullableTrimmedString("point$name"),
                    geotype = row.ReadNullableTrimmedString("pointtype$name")
                };
                XElement destinationXml = row.ReadXml("point$destinationpoints");
                if (destinationXml != null)
                {
                    result.destinations = (
                        from d in destinationXml.Descendants()
                        select (int)d.Attribute("inc")).ToArray<int>();
                }
                return result;
            }
            internal CatalogExcursion CatalogExcursion(DataRow row)
            {
                CatalogExcursion result = new CatalogExcursion
                {
                    id = row.ReadInt("excurs$inc"),
                    name = row.ReadNullableTrimmedString("excurs$name"),
                    url = row.ReadNullableTrimmedString("excurs$url"),
                    
                    excursionPartner = row.IsNull("expartner$inc") ? null : new Partner
                    {
                        id = row.ReadInt("expartner$inc"),
                        name = row.ReadNullableTrimmedString("expartner$name")
                    },
                    text = row.ReadNullableTrimmedString("excurs$route")
                };
                System.DateTime? durationDate = row.ReadNullableUnspecifiedDateTime("excurs$lasting");
                if (durationDate.HasValue)
                {
                    result.duration = new System.TimeSpan?(durationDate.Value - new System.DateTime(1900, 1, 1));
                }
                XElement destinationXml = row.ReadXml("excurs$destinationpoints");
                if (destinationXml != null)
                {
                    result.destinations = (
                        from d in destinationXml.Descendants()
                        select new GeoArea
                        {
                            id = (int)d.Attribute("inc"),
                            alias = (string)d.Attribute("alias"),
                            name = (string)d.Attribute("name"),
                            location = (d.Attribute("latitude") == null || d.Attribute("longitude") == null) ? null : new GeoLocation
                            {
                                latitude = (decimal)d.Attribute("latitude"),
                                longitude = (decimal)d.Attribute("longitude")
                            }
                        }).ToList<GeoArea>();
                }
                XElement departureXml = row.ReadXml("excurs$departurepoints");
                if (departureXml != null)
                {
                    result.departures = (
                        from d in departureXml.Descendants()
                        select new GeoArea
                        {
                            id = (int)d.Attribute("inc"),
                            alias = (string)d.Attribute("alias"),
                            name = (string)d.Attribute("name"),
                            location = (d.Attribute("latitude") == null || d.Attribute("longitude") == null) ? null : new GeoLocation
                            {
                                latitude = (decimal)d.Attribute("latitude"),
                                longitude = (decimal)d.Attribute("longitude")
                            }
                        }).ToList<GeoArea>();
                }
                XElement languagesXml = row.ReadXml("excurs$languages");
                if (languagesXml != null)
                {
                    result.languages = (
                        from d in languagesXml.Descendants()
                        select new Language
                        {
                            id = (int)d.Attribute("inc"),
                            alias = (string)d.Attribute("alias"),
                            name = (string)d.Attribute("name")
                        }).ToList<Language>();
                }
                XElement categoriesXml = row.ReadXml("excurs$categories");
                if (categoriesXml != null)
                {
                    result.categories = (
                        from d in categoriesXml.Descendants()
                        select new ExcursionCategory
                        {
                            id = (int)d.Attribute("inc"),
                            name = (string)d.Attribute("name"),
                            categorygroup = (!((int?)d.Attribute("groupinc")).HasValue) ? null : new CategoryGroup
                            {
                                id = (int)d.Attribute("groupinc"),
                                name = (string)d.Attribute("groupname")
                            },
                            sort = (int?)d.Attribute("sort")
                        }).ToList<ExcursionCategory>();
                }
                return result;
            }
            internal PriceSummary CatalogExcursionMinPrice(DataRow row)
            {
                PriceSummary result;
                if (row.IsNull("minprice$forservice") || row.IsNull("minprice$price"))
                {
                    result = null;
                }
                else
                {
                    result = new PriceSummary
                    {
                        priceType = (row.ReadInt("minprice$forservice") > 0) ? PriceSummary.PriceType.perService : PriceSummary.PriceType.perPerson,
                        price = row.ReadDecimal("minprice$price"),
                        currency = row.ReadNullableTrimmedString("minprice$currency")
                    };
                }
                return result;
            }
            internal ExcursionPrice ExcursionPrice(DataRow row, System.DateTime date)
            {
                ExcursionPrice result = new ExcursionPrice
                {
                    id = row.ReadInt("excurs$inc"),
                    date = date,
                    language = row.IsNull("lang$inc") ? null : new Language
                    {
                        id = row.ReadInt("lang$inc"),
                        alias = row.ReadNullableTrimmedString("lang$alias"),
                        name = row.ReadNullableTrimmedString("lang$name")
                    },
                    group = row.IsNull("group$inc") ? null : new ExcursionGroup
                    {
                        id = row.ReadInt("group$inc"),
                        name = row.ReadNullableTrimmedString("group$name")
                    },
                    time = row.IsNull("time$inc") ? null : new ExcursionTime
                    {
                        id = row.ReadInt("time$inc"),
                        name = row.ReadNullableTrimmedString("time$name")
                    },
                    issaleclosed = !row.IsNull("excurs$closedsale") && row.ReadInt("excurs$closedsale") > 0,
                    closesaletime = row.ReadNullableDateTime("excurs$closesale"),
                    isstopsale = !row.IsNull("excurs$stopsale") && row.ReadInt("excurs$stopsale") > 0,
                    freeseats = (row.ReadNullableInt("seats$free") > 0) ? row.ReadNullableInt("seats$free") : null
                };
                XElement departureXml = row.ReadXml("excurs$departurepoints");
                if (departureXml != null)
                {
                    result.departures = (
                        from d in departureXml.Descendants()
                        select new GeoArea
                        {
                            id = (int)d.Attribute("inc"),
                            alias = (string)d.Attribute("alias"),
                            name = (string)d.Attribute("name"),
                            location = (d.Attribute("latitude") == null || d.Attribute("longitude") == null) ? null : new GeoLocation
                            {
                                latitude = (decimal)d.Attribute("latitude"),
                                longitude = (decimal)d.Attribute("longitude")
                            }
                        }).ToList<GeoArea>();
                }
                if (!result.isstopsale && !result.issaleclosed)
                {
                    result.price = (row.IsNull("price$isserviceprice") ? null : new PriceDetails
                    {
                        priceType = (row.ReadInt("price$isserviceprice") > 0) ? PriceDetails.PriceType.perService : PriceDetails.PriceType.perPerson,
                        service = row.ReadDecimal("price$service"),
                        adult = row.ReadDecimal("price$adult"),
                        child = row.ReadDecimal("price$child"),
                        infant = row.ReadDecimal("price$infant"),
                        currency = row.ReadNullableTrimmedString("price$alias"),
                        minGroup = row.ReadInt("price$groupfrom", 1),
                        maxGroup = row.ReadInt("price$grouptill", 999)
                    });
                }
                XElement pickuppointsXml = row.ReadXml("excurs$pickuppoints");
                if (pickuppointsXml != null)
                {
                    result.pickuppoints = (
                        from d in pickuppointsXml.Descendants()
                        select new ExcursionPickup
                        {
                            id = (int)d.Attribute("inc"),
                            name = (string)d.Attribute("name"),
                            location = (d.Attribute("latitude") == null || d.Attribute("longitude") == null) ? null : new GeoLocation
                            {
                                latitude = (decimal)d.Attribute("latitude"),
                                longitude = (decimal)d.Attribute("longitude")
                            },
                            note = (string)d.Attribute("note"),
                            pickuptime = (d.Attribute("time") == null) ? null : ((System.DateTime?)d.Attribute("time"))
                        }).ToList<ExcursionPickup>();
                }
                return result;
            }
            internal ExcursionPicture ExcursionPicture(DataRow row)
            {
                return new ExcursionPicture
                {
                    ex = row.ReadInt("excurs"),
                    index = row.ReadInt("sorder"),
                    description = (!row.IsNull("descriptionlang")) ? row.ReadNullableTrimmedString("descriptionlang") : row.ReadNullableTrimmedString("description")
                };
            }
            internal ExcursionDescriptionSection ExcursionDescriptionSection(DataRow row)
            {
                return new ExcursionDescriptionSection
                {
                    title = (!row.IsNull("namelang")) ? row.ReadNullableTrimmedString("namelang") : row.ReadNullableTrimmedString("name")
                };
            }
            internal ExcursionDate ExcursionDate(DataRow row)
            {
                return new ExcursionDate
                {
                    date = row.ReadDateTime("date"),
                    isprice = row.ReadBoolean("exprice"),
                    isstopsale = row.ReadBoolean("exstopsale"),
                    allclosed = row.ReadBoolean("allclosed")
                };
            }
            internal GeoArea StatePoint(DataRow row)
            {
                return new GeoArea
                {
                    id = row.ReadInt("inc"),
                    name = row.ReadNullableTrimmedString("name"),
                    alias = row.ReadNullableTrimmedString("alias"),
                    location = (row.IsNull("latitude") || row.IsNull("longitude")) ? null : new GeoLocation
                    {
                        latitude = row.ReadDecimal("latitude"),
                        longitude = row.ReadDecimal("longitude")
                    }
                };
            }
            internal ExcursionPickupHotel ExcursionPickupHotel(DataRow row)
            {
                return new ExcursionPickupHotel
                {
                    id = row.ReadInt("hotel$inc"),
                    name = row.ReadNullableTrimmedString("hotel$name"),
                    pickuptime = row.ReadNullableDateTime("picktime"),
                    star = row.ReadNullableTrimmedString("star$name"),
                    strstar = row.ReadNullableInt("star$stdstar"),
                    town = row.ReadNullableTrimmedString("town$name")
                };
            }
        }
        private class EDSNode
        {
            public int id
            {
                get;
                set;
            }
            public int? parentid
            {
                get;
                set;
            }
            public ExcursionDescriptionSection section
            {
                get;
                set;
            }
            public static bool IsNodeEmpty(System.Collections.Generic.List<ExcursionProvider.EDSNode> list, ExcursionProvider.EDSNode node)
            {
                bool result;
                if (node.section.paragraphs != null)
                {
                    result = false;
                }
                else
                {
                    foreach (ExcursionProvider.EDSNode child in
                        from row in list
                        where row.parentid == node.id
                        select row)
                    {
                        if (!ExcursionProvider.EDSNode.IsNodeEmpty(list, child))
                        {
                            result = false;
                            return result;
                        }
                    }
                    result = true;
                }
                return result;
            }
        }
        private class CategoryGroupWithCategoriesComparer : System.Collections.Generic.IEqualityComparer<CategoryGroupWithCategories>
        {
            public bool Equals(CategoryGroupWithCategories x, CategoryGroupWithCategories y)
            {
                return object.ReferenceEquals(x, y) || (!object.ReferenceEquals(x, null) && !object.ReferenceEquals(y, null) && ((x.group != null) ? x.group.id : 0) == ((y.group != null) ? y.group.id : 0));
            }
            public int GetHashCode(CategoryGroupWithCategories group)
            {
                int result;
                if (object.ReferenceEquals(group, null))
                {
                    result = 0;
                }
                else
                {
                    result = ((group.group == null) ? 0 : group.group.id.GetHashCode());
                }
                return result;
            }
        }
        private class CatalogFilterItemsCounterBuilder<T>
        {
            public class CatalogFilterCounterItem<M>
            {
                public M item
                {
                    get;
                    set;
                }
                public int count
                {
                    get;
                    set;
                }
            }
            private System.Collections.Generic.Dictionary<int, ExcursionProvider.CatalogFilterItemsCounterBuilder<T>.CatalogFilterCounterItem<T>> list;
            public CatalogFilterItemsCounterBuilder()
            {
                this.list = new System.Collections.Generic.Dictionary<int, ExcursionProvider.CatalogFilterItemsCounterBuilder<T>.CatalogFilterCounterItem<T>>();
            }
            public void Add(int id, T item)
            {
                ExcursionProvider.CatalogFilterItemsCounterBuilder<T>.CatalogFilterCounterItem<T> counterItem = null;
                if (this.list.TryGetValue(id, out counterItem))
                {
                    counterItem.count++;
                }
                else
                {
                    this.list.Add(id, new ExcursionProvider.CatalogFilterItemsCounterBuilder<T>.CatalogFilterCounterItem<T>
                    {
                        count = 1,
                        item = item
                    });
                }
            }
            public System.Collections.Generic.List<ExcursionProvider.CatalogFilterItemsCounterBuilder<T>.CatalogFilterCounterItem<T>> ToList()
            {
                return (
                    from m in this.list
                    select m.Value).ToList<ExcursionProvider.CatalogFilterItemsCounterBuilder<T>.CatalogFilterCounterItem<T>>();
            }
        }
        public class LoadStatesResult
        {
            public System.Collections.Generic.Dictionary<int, int> Links
            {
                get;
                private set;
            }
            public System.Collections.Generic.List<GeoArea> States
            {
                get;
                set;
            }
            public LoadStatesResult()
            {
                this.Links = new System.Collections.Generic.Dictionary<int, int>();
            }
        }
        public enum ExcursionSorting
        {
            name,
            price,
            pricedname
        }
        private static ExcursionProvider.ExcursionFactory factory = new ExcursionProvider.ExcursionFactory();
        public static SearchExcursionResult SearchExcursionObjects(string lang, int? startPoint, string searchText, int limit)
        {
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getExcursionObjects", "geopoints", new
            {
                language = lang,
                startpoint = startPoint,
                searchtext = searchText,
                topcount = limit
            });
            SearchExcursionResult result = new SearchExcursionResult();
            result.geography = (
                from DataRow row in ds.Tables["geopoints"].Rows
                select ExcursionProvider.factory.SearchGeography(row)).ToList<SearchGeography>();
            return result;
        }
        public static System.Collections.Generic.List<CategoryWithGroup> GetCategories(string lang, int? startPoint)
        {
            System.Collections.Generic.List<CategoryWithGroup> result = new System.Collections.Generic.List<CategoryWithGroup>();
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getExcursionCategories", "categories", new
            {
                language = lang,
                startpoint = startPoint
            });
            return (
                from DataRow row in ds.Tables["categories"].Rows
                select ExcursionProvider.factory.CategoryWithGroup(row)).ToList<CategoryWithGroup>();
        }
        public static System.Collections.Generic.List<CategoryGroupWithCategories> GetCategoriesByGroup(string lang, int? startPoint)
        {
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getExcursionCategories", "categories", new
            {
                language = lang,
                startpoint = startPoint
            });
            System.Collections.Generic.List<CategoryGroupWithCategories> result = (
                from DataRow row in ds.Tables["categories"].Rows
                select ExcursionProvider.factory.CategoryGroupWithCategories(row)).Distinct(new ExcursionProvider.CategoryGroupWithCategoriesComparer()).ToList<CategoryGroupWithCategories>();
            CategoryGroupWithCategories emptyGroup = result.FirstOrDefault((CategoryGroupWithCategories m) => m.group == null);
            if (emptyGroup != null)
            {
                result.Remove(emptyGroup);
                result.Insert(0, emptyGroup);
            }
            foreach (CategoryGroupWithCategories group in result)
            {
                if (group.group == null)
                {
                    group.categories = (
                        from DataRow row in ds.Tables["categories"].Rows
                        where row.IsNull("categorygroup$inc")
                        select ExcursionProvider.factory.Category(row)).ToList<Category>();
                }
                else
                {
                    group.categories = (
                        from row in ds.Tables["categories"].Rows.Cast<DataRow>().Where(delegate (DataRow row)
                        {
                            int? num = row.ReadNullableInt("categorygroup$inc");
                            return num == ((@group.@group != null) ? new int?(@group.@group.id) : null);
                        })
                        select ExcursionProvider.factory.Category(row)).ToList<Category>();
                }
            }
            return result;
        }
        public static Image GetCategoryImage(int id)
        {
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getCategoryImage", "image", new
            {
                id
            });
            DataRow row = ds.Tables["image"].Rows.Cast<DataRow>().FirstOrDefault<DataRow>();
            Image result;
            if (row != null && !row.IsNull("img$picture"))
            {
                result = Image.FromStream(new System.IO.MemoryStream((byte[])row["img$picture"]));
            }
            else
            {
                result = null;
            }
            return result;
        }
        public static System.Collections.Generic.List<CatalogExcursionMinPrice> FindExcursions(string lang, int partner, System.DateTime? startDate, System.DateTime? endDate, int? topLimit, int? startPoint, string searchText, int[] categories, int[] departures, int[] destinations, int[] languages, System.TimeSpan? minDuration, System.TimeSpan? maxDuration, ExcursionProvider.ExcursionSorting? sorting, bool withoutPrice)
        {
            try
            {
                System.DateTime _startDate = startDate.HasValue ? startDate.Value : System.DateTime.Now.Date;
                System.DateTime _endDate = endDate.HasValue ? endDate.Value : _startDate.AddMonths(6);
                XName arg_261_0 = "excursionFilters";
                object[] array = new object[8];
                array[0] = ((!topLimit.HasValue) ? null : new XAttribute("topLimit", topLimit.Value));
                array[1] = ((searchText == null) ? null : new XElement("name", searchText));


                XElement categoriesEl;
                if (categories != null)
                {
                    categoriesEl = new XElement("categories",
                        from c in categories
                        select new XElement("category", c));
                }
                else
                    categoriesEl = null;

                array[2] = categoriesEl;


                XElement departuresEl;
                List<int> allowedIds = new List<int>();
                if (departures != null)
                {
                    //делаем фильтр экскурсий по id региона
                    DataSet set = DatabaseOperationProvider.Query("select inc from excurs where region = " + departures[0], "regions", new { });

                    foreach (DataRow row in set.Tables["regions"].Rows)
                        allowedIds.Add(row.ReadInt("inc"));
                }

                departuresEl = null;

                array[3] = departuresEl;

                XElement destEl;

                if (destinations != null)
                {
                    destEl = new XElement("destinationpoints",
                        from d in destinations
                        select new XElement("destinationpoint", d));
                }
                else
                    destEl = null;

                array[4] = destEl;

                XElement langEl;
                if (languages != null)
                {
                    langEl = new XElement("languages",
                        from l in languages
                        select new XElement("language", l));
                }
                else
                    langEl = null;

                array[5] = langEl;

                array[6] = new XElement("duration", new object[]
                {
                (!minDuration.HasValue) ? null : new XAttribute("minDuration", new System.DateTime(1900, 1, 1).Add(minDuration.Value)),
                (!maxDuration.HasValue) ? null : new XAttribute("maxDuration", new System.DateTime(1900, 1, 1).Add(maxDuration.Value))
                });
                array[7] = ((!sorting.HasValue) ? null : new XElement("sorting", sorting.ToString()));
                XElement xml = new XElement(arg_261_0, array);
                DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_findExcursions", "excursions", new
                {
                    language = lang,
                    partner = partner,
                    startpoint = startPoint,
                    startdate = _startDate,
                    enddate = _endDate,
                    filters = xml,
                    withpriceonly = !withoutPrice
                });

                if (allowedIds.Count > 0)
                    return (
                         from DataRow row in ds.Tables["excursions"].Rows
                         where allowedIds.Contains(row.ReadInt("excurs$inc")) //> 0 
                         select new CatalogExcursionMinPrice
                         {
                             excursion = ExcursionProvider.factory.CatalogExcursion(row),
                             minPrice = ExcursionProvider.factory.CatalogExcursionMinPrice(row)
                         }).ToList<CatalogExcursionMinPrice>();
                else
                    return (
                        from DataRow row in ds.Tables["excursions"].Rows
                        select new CatalogExcursionMinPrice
                        {
                            excursion = ExcursionProvider.factory.CatalogExcursion(row),
                            minPrice = ExcursionProvider.factory.CatalogExcursionMinPrice(row)
                        }).ToList<CatalogExcursionMinPrice>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public static Image GetCatalogImage(int id, int index)
        {
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getCatalogImage", "image", new
            {
                id,
                index
            });
            DataRow row = ds.Tables["image"].Rows.Cast<DataRow>().FirstOrDefault<DataRow>();
            Image result;
            if (row != null && !row.IsNull("img$picture"))
            {
                result = Image.FromStream(new System.IO.MemoryStream((byte[])row["img$picture"]));
            }
            else
            {
                result = null;
            }
            return result;
        }
        public static System.Collections.Generic.List<ExcursionPrice> GetPrice(string lang, int partner, int excursionId, System.DateTime date, int? startPoint)
        {
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getExcursionPrice", "prices", new
            {
                language = lang,
                partner = partner,
                startpoint = startPoint,
                excursionId = excursionId,
                date = date.Date
            });
            return (
                from DataRow row in ds.Tables["prices"].Rows
                select ExcursionProvider.factory.ExcursionPrice(row, date)).ToList<ExcursionPrice>();
        }
        public static System.Collections.Generic.List<ExcursionDate> GetDates(int partner, int excursionId, System.DateTime dateFrom, System.DateTime dateTill)
        {
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_GetExcursionDates", "dates", new
            {
                partner = partner,
                excurs = excursionId,
                dateBeg = dateFrom,
                dateEnd = dateTill
            });
            return (
                from DataRow row in ds.Tables["dates"].Rows
                select ExcursionProvider.factory.ExcursionDate(row)).ToList<ExcursionDate>();
        }
        public static System.Collections.Generic.List<ExcursionDescription> GetDescription(string lang, int[] excursions)
        {
            if (excursions == null)
            {
                throw new System.ArgumentNullException("excursions");
            }
            XElement xml = new XElement("excursions",
                from e in excursions
                select new XElement("excursion", new XAttribute("id", e)));
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getExcursionDescription", "excursions,pictures,dtree,description", new
            {
                language = lang,
                excursions = xml,
                loaddescription = true
            });
            System.Collections.Generic.IEnumerable<ExcursionPicture> pictures =
                from DataRow row in ds.Tables["pictures"].Rows
                select ExcursionProvider.factory.ExcursionPicture(row);
            return ds.Tables["excursions"].Rows.Cast<DataRow>().Select(delegate (DataRow row)
            {
                ExcursionDescription description = new ExcursionDescription();
                description.excursion = ExcursionProvider.factory.CatalogExcursion(row);
                description.pictures = (
                    from p in pictures
                    where p.ex == description.excursion.id
                    select p).ToList<ExcursionPicture>();
                System.Collections.Generic.List<ExcursionProvider.EDSNode> tree = (
                    from DataRow r in ds.Tables["dtree"].Rows
                    select new ExcursionProvider.EDSNode
                    {
                        id = r.ReadInt("inc"),
                        parentid = r.ReadNullableInt("parent_inc"),
                        section = ExcursionProvider.factory.ExcursionDescriptionSection(r)
                    }).ToList<ExcursionProvider.EDSNode>();
                ExcursionProvider.EDSNode ctree = null;
                foreach (DataRow paragraphRow in
                    from DataRow r in ds.Tables["description"].Rows
                    where r.ReadInt("excurs") == description.excursion.id
                    select r)
                {
                    int treeId = paragraphRow.ReadInt("tree");
                    if (ctree == null || ctree.id != treeId)
                    {
                        ctree = tree.FirstOrDefault((ExcursionProvider.EDSNode r) => r.id == treeId);
                    }
                    if (ctree != null)
                    {
                        if (ctree.section.paragraphs == null)
                        {
                            ctree.section.paragraphs = new System.Collections.Generic.List<string>();
                        }
                        ctree.section.paragraphs.Add(paragraphRow.ReadNullableTrimmedString((!paragraphRow.IsNull("descriptionlang")) ? "descriptionlang" : "description"));
                    }
                }
                description.description = new System.Collections.Generic.List<ExcursionDescriptionSection>();
                foreach (ExcursionProvider.EDSNode tnode in tree)
                {
                    if (!ExcursionProvider.EDSNode.IsNodeEmpty(tree, tnode))
                    {
                        if (!tnode.parentid.HasValue)
                        {
                            description.description.Add(tnode.section);
                        }
                        else
                        {
                            ExcursionProvider.EDSNode pnode = tree.FirstOrDefault((ExcursionProvider.EDSNode r) => r.id == tnode.parentid.Value);
                            if (pnode != null)
                            {
                                if (pnode.section.sections == null)
                                {
                                    pnode.section.sections = new System.Collections.Generic.List<ExcursionDescriptionSection>();
                                }
                                pnode.section.sections.Add(tnode.section);
                            }
                        }
                    }
                }
                return description;
            }).ToList<ExcursionDescription>();
        }
        public static System.Collections.Generic.List<CatalogFilterCategoryGroup> BuildFilterCategories(System.Collections.Generic.List<CatalogExcursionMinPrice> catalog, int[] marks)
        {
            ExcursionProvider.CatalogFilterItemsCounterBuilder<ExcursionCategory> builder = new ExcursionProvider.CatalogFilterItemsCounterBuilder<ExcursionCategory>();
            if (catalog != null)
            {
                foreach (CatalogExcursionMinPrice item in catalog)
                {
                    if (item.excursion != null && item.excursion.categories != null)
                    {
                        foreach (ExcursionCategory c in item.excursion.categories)
                        {
                            builder.Add(c.id, c);
                        }
                    }
                }
            }
            return (
                from m in (
                    from m in builder.ToList()
                    group m by (m.item.categorygroup != null) ? m.item.categorygroup.name : null).Select(delegate (IGrouping<string, ExcursionProvider.CatalogFilterItemsCounterBuilder<ExcursionCategory>.CatalogFilterCounterItem<ExcursionCategory>> m)
                    {
                        CatalogFilterCategoryGroup catalogFilterCategoryGroup = new CatalogFilterCategoryGroup();
                        catalogFilterCategoryGroup.name = m.Key;
                        catalogFilterCategoryGroup.items = (
                            from n in m
                            orderby n.item.sort
                            select new CatalogFilterItem
                            {
                                id = n.item.id,
                                name = n.item.name,
                                count = n.count
                            }).ToList<CatalogFilterItem>();
                        return catalogFilterCategoryGroup;
                    })
                orderby (m.name == null) ? 0 : 1, m.name
                select m).ToList<CatalogFilterCategoryGroup>();
        }
        public static System.Collections.Generic.List<CatalogFilterLocationItem> BuildFilterDepartures(System.Collections.Generic.List<CatalogExcursionMinPrice> catalog, int[] marks)
        {
            ExcursionProvider.CatalogFilterItemsCounterBuilder<GeoArea> builder = new ExcursionProvider.CatalogFilterItemsCounterBuilder<GeoArea>();
            if (catalog != null)
            {
                foreach (CatalogExcursionMinPrice item in catalog)
                {
                    if (item.excursion != null && item.excursion.departures != null)
                    {
                        foreach (GeoArea d in item.excursion.departures)
                        {
                            builder.Add(d.id, d);
                        }
                    }
                }
            }
            return (
                from m in builder.ToList()
                select new CatalogFilterLocationItem
                {
                    id = m.item.id,
                    name = m.item.name,
                    location = m.item.location,
                    count = m.count
                } into m
                orderby m.name
                select m).ToList<CatalogFilterLocationItem>();
        }

        public static System.Collections.Generic.List<GeoArea> BuildRegionList(System.Collections.Generic.List<CatalogExcursionMinPrice> catalog)
        {
            List<int> keys = new List<int>();
            if (catalog != null)
            {
                foreach (CatalogExcursionMinPrice item in catalog)
                {
                    if (item.excursion != null )
                        keys.Add(item.excursion.id);
                }
            }

            var excKeys = String.Join(",", keys);

            var geoData = new List<GeoArea>();
            try
            {
                DataSet ds = DatabaseOperationProvider.Query("select name, lname, inc from region where inc in (select region from excurs where inc in ("+excKeys+"))", "regions", new
                {
                });

                foreach (DataRow row in ds.Tables["regions"].Rows)
                {
                    geoData.Add(new GeoArea()
                    {
                        name = row.ReadNullableTrimmedString("lname"),

                        alias = row.ReadNullableTrimmedString("name"),

                        id = row.ReadInt("inc")
                    });
                }

                return geoData;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public static System.Collections.Generic.List<GeoArea> BuildDepartureList(System.Collections.Generic.List<CatalogExcursionMinPrice> catalog)
        {
            System.Collections.Generic.Dictionary<int, GeoArea> departures = new System.Collections.Generic.Dictionary<int, GeoArea>();
            if (catalog != null)
            {
                foreach (CatalogExcursionMinPrice item in catalog)
                {
                    if (item.excursion != null && item.excursion.departures != null)
                    {
                        foreach (GeoArea d in item.excursion.departures)
                        {
                            GeoArea area;
                            if (!departures.TryGetValue(d.id, out area))
                            {
                                departures[d.id] = d;
                            }
                        }
                    }
                }
            }
            return (
                from m in departures.Values
                orderby m.name
                select m).ToList<GeoArea>();

        }
        public static System.Collections.Generic.List<CatalogFilterLocationItem> BuildFilterDestinations(System.Collections.Generic.List<CatalogExcursionMinPrice> catalog, int[] marks)
        {
            ExcursionProvider.CatalogFilterItemsCounterBuilder<GeoArea> builder = new ExcursionProvider.CatalogFilterItemsCounterBuilder<GeoArea>();
            if (catalog != null)
            {
                foreach (CatalogExcursionMinPrice item in catalog)
                {
                    if (item.excursion != null && item.excursion.destinations != null)
                    {
                        foreach (GeoArea d in item.excursion.destinations)
                        {
                            builder.Add(d.id, d);
                        }
                    }
                }
            }
            return (
                from m in builder.ToList()
                select new CatalogFilterLocationItem
                {
                    id = m.item.id,
                    name = m.item.name,
                    location = m.item.location,
                    count = m.count
                } into m
                orderby m.name
                select m).ToList<CatalogFilterLocationItem>();
        }
        public static System.Collections.Generic.List<CatalogFilterItem> BuildFilterLanguages(System.Collections.Generic.List<CatalogExcursionMinPrice> catalog, int[] marks)
        {
            ExcursionProvider.CatalogFilterItemsCounterBuilder<Language> builder = new ExcursionProvider.CatalogFilterItemsCounterBuilder<Language>();
            if (catalog != null)
            {
                foreach (CatalogExcursionMinPrice item in catalog)
                {
                    if (item.excursion != null && item.excursion.languages != null)
                    {
                        foreach (Language i in item.excursion.languages)
                        {
                            builder.Add(i.id, i);
                        }
                    }
                }
            }
            return (
                from m in builder.ToList()
                select new CatalogFilterItem
                {
                    id = m.item.id,
                    name = m.item.name,
                    count = m.count
                } into m
                orderby m.name
                select m).ToList<CatalogFilterItem>();
        }
        public static CatalogFilterDuration BuildFilterDurations(System.Collections.Generic.List<CatalogExcursionMinPrice> catalog)
        {
            System.TimeSpan? min = null;
            System.TimeSpan? max = null;
            if (catalog != null)
            {
                foreach (CatalogExcursionMinPrice item in catalog)
                {
                    if (item.excursion != null && item.excursion.duration.HasValue)
                    {
                        if (!min.HasValue || item.excursion.duration < min)
                        {
                            min = item.excursion.duration;
                        }
                        if (!max.HasValue || item.excursion.duration > max)
                        {
                            max = item.excursion.duration;
                        }
                    }
                }
            }
            return (min.HasValue && max.HasValue) ? new CatalogFilterDuration
            {
                min = min.Value,
                max = max.Value
            } : null;
        }
        public static ExcursionProvider.LoadStatesResult LoadStatesForPoints(string lang, int[] points)
        {
            if (points == null)
            {
                throw new System.ArgumentNullException("points");
            }
            XElement xml = new XElement("geopoints",
                from e in points
                select new XElement("geopoint", new XAttribute("id", e)));
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getStatesForPoints", "links,states", new
            {
                language = lang,
                points = xml
            });
            ExcursionProvider.LoadStatesResult result = new ExcursionProvider.LoadStatesResult();
            foreach (DataRow row2 in ds.Tables["links"].Rows.Cast<DataRow>())
            {
                result.Links[row2.ReadInt("pinc")] = row2.ReadInt("sinc");
            }
            result.States = (
                from DataRow row in ds.Tables["states"].Rows
                select ExcursionProvider.factory.StatePoint(row)).ToList<GeoArea>();
            return result;
        }
        public static System.Collections.Generic.List<CatalogFilterCategoryGroup> BuildDescriptionCategories(CatalogExcursion catalog)
        {
            ExcursionProvider.CatalogFilterItemsCounterBuilder<ExcursionCategory> builder = new ExcursionProvider.CatalogFilterItemsCounterBuilder<ExcursionCategory>();
            if (catalog != null && catalog.categories != null)
            {
                foreach (ExcursionCategory c in catalog.categories)
                {
                    builder.Add(c.id, c);
                }
            }
            return (
                from m in (
                    from m in builder.ToList()
                    group m by (m.item.categorygroup != null) ? m.item.categorygroup.name : null).Select(delegate (IGrouping<string, ExcursionProvider.CatalogFilterItemsCounterBuilder<ExcursionCategory>.CatalogFilterCounterItem<ExcursionCategory>> m)
                    {
                        CatalogFilterCategoryGroup catalogFilterCategoryGroup = new CatalogFilterCategoryGroup();
                        catalogFilterCategoryGroup.name = m.Key;
                        catalogFilterCategoryGroup.items = (
                            from n in m
                            orderby n.item.sort
                            select new CatalogFilterItem
                            {
                                id = n.item.id,
                                name = n.item.name,
                                count = 0
                            }).ToList<CatalogFilterItem>();
                        return catalogFilterCategoryGroup;
                    })
                orderby (m.name == null) ? 0 : 1, m.name
                select m).ToList<CatalogFilterCategoryGroup>();
        }
        public static System.Collections.Generic.List<ExcursionPickupHotel> GetExcursionPickupHotels(string lang, int excursion, int? excursionTime, int[] departurePoints)
        {
            string depaturepoints = (departurePoints != null && departurePoints.Length > 0) ? string.Join<int>(",", departurePoints) : null;
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getExcursionHotelPickup", "hotels", new
            {
                language = lang,
                excurs = excursion,
                extime = excursionTime,
                depaturepoints = depaturepoints
            });
            return (
                from DataRow row in ds.Tables["hotels"].Rows
                select ExcursionProvider.factory.ExcursionPickupHotel(row)).ToList<ExcursionPickupHotel>();
        }
    }
}
