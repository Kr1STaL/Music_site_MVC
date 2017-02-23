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

        public ActionResult GetPrevSong(int artist_id ,int song_id)
        {
            Artist cur_artist = db.Artists.Find(artist_id);
            Song cur_song = db.Songs.Find(song_id);
            int songs_count = cur_artist.Songs.Count;
            int cur_position = cur_artist.Songs.ToList().IndexOf(cur_song);
            if ((cur_position - 1) >= 0)
            {
                Song song_info = cur_song.Artist.Songs.ElementAt(cur_position - 1);
                return PartialView(song_info);
            }
            else
            {
                Song song_info = cur_song.Artist.Songs.Last();
                return PartialView(song_info);
            }
        }

    }
}