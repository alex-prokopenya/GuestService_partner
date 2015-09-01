namespace GuestService.Controllers.Api
{
    using GuestService;
    using GuestService.Code;
    using GuestService.Data;
    using GuestService.Data.Survey;
    using GuestService.Models;
    using GuestService.Models.Excursion;
    using GuestService.Resources;
    using Sm.System.Exceptions;
    using Sm.System.Mvc.Language;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Http;

    [HttpUrlLanguage]
    public class ExcursionController : ApiController
    {
        [ActionName("catalog"), HttpGet]
        public CatalogResult Catalog([FromUri] CatalogParam param)
        {
            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            if (!param.StartPoint.HasValue && param.StartPointAlias != null)
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }
            ExcursionProvider.ExcursionSorting sorting = (!string.IsNullOrEmpty(param.SortOrder)) ? ((ExcursionProvider.ExcursionSorting)System.Enum.Parse(typeof(ExcursionProvider.ExcursionSorting), param.SortOrder)) : ExcursionProvider.ExcursionSorting.name;

            //получить id экскурсий в регионе

            //фильтровать по id

            CatalogResult result = new CatalogResult();
            result.excursions = ExcursionProvider.FindExcursions(param.Language, partner.id, param.FirstDate, param.LastDate, param.SearchLimit, param.StartPoint, param.SearchText, param.Categories, param.Departures, (param.Destinations != null && param.Destinations.Length > 0) ? param.Destinations : (param.DestinationState.HasValue ? new int[]
            {
                param.DestinationState.Value
            } : null), param.ExcursionLanguages, param.MinDuration, param.MaxDuration, new ExcursionProvider.ExcursionSorting?(sorting), param.WithoutPrice);
            System.Collections.Generic.Dictionary<int, ExcursionRank> rankings = SurveyProvider.GetExcursionsRanking((
                from m in result.excursions
                select m.excursion.id).ToList<int>(), param.Language);

            foreach (CatalogExcursionMinPrice excursion in result.excursions)
            {
                ExcursionRank rank = null;
                if (rankings.TryGetValue(excursion.excursion.id, out rank))
                {
                    excursion.ranking = CatalogExcursionRanking.Create(rank, param.Language);
                }
            }
            return result;
        }

        [HttpGet, ActionName("catalogimage")]
        public HttpResponseMessage CatalogImage(int id, [FromUri] CatalogImageParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            HttpResponseMessage message = new HttpResponseMessage();
            object[] args = new object[6];
            args[0] = id;
            args[1] = param.h;
            args[2] = param.w;
            args[3] = param.i;
            args[4] = param.Mode;
            bool? showDefault = param.ShowDefault;
            args[5] = showDefault.HasValue ? ((object) showDefault.GetValueOrDefault()) : ((object) 1);
            string key = string.Format("catalogImage[id:{0}][w:{1}][h:{2}][i:{3}][m:{4}][d:{5}]", args);
            ImageCacheItem item = HttpContext.Current.Cache[key] as ImageCacheItem;
            if ((item == null) || Settings.IsCacheDisabled)
            {
                Image catalogImage = ExcursionProvider.GetCatalogImage(id, param.Index);
                if ((catalogImage == null) && (!(showDefault = param.ShowDefault).GetValueOrDefault() && showDefault.HasValue))
                {
                    message.StatusCode = HttpStatusCode.NotFound;
                }
                else
                {
                    ImageFormatter formatter = new ImageFormatter(catalogImage, Pictures.nophoto) {
                        Format = (catalogImage != null) ? ImageFormat.Jpeg : ImageFormat.Png
                    };
                    param.ApplyFormat(formatter);
                    Stream stream = formatter.CreateStream();
                    if (stream != null)
                    {
                        item = ImageCacheItem.Create(stream, formatter.MediaType);
                        HttpContext.Current.Cache.Add(key, item, null, DateTime.Now.AddMinutes(10.0), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                        message.Content = new StreamContent(stream);
                        message.Content.Headers.ContentType = new MediaTypeHeaderValue(formatter.MediaType);
                    }
                    else
                    {
                        message.StatusCode = HttpStatusCode.NotFound;
                    }
                }
            }
            else
            {
                message.Content = new StreamContent(item.CraeteStream());
                message.Content.Headers.ContentType = new MediaTypeHeaderValue(item.MediaType);
            }
            message.Headers.CacheControl = new CacheControlHeaderValue();
            message.Headers.CacheControl.Public = true;
            message.Headers.CacheControl.MaxAge = new TimeSpan?(TimeSpan.FromHours(1.0));
            return message;
        }

        [HttpGet, ActionName("categories")]
        public CategoryWithGroupList Categories([FromUri] CategoryParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            if (!(param.StartPoint.HasValue || (param.StartPointAlias == null)))
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }
            return new CategoryWithGroupList(ExcursionProvider.GetCategories(param.Language, param.StartPoint));
        }

        [ActionName("categoriesbygroup"), HttpGet]
        public CategoryGroupWithCategoriesList CategoriesByGroup([FromUri] CategoryParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            if (!(param.StartPoint.HasValue || (param.StartPointAlias == null)))
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }
            return new CategoryGroupWithCategoriesList(ExcursionProvider.GetCategoriesByGroup(param.Language, param.StartPoint));
        }

        [HttpGet, ActionName("categoryimage")]
        public HttpResponseMessage CategoryImage(int id, [FromUri] ImageParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            HttpResponseMessage message = new HttpResponseMessage();
            object[] args = new object[5];
            args[0] = id;
            args[1] = param.h;
            args[2] = param.w;
            args[3] = param.Mode;
            bool? showDefault = param.ShowDefault;
            args[4] = showDefault.HasValue ? ((object) showDefault.GetValueOrDefault()) : ((object) 1);
            string key = string.Format("categoryImage[id:{0}][w:{1}][h:{2}][m:{3}][d:{4}]", args);
            ImageCacheItem item = null;
            if ((item == null) || Settings.IsCacheDisabled)
            {
                Image categoryImage = ExcursionProvider.GetCategoryImage(id);
                if ((categoryImage == null) && (!(showDefault = param.ShowDefault).GetValueOrDefault() && showDefault.HasValue))
                {
                    message.StatusCode = HttpStatusCode.NotFound;
                }
                else
                {
                    ImageFormatter formatter = new ImageFormatter(categoryImage, Pictures.nophoto) {
                        Format = (categoryImage != null) ? ImageFormat.Jpeg : ImageFormat.Png
                    };
                    param.ApplyFormat(formatter);
                    Stream stream = formatter.CreateStream();
                    if (stream != null)
                    {
                        item = ImageCacheItem.Create(stream, formatter.MediaType);
                        HttpContext.Current.Cache.Add(key, item, null, DateTime.Now.AddMinutes(10.0), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                        message.Content = new StreamContent(stream);
                        message.Content.Headers.ContentType = new MediaTypeHeaderValue(formatter.MediaType);
                        message.Headers.CacheControl = new CacheControlHeaderValue();
                        message.Headers.CacheControl.Public = true;
                        message.Headers.CacheControl.MaxAge = new TimeSpan?(TimeSpan.FromHours(1.0));
                    }
                    else
                    {
                        message.StatusCode = HttpStatusCode.NotFound;
                    }
                }
            }
            else
            {
                message.Content = new StreamContent(item.CraeteStream());
                message.Content.Headers.ContentType = new MediaTypeHeaderValue(item.MediaType);
            }
            message.Headers.CacheControl = new CacheControlHeaderValue();
            message.Headers.CacheControl.Public = true;
            message.Headers.CacheControl.MaxAge = new TimeSpan?(TimeSpan.FromHours(1.0));
            return message;
        }

        [HttpGet, ActionName("dates")]
        public ExcursionDateList Dates(int id, [FromUri] DatesParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            if (!param.FirstDate.HasValue)
            {
                throw new ArgumentNullExceptionWithCode(0x67, "firstadate");
            }
            if (!param.LastDate.HasValue)
            {
                throw new ArgumentNullExceptionWithCode(0x68, "lastdate");
            }
            return new ExcursionDateList(ExcursionProvider.GetDates(partner.id, id, param.FirstDate.Value, param.LastDate.Value));
        }

        [HttpGet, ActionName("departures")]
        public DeparturesResult Departures([FromUri] DepartureParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            if (!(param.StartPoint.HasValue || (param.StartPointAlias == null)))
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }
            object[] args = new object[5];
            args[0] = param.Language;
            args[1] = partner.id;
            args[2] = param.StartPoint.HasValue ? param.StartPoint.ToString() : "-";
            int? destinationState = param.DestinationState;
            args[3] = destinationState.HasValue ? (destinationState = param.DestinationState).ToString() : "-";
            args[4] = param.WithoutPrice;
            string key = string.Format("cache_departuresResult[ln:{0}][p:{1}][sp:{2}][st:{3}][wp:{4}]", args);
            DeparturesResult result = HttpContext.Current.Cache[key] as DeparturesResult;
            if ((result == null) || Settings.IsCacheDisabled)
            {
                DateTime? startDate = null;
                TimeSpan? minDuration = null;
                List<CatalogExcursionMinPrice> catalog = ExcursionProvider.FindExcursions(param.Language, partner.id, startDate, null, null, param.StartPoint, null, null, null, param.DestinationState.HasValue ? new int[] { param.DestinationState.Value } : null, null, minDuration, null, null, param.WithoutPrice);
                result = new DeparturesResult {
                    departures = ExcursionProvider.BuildRegionList(catalog)
                };

                HttpContext.Current.Cache.Add(key, result, null, DateTime.Now.AddMinutes(10.0), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            return result;
        }

        [ActionName("description"), HttpGet]
        public ExcursionDescriptionList Description([FromUri] DescriptionParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            if (param.Excursions == null)
            {
                throw new ArgumentNullExceptionWithCode(0x69, "ex");
            }
            return new ExcursionDescriptionList(ExcursionProvider.GetDescription(param.Language, param.Excursions));
        }

        [ActionName("destinationsandcategorygroups"), HttpGet]
        public DestinationsAndCategoryGroupsResult DestinationsAndCategoryGroups([FromUri] DestinationAndCategoryParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            if (!(param.StartPoint.HasValue || (param.StartPointAlias == null)))
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }
            DestinationsAndCategoryGroupsResult result = new DestinationsAndCategoryGroupsResult();
            FilterDetailsResult cachedFilterDetails = GetCachedFilterDetails(param, partner);
            result.destinationstates = cachedFilterDetails.destinationstates;
            result.categorygroups = ExcursionProvider.GetCategoriesByGroup(param.Language, param.StartPoint);
            return result;
        }

        [HttpGet, ActionName("excursionpickuphotels")]
        public ExcursionPickupHotelsList ExcursionPickupHotels([FromUri] ExcursionPickupHotelsParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            return new ExcursionPickupHotelsList(ExcursionProvider.GetExcursionPickupHotels(param.Language, param.Excursion, new int?(param.ExcursionTime), param.DeparturePoints));
        }

        [HttpGet, ActionName("exdescription")]
        public ExcursionExtendedDescriptionList ExtendedDescription([FromUri] ExtendedDescriptionParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            if (param.Excursions == null)
            {
                throw new ArgumentNullExceptionWithCode(0x69, "ex");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            if (!param.FirstDate.HasValue)
            {
                param.fd = new DateTime?(DateTime.Now.Date);
            }
            if (!param.LastDate.HasValue)
            {
                param.ld = new DateTime?(param.FirstDate.Value.AddDays((double) Settings.ExcursionCheckAvailabilityDays));
            }
            ExcursionExtendedDescriptionList list = new ExcursionExtendedDescriptionList();
            List<ExcursionDescription> list2 = ExcursionProvider.GetDescription(param.Language, param.Excursions);
            foreach (ExcursionDescription description in list2)
            {
                ExcursionExtendedDescription item = new ExcursionExtendedDescription(description);
                if ((description != null) && (description.excursion != null))
                {
                    item.categorygroups = ExcursionProvider.BuildDescriptionCategories(description.excursion);
                    item.excursiondates = ExcursionProvider.GetDates(partner.id, description.excursion.id, param.FirstDate.Value, param.LastDate.Value);
                    item.ranking = CatalogDescriptionExcursionRanking.Create(SurveyProvider.GetExcursionRanking(description.excursion.id, param.Language), param.Language);
                    item.surveynotes = ExcursionSurveyNote.Create(SurveyProvider.GetExcursionNotes(description.excursion.id));
                }
                list.Add(item);
            }
            return list;
        }

        [HttpGet, ActionName("filterdetails")]
        public FilterDetailsResult FilterDetails([FromUri] FiltersParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            if (!(param.StartPoint.HasValue || (param.StartPointAlias == null)))
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }
            return GetCachedFilterDetails(param, partner);
        }

        [ActionName("filters"), HttpGet]
        public FiltersResult Filters([FromUri] FiltersParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            if (!(param.StartPoint.HasValue || (param.StartPointAlias == null)))
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }
            object[] args = new object[5];
            args[0] = param.Language;
            args[1] = partner.id;
            args[2] = param.StartPoint.HasValue ? param.StartPoint.ToString() : "-";
            int? destinationState = param.DestinationState;
            args[3] = destinationState.HasValue ? (destinationState = param.DestinationState).ToString() : "-";
            args[4] = param.WithoutPrice;
            string key = string.Format("cache_filterResult[ln:{0}][p:{1}][sp:{2}][st:{3}][wp:{4}]", args);
            FiltersResult result = HttpContext.Current.Cache[key] as FiltersResult;
            if ((result == null) || Settings.IsCacheDisabled)
            {
                result = new FiltersResult();
                DateTime? startDate = null;
                TimeSpan? minDuration = null;
                List<CatalogExcursionMinPrice> catalog = ExcursionProvider.FindExcursions(param.Language, partner.id, startDate, null, null, param.StartPoint, null, null, null, param.DestinationState.HasValue ? new int[] { param.DestinationState.Value } : null, null, minDuration, null, null, param.WithoutPrice);
                result.categorygroups = ExcursionProvider.BuildFilterCategories(catalog, null);
            //    result.departures = ExcursionProvider.BuildFilterDepartures(catalog, null);
                result.destinations = ExcursionProvider.BuildFilterDestinations(catalog, null);
                result.languages = ExcursionProvider.BuildFilterLanguages(catalog, null);
                result.durations = ExcursionProvider.BuildFilterDurations(catalog);
                HttpContext.Current.Cache.Add(key, result, null, DateTime.Now.AddMinutes(10.0), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            return result;
        }

        private static FilterDetailsResult GetCachedFilterDetails(IStartPointAndLanguageAndPriceOptionParam param, WebPartner partner)
        {
            string filtersResultCacheKey = string.Format("cache_filterDetails[ln:{0}][p:{1}][sp:{2}][wp:{3}]", new object[]
            {
                param.Language,
                partner.id,
                param.StartPoint.HasValue ? param.StartPoint.ToString() : "-",
                param.WithoutPrice
            });
            FilterDetailsResult result = HttpContext.Current.Cache[filtersResultCacheKey] as FilterDetailsResult;
            if (result == null || Settings.IsCacheDisabled)
            {
                result = new FilterDetailsResult();
                System.Collections.Generic.List<CatalogExcursionMinPrice> excursions = ExcursionProvider.FindExcursions(param.Language, partner.id, null, null, null, param.StartPoint, null, null, null, null, null, null, null, null, param.WithoutPrice);
                result.categorygroups = ExcursionProvider.BuildFilterCategories(excursions, null);
             //   result.departures = ExcursionProvider.BuildFilterDepartures(excursions, null);
                result.languages = ExcursionProvider.BuildFilterLanguages(excursions, null);
                result.durations = ExcursionProvider.BuildFilterDurations(excursions);
                System.Collections.Generic.List<CatalogFilterLocationItem> destinations = ExcursionProvider.BuildFilterDestinations(excursions, null);
                if (destinations != null)
                {
                    result.destinations = new System.Collections.Generic.List<CatalogFilterLocationWithStateItem>();
                    if (destinations.Count > 0)
                    {
                        ExcursionProvider.LoadStatesResult stateResult = ExcursionProvider.LoadStatesForPoints(param.Language, (
                            from m in destinations
                            select m.id).ToArray<int>());
                        foreach (CatalogFilterLocationItem item in destinations)
                        {
                            int stateId;
                            if (stateResult.Links.TryGetValue(item.id, out stateId))
                            {
                                result.destinations.Add(new CatalogFilterLocationWithStateItem(item, stateId.ToString()));
                            }
                            else
                            {
                                result.destinations.Add(new CatalogFilterLocationWithStateItem(item, null));
                            }
                        }
                        result.destinationstates = stateResult.States;
                    }
                }
                HttpContext.Current.Cache.Add(filtersResultCacheKey, result, null, System.DateTime.Now.AddMinutes(10.0), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            return result;
        }

        [ActionName("price"), HttpGet]
        public ExcursionPriceList Price(int id, [FromUri] PriceParam param)
        {
            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            if (!param.Date.HasValue)
            {
                throw new ArgumentNullExceptionWithCode(202, "date");
            }
            if (!param.StartPoint.HasValue && param.StartPointAlias != null)
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }
            ExcursionPriceList result;
            if (param.Date.Value.Date < System.DateTime.Today)
            {
                result = new ExcursionPriceList(new System.Collections.Generic.List<ExcursionPrice>());
            }
            else
            {
                System.Collections.Generic.List<ExcursionPrice> prices = ExcursionProvider.GetPrice(param.Language, partner.id, id, param.Date.Value, param.StartPoint);
                result = new ExcursionPriceList((
                    from m in prices
                    where !m.issaleclosed && !m.isstopsale && m.price != null
                    select m).ToList<ExcursionPrice>());
            }
            return result;
        }

        [ActionName("search"), HttpGet]
        public SearchExcursionResult Search([FromUri] SearchParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            if (string.IsNullOrEmpty(param.SearchText))
            {
                throw new ArgumentNullExceptionWithCode(0x65, "s");
            }
            if (!(param.StartPoint.HasValue || (param.StartPointAlias == null)))
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }
            int limit = (param.SearchLimit.HasValue && (param.SearchLimit.Value > 0)) ? param.SearchLimit.Value : Settings.ExcursionGeographySearchLimit;
            return ExcursionProvider.SearchExcursionObjects(param.Language, param.StartPoint, param.SearchText, limit);
        }
    }
}

