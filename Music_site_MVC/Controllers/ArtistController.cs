using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Web.UI;

namespace Music_site_MVC.Controllers
{
    public class ArtistController : Controller
    {
        Music_MVC_Context db = new Music_MVC_Context();

        public ActionResult Index(int id, int? page, string sortOrder)
        {
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            var songs = db.Songs.Where(p => p.ArtistId == id);
            switch (sortOrder)
            {
                case "Name desc":
                    songs = songs.OrderByDescending(s => s.SongName);
                    break;
                case "Songs":
                    songs = songs.OrderBy(s => s.ViewsCount);
                    break;
                case "Songs desc":
                    songs = songs.OrderByDescending(s => s.ViewsCount);
                    break;
                default:
                    songs = songs.OrderBy(s => s.SongName);
                    break;
            }
            return View(songs.ToPagedList(pageNumber, pageSize));
        }
    }
}