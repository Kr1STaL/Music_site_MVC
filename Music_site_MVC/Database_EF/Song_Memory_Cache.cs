using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
using Music_site_MVC.Models;

namespace Music_site_MVC.Database_EF
{
    public class Song_Memory_Cache
    {
        public Song GetValue(int id)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get("Song" + id.ToString()) as Song;
        }

        public bool Add(Song value)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add("Song" + value.Id.ToString(), value, DateTime.Now.AddMinutes(10));
        }

        public void Update(Song value)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Set("Song" + value.Id.ToString(), value, DateTime.Now.AddMinutes(10));
        }

        public void Delete(int id)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains("Song" + id.ToString()))
            {
                memoryCache.Remove("Song" + id.ToString());
            }
        }

    }
}