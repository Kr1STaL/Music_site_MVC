using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Music_site_MVC.Models;
using System.Text;
using HtmlAgilityPack;

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

    class Music_MVC_Context_Initializer : CreateDatabaseIfNotExists<Music_MVC_Context>
    {
        protected override void Seed(Music_MVC_Context db)
        {
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = Encoding.UTF8;


            List<string> Artists_web_strings = new List<string>();
            List<string> Pages_web_strings = new List<string>();
            List<string> Songs_web_strings = new List<string>();

            for (int i = 1; i <= 10; i++)
            {
                string str = web.DownloadString("http://amdm.ru/chords/page" + i + "/");
                str = HttpUtility.HtmlDecode(str);
                Pages_web_strings.Add(str);
            }
        }
    }

}