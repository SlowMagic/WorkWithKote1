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
         public static List<System.Web.Mvc.SelectListItem> Places()
         {
             UsersContext db = new UsersContext();
             var places = db.Places.Select(m => m.PlaceName).Distinct();
             List<System.Web.Mvc.SelectListItem> list = new List<SelectListItem>();
             List<string> newList = new List<string>();
             foreach (var item in places)
             {
                 newList.Add(item);
             }
             var place1 = db.Tour.Select(m => m.PlaceOfArrival).Distinct();
             foreach (var item in place1)
             {
                 newList.Add(item);
             }
             newList =  newList.Distinct().ToList();
             foreach (var item in newList)
             {
                 list.Add(new SelectListItem { Text = item});
             }
             return list;
         }
         public static List<System.Web.Mvc.SelectListItem> PlaceDeparture()
         {
             UsersContext db = new UsersContext();
             var places = db.Tour.Select(m=>m.PlaceOfDeparture).Distinct();
             List<System.Web.Mvc.SelectListItem> list = new List<SelectListItem>();
             foreach (var item in places)
             {
                 list.Add(new SelectListItem { Text = item });
             }
             return list;
         }
         public static List<System.Web.Mvc.SelectListItem> Tags()
         {
             UsersContext db = new UsersContext();
             var tags = db.Teg.Select(m => m.TagName).Distinct();
             List<System.Web.Mvc.SelectListItem> list = new List<SelectListItem>();
             foreach(var item in tags)
             {
                 list.Add(new SelectListItem { Text = item });
             }
             return list;
         }
         public static List<System.Web.Mvc.SelectListItem> DataFirst()
         {
           List<SelectListItem>list =   new List<SelectListItem> { new SelectListItem{Value="1", Text="Январь"},  new SelectListItem{Value="2",Text="Февраль"}, new SelectListItem{Value="3",Text="Март"},
                 new SelectListItem{Value="4",Text="Апрель"}, new SelectListItem{Value="5",Text="Май"}, new SelectListItem{Value = "6",Text="Июнь"}, new SelectListItem{Value="7",Text="Июль"},
                 new SelectListItem{Value="8",Text="Август"}, new SelectListItem{Value="9",Text="Сентябрь"}, new SelectListItem{Value="10",Text="Октябрь"}, new SelectListItem{Value="11",Text="Ноябрь"}, new SelectListItem{Value="12",Text="Декабрь"}};
           int month = DateTime.Now.Month;
           List<SelectListItem> list1 = new List<SelectListItem>();
           List<SelectListItem> list2 = new List<SelectListItem>();
             foreach(var item in list)
            {
                if (int.Parse(item.Value) >= month)
                {
                    list1.Add(item);
                }
                if (int.Parse(item.Value) < month)
                {
                    list2.Add(item);
                }
            }
             list = new List<SelectListItem>();
             foreach (var item in list1)
             {
                 list.Add(item);
             }
             foreach (var item in list2)
             {
                 list.Add(item);
             }
             return list;
         }
    }
}