using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkWithKOTE.Models;

namespace WorkWithKOTE.Code
{
    public static class DropDown 
    {
       
         public  static List<System.Web.Mvc.SelectListItem> SelectlistForTypeTour() {
             UsersContext db = new UsersContext();
             List<System.Web.Mvc.SelectListItem> list = new List<System.Web.Mvc.SelectListItem>();
             var typeOfTour = db.TypeOfTours;
             foreach(var Item in typeOfTour )
             {
                 list.Add(new System.Web.Mvc.SelectListItem { Value = Convert.ToString(Item.TypeOfTourId), Text = Item.TypeOfTourName });
             }
             return list ;
        }
         public static List<System.Web.Mvc.SelectListItem> SelectlistForArrive()
         {
             UsersContext db = new UsersContext();
             List<System.Web.Mvc.SelectListItem> list = new List<System.Web.Mvc.SelectListItem>();
             var placeArrive = db.Tour.Select(m=>m.PlaceOfArrival).Distinct();
            foreach(var item in placeArrive)
            {
                list.Add(new System.Web.Mvc.SelectListItem { Text = item });
            }
            return list;
         }
         public static List<System.Web.Mvc.SelectListItem> Price()
        {
            List<System.Web.Mvc.SelectListItem> list = new List<System.Web.Mvc.SelectListItem>() { new System.Web.Mvc.SelectListItem { Text = "1000" }, new System.Web.Mvc.SelectListItem { Text = "2000" }, new System.Web.Mvc.SelectListItem { Text = "3000" }, new System.Web.Mvc.SelectListItem { Text = "4000" } };
            return list;
        }
    }
}