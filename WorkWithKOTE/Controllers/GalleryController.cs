﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkWithKOTE.Models;

namespace WorkWithKOTE.Controllers
{
    public class GalleryController : Controller
    {
        //
        // GET: /Gallery/
        UsersContext db = new UsersContext();

        public ActionResult CreateGallery()
        {
            var data = new Gallery();
            return View(data);
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}