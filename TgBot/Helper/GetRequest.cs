using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TgBot.Helper
{
    public static class GetRequest
    {
        public static string GetUrl(string text)
        {
            string mes = text;
            string[] words = mes.Split();
            for (int i = 0; i < words.Length; i++)
            {
                if (i == words.Length - 1)
                    break;
                words[i] = words[i] + "%20";
            }
            string request = "https://www.ozon.ru/search/?deny_category_prediction=true&text=";
            for (int i = 0; i < words.Length; i++)
            {
                request += words[i];
            }
            //request += "&from_global = true";
            return request;
        }
        public static string GetPage(string link)
        {
            HttpRequest request = new HttpRequest();
            //Задание атрибутов пакета, чтобы получить только нужный пакет от сервера
            /*request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Accept-Language", "ru,en;q=0.9");
            request.AddHeader("Cache-Control", "max-age=0");
            request.AddHeader("Host", "www.ozon.ru");
            request.AddHeader("Referer", link);
            request.AddHeader("Upgrade-Insecure-Requests", "1");*/
            //request.AddHeader(HttpHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            request.KeepAlive = true;
            request.UserAgent = Http.ChromeUserAgent();

            string response = request.Get(link).ToString();
            return response;
        }

        public static List<Product> ParseCard(string data)
        {
            List<Product> products = new List<Product>();
            var actualData = data;
            for (int i = 0; i < 3; i++)
            {
                var cardPriceStart = actualData.IndexOf("ui-s5 ui-s8");
                var priceStart = actualData.IndexOf(">", cardPriceStart) + 1;
                var priceEnd = actualData.IndexOf("</span>", priceStart);
                string price = actualData.Substring(priceStart, priceEnd - priceStart);
                var cardTitleStart = actualData.IndexOf("de0 ed0 de1 e2d tsBodyL i5n");

                var titleStart = actualData.IndexOf("<span>", cardTitleStart) + 6;
                var titleEnd = actualData.IndexOf("</span>", titleStart);
                string title = actualData.Substring(titleStart, titleEnd - titleStart);

                actualData = actualData.Substring(titleEnd, actualData.Length - titleEnd);

                Product product = new Product { Title = title, Price = price };
                products.Add(product);
            }

            foreach (var item in products)
            {
                Console.WriteLine(item.Title);
                Console.WriteLine(item.Price);
            }
           
            return products;
        }

    }
}
