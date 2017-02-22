using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Music_site_MVC.Controllers
{
    public class SongController : Controller
    {
        Music_MVC_Context db = new Music_MVC_Context();

        // GET: Song
        public ActionResult Index(int id)
        {
            var song_info = db.Songs.Find(id);
            return View(song_info);
        }
    }
}