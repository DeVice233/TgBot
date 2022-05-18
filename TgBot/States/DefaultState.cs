using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Extensions.Polling;

namespace TgBot.States
{
    public class DefaultState : State
    {
        internal override async Task UpdateHandler(User user, Telegram.Bot.ITelegramBotClient arg1, Update arg2)
        {
            if (arg2.Message == null)
                return;
            if (arg2.Message.Text == "/start")
            {
                await arg1.SendTextMessageAsync(arg2.Message.Chat.Id,
                    "Добро пожаловать в бота поиска товаров Ozon!", Telegram.Bot.Types.Enums.ParseMode.Markdown
                         );

                await arg1.SendTextMessageAsync(arg2.Message.Chat.Id,
                    "Введите название товара", Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: new ReplyKeyboardRemove()
                         );

                user.State.SetState(new MainMenuState()); // тут указываем класс-обработчик новых команд, таких классов может быть дофига
            }
            if (arg2.Message.Text == "Искать товар")
            {

                await arg1.SendTextMessageAsync(arg2.Message.Chat.Id,
                  "Введите название товара", Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: new ReplyKeyboardRemove()
                       );
                user.State.SetState(new MainMenuState());
            }
        }
    }
}
