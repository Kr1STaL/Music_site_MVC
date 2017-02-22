using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Music_site_MVC.Controllers
{
    public class ArtistController : Controller
    {
        Music_MVC_Context db = new Music_MVC_Context();

        // GET: Artist
        public ActionResult Index(int id)
        {
            var artist_info = db.Artists.Find(id);
            return View(artist_info);
        }
    }
}