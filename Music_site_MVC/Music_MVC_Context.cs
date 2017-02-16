using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Music_site_MVC.Models;

namespace Music_site_MVC
{
    public class Music_MVC_Context : DbContext
    {
        public Music_MVC_Context()
            :base("DbConnection")
        { }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Akord> Akords { get; set; }
    }
}