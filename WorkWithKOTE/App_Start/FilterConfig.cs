﻿using System.Web;
using System.Web.Mvc;
using WorkWithKOTE.Filters;
namespace WorkWithKOTE
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}