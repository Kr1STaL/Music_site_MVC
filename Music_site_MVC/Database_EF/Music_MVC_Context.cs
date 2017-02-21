using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Music_site_MVC.Models;
using System.Text;
using HtmlAgilityPack;
using Music_site_MVC.Database_EF;

namespace Music_site_MVC
{
    public class Music_MVC_Context : DbContext
    {
        static Music_MVC_Context()
        {
            Database.SetInitializer(new Music_MVC_Context_Initializer());
        }

        public Music_MVC_Context()
            :base("DbConnection")
        {

        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Akord> Akords { get; set; }
    }

    class Music_MVC_Context_Initializer : CreateDatabaseIfNotExists <Music_MVC_Context>
    {
        protected override void Seed(Music_MVC_Context db)
        {
            Parsing parse = new Parsing();
            List<Artist> artists_list = parse.Parse_Artists();
            foreach (Artist artist in artists_list)
            {
                db.Artists.Add(artist);
            }
            db.SaveChanges();
        }
    }

}