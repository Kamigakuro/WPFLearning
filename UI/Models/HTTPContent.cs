using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using UI.Classes;

namespace UI.Models
{
    public class HTTPContent
    {
        private string getHttpPage(string url)
        {
            try
            {
                WebRequest req = WebRequest.Create(url);
                WebResponse res = req.GetResponse();
                using (StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex) { return ex.Message; }
        }

        public ObservableCollection<WGNew> GetNews(string url)
        {
            string content = getHttpPage(url);
            if (content == null) return null;
            
            Regex regex = new Regex(@"<div class=""preview_item"">.*?<a(.*?)<div class=""preview_item"">");
            MatchCollection matches = regex.Matches(content.Replace("\n", ""));
            if (matches.Count > 0)
            {
                ObservableCollection<WGNew> collection = new ObservableCollection<WGNew>();
                foreach (var match in matches)
                {
                    WGNew New = new WGNew();
                    Regex reg = new Regex(@"<a class=""preview_link"" href=""(.*?)"">");
                    MatchCollection matcher = reg.Matches(match.ToString());
                    if (matcher.Count > 0) New.Url = "https://worldoftanks.ru/" + matcher[0].Groups[1].Value;

                    reg = new Regex(@"<span class=""preview_image-holder"" style=""background-image: url\(..(.*?)\)"">");
                    matcher = reg.Matches(match.ToString());
                    if (matcher.Count > 0) New.Image = "http://" + matcher[0].Groups[1].Value;
                    
                    reg = new Regex(@"<h2 class=""preview_title"">(.*?)</h2>");
                    matcher = reg.Matches(match.ToString());
                    if (matcher.Count > 0) New.Caption = matcher[0].Groups[1].Value;

                    reg = new Regex(@"<span class=""preview_time js-newstime""\s*data-timestamp=""(.*?)""></span>");
                    matcher = reg.Matches(match.ToString());
                    if (matcher.Count > 0) New.Creationdate = matcher[0].Groups[1].Value;
                    collection.Add(New);
                }
                return collection;
            }
            return null;
        }




    }
}