using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
using Music_site_MVC.Models;

namespace Music_site_MVC.Database_EF
{
    public class Artist_Memory_Cache
    {
        public Artist GetValue(int id)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get("Artist" + id.ToString()) as Artist;
        }

        public bool Add(Artist value)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add("Artist" + value.Id.ToString(), value, DateTime.Now.AddMinutes(10));
        }

        public void Update(Artist value)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Set("Artist" + value.Id.ToString(), value, DateTime.Now.AddMinutes(10));
        }

        public void Delete(int id)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains("Artist" + id.ToString()))
            {
                memoryCache.Remove("Artist" + id.ToString());
            }
        }
    }
}