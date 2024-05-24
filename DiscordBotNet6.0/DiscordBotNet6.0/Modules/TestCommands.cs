using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Config;
using ConsoleApp1.Entites;
using Newtonsoft.Json;
using Discord.Interactions;

namespace DiscordBotNet6._0.Modules
{
    public class InfoCommands : ModuleBase<SocketCommandContext>
    {
        public InteractionService commands { get; set; }
        CommandHandler Handler { get; set; }
        public InfoCommands(CommandHandler handler)
        {
            Handler = handler;
        }



    }
}
