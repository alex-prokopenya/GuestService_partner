namespace GuestService.Controllers.Api
{
    using GuestService.Data;
    using GuestService.Models.Catalog;
    using Sm.System.Exceptions;
    using Sm.System.Mvc.Language;
    using System;
    using System.Web.Http;

    [HttpUrlLanguage]
    public class CatalogController : ApiController
    {
        [HttpGet, ActionName("geopointbyalias")]
        public GeoCatalogObject GeoPointByAlias([FromUri] GeoPointByAliasParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            if (string.IsNullOrEmpty(param.GeoPointAlias))
            {
                throw new ArgumentNullExceptionWithCode(0x6a, "gpa");
            }
            return CatalogProvider.GetGeoPointByAlias(param.Language, param.GeoPointAlias);
        }

        [ActionName("geopoints"), HttpGet]
        public GeoCatalogObjectList GeoPoints([FromUri] GeoCatalogParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            if (string.IsNullOrEmpty(param.SearchText))
            {
                throw new ArgumentNullExceptionWithCode(0x65, "s");
            }
            return new GeoCatalogObjectList(CatalogProvider.GetGeoPoints(param.Language, param.SearchText));
        }
    }
}

