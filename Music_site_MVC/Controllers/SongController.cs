using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Music_site_MVC.Models;
using System.Data.Entity;
using Music_site_MVC.Database_EF;
using System.Net;

namespace Music_site_MVC.Controllers
{
    public class SongController : Controller
    {
        Music_MVC_Context db = new Music_MVC_Context();
        Song_Memory_Cache song_cache;
        Artist_Memory_Cache artist_cache;

        public SongController()
        {
            song_cache = new Song_Memory_Cache();
            artist_cache = new Artist_Memory_Cache();
        }

        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = song_cache.GetValue(id.Value);
            if (result == null)
            {
                result = db.Songs.Find(id);
                song_cache.Add(result);
            }
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        [HttpGet]
        public PartialViewResult GetPrevSong(int artist_id ,int song_id)
        {

            Artist cur_artist = artist_cache.GetValue(artist_id);
            if (cur_artist == null)
            {
                cur_artist = db.Artists.Find(artist_id);
                artist_cache.Add(cur_artist);
            }
            if (cur_artist.Songs.First().Id != song_id)
            {
                Song prev_song = song_cache.GetValue(song_id - 1);
                if (prev_song == null)
                {
                    prev_song = db.Songs.Find(song_id - 1);
                    song_cache.Add(prev_song);
                }
                return PartialView(prev_song);
            }
            else
            {
                Song prev_song = cur_artist.Songs.Last();
                return PartialView(prev_song);
            }
        }

        [HttpGet]
        public PartialViewResult GetNextSong(int artist_id, int song_id)
        {
            Artist cur_artist = artist_cache.GetValue(artist_id);
            if (cur_artist == null)
            {
                cur_artist = db.Artists.Find(artist_id);
                artist_cache.Add(cur_artist);
            }
            if (cur_artist.Songs.Last().Id != song_id)
            {
                Song next_song = song_cache.GetValue(song_id + 1);
                if (next_song == null)
                {
                    next_song = db.Songs.Find(song_id + 1);
                    song_cache.Add(next_song);
                }
                return PartialView(next_song);
            }
            else
            {
                Song next_song = cur_artist.Songs.First();
                return PartialView(next_song);
            }
        }

        [HttpPost]
        public ActionResult ChangeSong(int SongId, string SongText)
        {
            Song modif_song = song_cache.GetValue(SongId);
            if (modif_song == null)
            {
                modif_song = db.Songs.Find(SongId);
                song_cache.Add(modif_song);
            }
            if (modif_song.SongText != SongText)
            {
                modif_song.SongText = SongText;
                db.Entry(modif_song).State = EntityState.Modified;
                db.SaveChanges();
                song_cache.Update(modif_song);
                return RedirectToAction("Index/" + SongId);
            }
            else
            {
                return RedirectToAction("Index/" + SongId);
            }
        }

        [HttpPost]
        public ActionResult AddAkord(string Akord_Name, int SongId)
        {
            Song curSong = song_cache.GetValue(SongId);
            if (curSong == null)
            {
                curSong = db.Songs.Find(SongId);
                song_cache.Add(curSong);
            }
            if ((curSong.Akords.Where(p => p.Name == Akord_Name).Count()) == 0 && (db.Akords.Where(a => a.Name == Akord_Name).Count() != 0))
            {
                var Akord = db.Akords.Where(p => p.Name == Akord_Name).First();
                curSong = db.Songs.Find(SongId);
                curSong.Akords.Add(Akord);
                db.Entry(curSong).State = EntityState.Modified;
                db.SaveChanges();
                song_cache.Update(curSong);
                return new HttpStatusCodeResult(200);
            }
            else
            {
                return RedirectToAction("Index/" + SongId);
            }
        }

        [HttpPost]
        public ActionResult DeleteAkord(string Akord_Name, int SongId)
        {
            Song curSong = song_cache.GetValue(SongId);
            if (curSong == null)
            {
                curSong = db.Songs.Find(SongId);
                song_cache.Add(curSong);
            }
            var Akord = db.Akords.Where(p => p.Name == Akord_Name).First();
            curSong = db.Songs.Find(SongId);
            curSong.Akords.Remove(Akord);
            db.Entry(curSong).State = EntityState.Modified;
            db.SaveChanges();
            song_cache.Update(curSong);
            return new HttpStatusCodeResult(200);
        }

    }
}