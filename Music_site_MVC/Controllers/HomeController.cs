using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Music_site_MVC.Models;
using PagedList.Mvc;
using PagedList;
using Music_site_MVC.Database_EF;
using MvcSiteMapProvider;

namespace Music_site_MVC.Controllers
{
    public class HomeController : Controller
    {
        Music_MVC_Context db = new Music_MVC_Context();

        [HttpGet]
        public ActionResult Index(int? page, string sortOrder)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var artists = db.Artists.Select(ar => ar);
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
            return View(artists.ToPagedList(pageNumber, pageSize));
        }

        public List<Artist> FiltrA(List<Artist> artists)
        {
            List<Artist> filtered_artists = new List<Artist>();
            foreach (var item in artists)
            {
                if (item.Name.ToLower()[0] == 'а')
                {
                    filtered_artists.Add(item);
                }
            }
            return filtered_artists;
        }
    }
}