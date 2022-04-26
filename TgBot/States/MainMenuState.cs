using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.Helper;

namespace TgBot.States
{
    internal class MainMenuState : State
    {
        internal override async Task UpdateHandler(User user, Telegram.Bot.ITelegramBotClient arg1, Update arg2)
        {
            if (arg2.Message == null)
                return;

            var url = GetRequest.GetUrl(arg2.Message.Text);
            var page = GetRequest.GetPage(url);
            List<Product> products = GetRequest.ParseCard(page);
           
            foreach (var product in products)
            {
                product.Title = product.Title.Replace("&#x2F;", "/");
                product.Title = product.Title.Replace("&#34;", "\"");
            }

            ReplyKeyboardMarkup replyKeyboardMarkup = new(
                   new[]{
                        new KeyboardButton(text:"Искать товар"),
                   });

            await arg1.SendTextMessageAsync(arg2.Message.Chat.Id,
                GenerateMessage(products),
                Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: replyKeyboardMarkup
                    );

            user.State.SetState(new DefaultState());
            await Task.CompletedTask; // заглушка
        }

        public string GenerateMessage(List<Product> products)
        {
            string message = "";
            int i = 1;
            foreach (var product in products)
            {
                message += i.ToString() + ") " + product.Title + " - " + product.Price + "\n" + "\n";
                i++;
            }
            return message;
        }
    }
}
