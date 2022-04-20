using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgBot.States;

namespace TgBot
{
   public class User
    {
        public long Id { get; internal set; }
        public string UserName { get; internal set; }

        public StateMachine State { get; set; }

        public User()
        {
            State = new StateMachine(this, new DefaultState());
        }
    }
}
