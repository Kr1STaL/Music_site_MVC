using System;
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

        public List<Artist> Parse_Artists()
        {
            List<string> Top_pages = new List<string>();
            List<string> Artist_pages = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                Top_pages.Add("http://amdm.ru/chords/page" + i + "/");
                foreach (var item in Get_Artist_List(Top_pages[i - 1]))
                {
                    Artist_pages.Add(item);
                }
            }
            return Get_Artist_Biograpy(Artist_pages);
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
                Thread.Sleep(500);
                string str = web.DownloadString(html_page);
                HtmlDocument doc = new HtmlDocument();

                doc.LoadHtml(str);

                HtmlNode name_node = doc.DocumentNode.SelectSingleNode("//span[@itemprop='name']");
                HtmlNode text_node = doc.DocumentNode.SelectSingleNode("//pre[@itemprop='chordsBlock']");
                HtmlNodeCollection akkords_node = doc.DocumentNode.SelectNodes("//div[@id='song_chords']/img");

                Song song = new Song();
                List<Akord> akkord_list = new List<Akord>();

                song.SongName = name_node.InnerText;
                song.SongText = text_node.InnerText;

                foreach (HtmlNode note in akkords_node)
                {
                    Akord akkord = new Akord();
                    akkord.Name = note.Attributes["alt"].Value;
                    akkord.Image = note.Attributes["src"].Value.Insert(0, "http:");
                    akkord_list.Add(akkord);
                }

                song.Akords = akkord_list;
                artist_songs.Add(song);
            }
            artist.Songs = artist_songs;
            return artist;
        }
    }
}