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
            ViewBag.curSong = song_info;
            return View(song_info);
        }

        public PartialViewResult GetPrevSong(int artist_id ,int song_id)
        {
            Artist cur_artist = db.Artists.Find(artist_id);
            Song cur_song = db.Songs.Find(song_id);
            if (cur_song == null || cur_song.ArtistId != artist_id)
            {
                Song song_info = cur_artist.Songs.Last();
                ViewBag.curSong = song_info;
                return PartialView(song_info);
            }
            else
            {
                ViewBag.curSong = cur_song;
                return PartialView(cur_song);
            }
        }

        public PartialViewResult GetNextSong(int artist_id, int song_id)
        {
            Artist cur_artist = db.Artists.Find(artist_id);
            Song cur_song = db.Songs.Find(song_id);
            if (cur_song == null || cur_song.ArtistId != artist_id)
            {
                Song song_info = cur_artist.Songs.First();
                ViewBag.curSong = song_info;
                return PartialView(song_info);
            }
            else
            {
                ViewBag.curSong = cur_song;
                return PartialView(cur_song);
            }
        }

    }
}