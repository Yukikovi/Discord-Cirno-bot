using ConsoleApp1.Entites;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Commands
{
    public class FunCommands : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService commands { get; set; }
        private CommandHandler _handler;

        public FunCommands(CommandHandler handler) {
            _handler = handler;
        }
        [SlashCommand("ping", "Gets some stats about the bot and it’s online availability.")]
        public async Task Ping()
        {
            await RespondAsync($"Pong!");
        }
    }
}
