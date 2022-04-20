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
                InlineKeyboardMarkup replyKeyboardMarkup = new(
                    new[]{
                        InlineKeyboardButton.WithCallbackData(text: "1.1", callbackData: "main_state1"),
                        InlineKeyboardButton.WithCallbackData(text: "1.2", callbackData: "main_state2"),
                    });

                // меняем интерфейс
                await arg1.SendTextMessageAsync(arg2.Message.Chat.Id,
                    "Привет",
                    Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    replyMarkup: replyKeyboardMarkup);

                user.State.SetState(new MainMenuState()); // тут указываем класс-обработчик новых команд, таких классов может быть дофига
            }
        }
    }
}
