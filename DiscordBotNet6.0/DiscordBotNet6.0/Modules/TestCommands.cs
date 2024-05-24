using Discord.Commands;
using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Config;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3;
using ConsoleApp1.Entites;
using Newtonsoft.Json;

namespace DiscordBotNet6._0.Modules
{
    public class TestCommands : ModuleBase<SocketCommandContext>
    {
      
        CommandHandler Handler { get; set; }
        public TestCommands(CommandHandler handler)
        {
            Handler = handler;

        }

    }
}
