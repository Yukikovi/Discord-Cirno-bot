﻿using ConsoleApp1.Entites;
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

namespace DiscordBotNet6._0.Modules
{
    public class InfoCommands : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService commands { get; set; }
        private CommandHandler Handler { get; set; }
        public InfoCommands(CommandHandler handler)
        {
            Handler = handler;
        }
        [SlashCommand("list-roles", "List of server user roles")]
        public async Task ListRoles(SocketGuildUser user)
        {
            var rolelist = string.Join(",\n", user.Roles.Where(x => !x.IsEveryone).Select(x => x.Mention));

            var embedBuilder = new EmbedBuilder()
                .WithAuthor(user.ToString(), user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
                .WithTitle("Roles")
                .WithDescription(rolelist)
                .WithColor(Color.DarkBlue)
                .WithCurrentTimestamp();

            await RespondAsync(embed: embedBuilder.Build());
        }
        [SlashCommand("bot-status", "The life cirno")]
        public async Task InfoTask(SocketSelfUser user)
        {
            var embedBuilder = new EmbedBuilder()
                .WithAuthor(user.Username.ToString(), user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
                .WithTitle("Status")
                .WithDescription($"")
        }
    }
}
