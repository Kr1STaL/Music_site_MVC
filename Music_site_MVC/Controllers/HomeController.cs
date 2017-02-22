﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Music_site_MVC.Models;

namespace Music_site_MVC.Controllers
{
    public class HomeController : Controller
    {
        Music_MVC_Context db = new Music_MVC_Context();

        public ActionResult Index()
        {
            var artists = db.Artists.ToList();
            return View(artists);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}