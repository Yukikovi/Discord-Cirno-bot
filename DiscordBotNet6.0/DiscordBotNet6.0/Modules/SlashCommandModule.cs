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
using ConsoleApp1.Config;
using Newtonsoft.Json;

namespace ConsoleApp1.Commands
{
    public class SlashCommandModule : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService commands { get; set; }
        private CommandHandler _handler;
        public SlashCommandModule(CommandHandler handler)
        {
            _handler = handler;
        }

        [SlashCommand("echo", "echo an input")]
        public async Task PingSlash(string input)
        {
            await RespondAsync(input);
        }
        [SlashCommand("8ball", "find your answer!")]
        public async Task EightBall(string question)
        {
            // create a list of possible replies
            var replies = new List<string>
            {
                // add our possible replies
                "yes",
                "no",
                "maybe",
                "hazzzzy...."
            };

            // get the answer
            var answer = replies[new Random().Next(replies.Count - 1)];

            // reply with the answer
            await RespondAsync($"You asked: [**{question}**], and your answer is: [**{answer}**]");
        }
       
    }
}
