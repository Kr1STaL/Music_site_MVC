using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Music_site_MVC.Models;

namespace Music_site_MVC.Controllers
{
    public class SongController : Controller
    {
        Music_MVC_Context db = new Music_MVC_Context();

        // GET: Song
        public ActionResult Index(int id)
        {
            Song song_info = db.Songs.Find(id);
            return View(song_info);
        }

        public PartialViewResult GetPrevSong(int artist_id ,int song_id)
        {
            Artist cur_artist = db.Artists.Find(artist_id);
            if (cur_artist.Songs.First().Id != song_id)
            {
                Song prev_song = db.Songs.Find(song_id - 1);
                return PartialView(prev_song);
            }
            else
            {
                Song prev_song = cur_artist.Songs.Last();
                return PartialView(prev_song);
            }
        }

        public PartialViewResult GetNextSong(int artist_id, int song_id)
        {
            Artist cur_artist = db.Artists.Find(artist_id);
            if (cur_artist.Songs.Last().Id != song_id)
            {
                Song next_song = db.Songs.Find(song_id + 1);
                return PartialView(next_song);
            }
            else
            {
                Song next_song = cur_artist.Songs.First();
                return PartialView(next_song);
            }
        }

    }
}