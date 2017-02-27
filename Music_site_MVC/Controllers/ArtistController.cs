using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace Music_site_MVC.Controllers
{
    public class ArtistController : Controller
    {
        Music_MVC_Context db = new Music_MVC_Context();

        // GET: Artist
        public ActionResult Index(int id, int? page, string sortOrder)
        {
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            var songs = from s in db.Songs
                        where s.ArtistId == id
                        select s;
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