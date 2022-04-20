using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using TgBot.Helper;

namespace TgBot
{
    class Program
    {
        //t.me/Sample1135_bot
        static TelegramBotClient BotClient = new TelegramBotClient("5339394654:AAEg1tY3mXcS1jAd2K_m4FvNiIX7RsgGZNA");
        static CancellationTokenSource cts = new CancellationTokenSource();

        static Users users = new Users();
        static void Main(string[] args)
        {
            //var test = BotClient.GetMeAsync().Result;
            //Console.WriteLine(test.Username);
            //var opt = new ReceiverOptions
            //{
            //    AllowedUpdates = { } // receive all update types
            //};
            //BotClient.StartReceiving(UpdateHandler, HandleErrorAsync, opt, cts.Token);

            //Console.WriteLine("Hello World BOT!");
            //Console.ReadLine();


            Parser();
        }


        private static async Task UpdateHandler(ITelegramBotClient arg1,
            Telegram.Bot.Types.Update arg2,
            CancellationToken arg3)
        {
            long userId = -1;
            if (arg2.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                userId = arg2.Message.From.Id;
            else if (arg2.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                userId = arg2.CallbackQuery.From.Id;
            }

            if (users.HasUser(userId))
                users.GetUser(userId).State.UpdateHandler(arg1, arg2);
            else
                users.AddUser(userId, arg2.Message?.From.Username).State.UpdateHandler(arg1, arg2);

        }

        private static async Task HandleErrorAsync(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            Console.WriteLine($"беда {arg2.Message}");
        }


        public static void Parser()
        { 
            //var proxy = new WebProxy("127.0.0.1:8888");
            var cookieContainer = new CookieContainer();

            var postRequest = new PostRequest("https://market.yandex.ru/");
            postRequest.Data = $"catalog--videokarty/26912670/list?text=видеокарта%203080%20ti&cpa=1&hid=91031&srnum=913&rs=eJwz4ghgrGLhmH6cFQALNQJ1&was_redir=1&rt=9&onstock=0&local-offers-first=0";
            postRequest.Accept = "*/*";
            postRequest.Useragent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.84 Safari/537.36 OPR/85.0.4341.72";
            postRequest.ContentType = "application/x-www-form-urlencoded";
            postRequest.Referer = "https://market.yandex.ru/";
            postRequest.Host = "market.yandex.ru";
            //postRequest.Proxy = proxy;

            //postRequest.Headers.Add("Bx-ajax", "true");
            //postRequest.Headers.Add("Origin", "https://baucenter.ru");
            //postRequest.Headers.Add("sec-ch-ua", "\"Chromium\";v=\"92\", \" Not A;Brand\";v=\"99\", \"Google Chrome\";v=\"92\"");
            //postRequest.Headers.Add("sec-ch-ua-mobile", "?0");
            //postRequest.Headers.Add("Sec-Fetch-Dest", "empty");
            //postRequest.Headers.Add("Sec-Fetch-Mode", "cors");
            //postRequest.Headers.Add("Sec-Fetch-Site", "same-origin");

            postRequest.Run(cookieContainer);
            var a = postRequest.Response;

            var strStart = postRequest.Response.IndexOf("search-result-group search-result-product");
            strStart = postRequest.Response.IndexOf("<a href=", strStart) + 9;

            var strEnd = postRequest.Response.IndexOf("\"", strStart);
            var getPath = postRequest.Response.Substring(strStart, strEnd - strStart);

            Console.WriteLine($"getPath={getPath}");

            var getRequest = new GetRequest($"https://baucenter.ru{getPath}");
            getRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            getRequest.Useragent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36";
            getRequest.Referer = "https://baucenter.ru/";
            //getRequest.Headers.Add("sec-ch-ua", "\"Chromium\";v=\"92\", \" Not A;Brand\";v=\"99\", \"Google Chrome\";v=\"92\"");
            //getRequest.Headers.Add("sec-ch-ua-mobile", "?0");
            //getRequest.Headers.Add("Sec-Fetch-Dest", "document");
            //getRequest.Headers.Add("Sec-Fetch-Mode", "navigate");
            //getRequest.Headers.Add("Sec-Fetch-Site", "same-origin");
            //getRequest.Headers.Add("Upgrade-Insecure-Requests", "1");
            getRequest.Host = "baucenter.ru";
            //getRequest.Proxy = proxy;
            getRequest.Run(cookieContainer);

            Console.ReadKey();
        }
    }
}
