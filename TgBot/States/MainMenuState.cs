using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TgBot.States
{
    internal class MainMenuState : State
    {
        internal override async Task UpdateHandler(User user, Telegram.Bot.ITelegramBotClient arg1, Update arg2)
        {
            if (arg2.CallbackQuery == null)
                return;

            if (arg2.CallbackQuery.Data == "main_state1")
            {   
                await arg1.SendTextMessageAsync(arg2.CallbackQuery.Message.Chat.Id,
                   "21", Telegram.Bot.Types.Enums.ParseMode.Markdown
                  );
            }
            else if (arg2.CallbackQuery.Data == "main_state2")
            {
                await arg1.SendTextMessageAsync(arg2.CallbackQuery.Message.Chat.Id,
                  "12" ,Telegram.Bot.Types.Enums.ParseMode.Markdown);

            }
            user.State.SetState(new DefaultState());
            await Task.CompletedTask; // заглушка
        }
    }
}
