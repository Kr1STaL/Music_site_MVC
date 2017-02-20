using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Music_site_MVC.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public int ViewsCount { get; set; }


        public ICollection<Song> Songs { get; set; }
        public Artist()
        {
            Songs = new List<Song>();
        }
    }
}