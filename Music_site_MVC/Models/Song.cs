using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Music_site_MVC.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string SongName { get; set; }
        public string SongText { get; set; }
        public int ViewsCount { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        public ICollection<Akord> Akords { get; set; }
        public Song()
        {
            Akords = new List<Akord>();
        }

    }
}