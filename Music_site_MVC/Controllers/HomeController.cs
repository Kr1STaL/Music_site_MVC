using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Music_site_MVC.Models;
using PagedList.Mvc;
using PagedList;

namespace Music_site_MVC.Controllers
{
    public class HomeController : Controller
    {
        Music_MVC_Context db = new Music_MVC_Context();

        public ActionResult Index(int? page, string sortOrder)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            var artists = from s in db.Artists
                           select s;
            switch (sortOrder)
            {
                case "Name desc":
                    artists = artists.OrderByDescending(s => s.Name);
                    break;
                case "Songs":
                    artists = artists.OrderBy(s => s.Songs.Count);
                    break;
                case "Songs desc":
                    artists = artists.OrderByDescending(s => s.Songs.Count);
                    break;
                default:
                    artists = artists.OrderBy(s => s.Name);
                    break;
            }
            return View(artists.ToList().ToPagedList(pageNumber, pageSize));
        }
    }
}