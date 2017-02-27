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

        public ActionResult Index(int? page)
        {
            int pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(db.Artists.ToList().ToPagedList(pageNumber, pageSize));
        }
    }
}