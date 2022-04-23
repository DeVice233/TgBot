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
            Product product = GetRequest.ParseCard(page);

            product.Title = product.Title.Replace("&#x2F;", "/");

            await arg1.SendTextMessageAsync(arg2.Message.Chat.Id,
                  product.Title + "\n" + product.Price, Telegram.Bot.Types.Enums.ParseMode.Markdown
                       );

            user.State.SetState(new DefaultState());
            await Task.CompletedTask; // заглушка
        }
    }
}
