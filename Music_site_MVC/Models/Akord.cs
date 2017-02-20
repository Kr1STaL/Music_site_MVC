using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Music_site_MVC.Models
{
    public class Akord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public ICollection<Song> Songs { get; set; }
        public Akord()
        {
            Songs = new List<Song>();
        }

    }
}