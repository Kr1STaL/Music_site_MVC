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
        public ActionResult Index(int id, int? page)
        {
            var artist_info = db.Artists.Find(id);
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return View(artist_info.Songs.ToPagedList(pageNumber, pageSize));
            //return View(artist_info);
        }
    }
}