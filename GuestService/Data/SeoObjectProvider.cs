using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GuestService.Models;
using Sm.System.Database;
using System.Data;

namespace GuestService.Data
{
    public class SeoObjectProvider
    {
        public static SeoObject GetSeoObject(int id, string type, string lang) {

            //запрос на поиск объекта
            var query = "select text, title, keywords, description  " +
                        " from seo_objects " +
                        " where lang = '{0}' and  " +
                        "       object_id = {1} and   " +
                        "       object_type = '{2}' ";

            query = string.Format(query, lang, id, type);

            var res = DatabaseOperationProvider.Query(query, "seo" , new object { });

            //если есть такой объект, выводим его инфу
            foreach (DataRow row in res.Tables["seo"].Rows)
                return new SeoObject() {
                    Description = row.ReadNullableString("description"),
                    Title = row.ReadNullableString("title"),
                    Keywords = row.ReadNullableString("keywords"),
                    SeoText = row.ReadNullableString("text")
                };


            //если нет, пустую заглушку
            return new SeoObject() {
                            Description = "",
                            Keywords = "",
                            SeoText = "",
                            Title = ""
                        };
        }
    }
}
