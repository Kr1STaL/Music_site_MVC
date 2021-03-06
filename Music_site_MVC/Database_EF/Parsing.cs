﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Music_site_MVC.Models;
using HtmlAgilityPack;
using System.Text;
using System.Threading;

namespace Music_site_MVC.Database_EF
{
    public class Parsing
    {
        Music_MVC_Context db = new Music_MVC_Context();
        List<Akord> all_akords = new List<Akord>();

        public List<Artist> Parse_Artists()
        {
            List<Akord> vot = db.Akords.ToList();
            all_akords = vot;
            List<string> Top_pages = new List<string>();
            List<string> Artist_pages = new List<string>();
            for (int i = 5; i <= 10; i++)
            {
                Top_pages.Add("http://amdm.ru/chords/page" + i + "/");
                foreach (var item in Get_Artist_List(Top_pages[i - 5]))
                {
                    Artist_pages.Add(item);
                }
            }
            return Get_Artist_Biograpy(Artist_pages);
        }

        public void InitBaze(List<Artist> artists)
        {
            foreach (var item in artists)
            {
                db.Artists.Add(item);
            }
            db.SaveChanges();
        }

        public List<string> Get_Artist_List(string html_page)
        {
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = Encoding.UTF8;

            string str = web.DownloadString(html_page);
            HtmlDocument doc = new HtmlDocument();

            doc.LoadHtml(str);
            HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//table[@class='items']/tr//a[@class='artist']");

            List<string> web_artist_list = new List<string>();

            foreach (HtmlNode n in node)
            {
                string web_href = n.Attributes["href"].Value;
                string web_http = web_href.Insert(0, "http:");
                web_artist_list.Add(web_http);
            }
            return web_artist_list;
        }

        public List<Artist> Get_Artist_Biograpy(List<string> artist_pages)
        {
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = Encoding.UTF8;

            List<Artist> artists_list = new List<Artist>();


            foreach (string html_page in artist_pages)
            {
                string str = web.DownloadString(html_page);
                HtmlDocument doc = new HtmlDocument();

                doc.LoadHtml(str);
                HtmlNode biograpy_node = doc.DocumentNode.SelectSingleNode("//div[@class='artist-profile__bio']/a");
                HtmlNodeCollection songs_node = doc.DocumentNode.SelectNodes("//table[@id='tablesort']/tr//a");

                List<string> artist_songs_list = new List<string>();

                Artist artist;

                if (biograpy_node != null)
                {
                    string web_href = biograpy_node.Attributes["href"].Value;
                    string web_http = web_href.Insert(0, "http://amdm.ru");
                    artist = Set_Artist_data(web_http);
                }
                else
                {
                    artist = Set_Artist_data(html_page);
                }

                foreach (HtmlNode song in songs_node)
                {
                    string web_href = song.Attributes["href"].Value;
                    string web_http = web_href.Insert(0, "http:");
                    artist_songs_list.Add(web_http);
                }

                artists_list.Add(Set_Artist_songs(artist_songs_list, artist));
                return artists_list;
            }

            return artists_list;
        }

        public Artist Set_Artist_data(string biography_page)
        {
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = Encoding.UTF8;
            string str = web.DownloadString(biography_page);
            HtmlDocument doc = new HtmlDocument();

            doc.LoadHtml(str);

            HtmlNode name_node = doc.DocumentNode.SelectSingleNode("//div[@class='artist-profile__info']/h1");
            HtmlNode biography_node = doc.DocumentNode.SelectSingleNode("//div[@class='artist-profile__bio']");

            Artist artist = new Artist();

            artist.Name = name_node.InnerText;
            artist.Biography = biography_node.InnerText;

            return artist;
        }

        public Artist Set_Artist_songs(List<string> songs_pages, Artist artist)
        {
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = Encoding.UTF8;

            List<Song> artist_songs = new List<Song>();

            foreach (string html_page in songs_pages)
            {
                again:
                try
                {
                    string str = web.DownloadString(html_page);
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(str);

                    HtmlNode name_node = doc.DocumentNode.SelectSingleNode("//span[@itemprop='name']");
                    HtmlNode text_node = doc.DocumentNode.SelectSingleNode("//pre[@itemprop='chordsBlock']");
                    HtmlNodeCollection akkords_node = doc.DocumentNode.SelectNodes("//div[@id='song_chords']/img");

                    if (akkords_node != null && name_node != null && text_node != null)
                    {
                        Song song = new Song();
                        List<Akord> akkord_list = new List<Akord>();

                        song.SongName = name_node.InnerText;
                        song.SongText = text_node.InnerText;

                        foreach (HtmlNode note in akkords_node)
                        {
                            Akord ak = new Akord();
                            Akord akkord = new Akord();
                            akkord.Name = note.Attributes["alt"].Value;
                            akkord.Image = note.Attributes["src"].Value.Insert(0, "http:");
                            if ((ak = all_akords.Find(x => x.Name == akkord.Name)) != default(Akord))
                            {
                                akkord_list.Add(ak);
                            }
                            else
                            {
                                akkord_list.Add(akkord);
                                all_akords.Add(akkord);
                            }
                        }
                        song.Akords = akkord_list;
                        artist_songs.Add(song);
                    }
                }
                catch (Exception error)
                {
                    if (error.Message == "The remote server returned an error: (404) Not Found.")
                    {
                        continue;
                    }
                    else
                    {
                        Thread.Sleep(40000);
                        goto again;
                    }
                }
            }

            artist.Songs = artist_songs;
            //Thread.Sleep(40000);
            return artist;
        }
    }
}