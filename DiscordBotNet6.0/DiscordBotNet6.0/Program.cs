using ConsoleApp1.Config;
using ConsoleApp1.Entites;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using RunMode = Discord.Commands.RunMode;

namespace DiscordBotNet6._0
{
    public class Program
    {
        public static Task Main(string[] args) => new Program().MainAsync();

        public DiscordSocketClient _client { get; set; }
        public CommandService _command { get; set; }
        public InteractionService _interactionService { get; set; }

        private ulong _testGuildId;

        public static bool IsDebugRelease
        {
            get
            {
                return true;
            }
        }

        public async Task MainAsync()
        {
            


            var services = ConfigureServices();
            //get services
            _client = services.GetRequiredService<DiscordSocketClient>();
            _interactionService = services.GetRequiredService<InteractionService>();
            _command = services.GetRequiredService<CommandService>();

            //setup logging and the ready events
            _client.Log += Log;
            _command.Log += Log;
            _interactionService.Log += Log;
            _client.Ready += _client_Ready;

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(File.ReadAllText("Config\\config.json"));

            _testGuildId = configJson.testGuildId;

            await _client.LoginAsync(TokenType.Bot, configJson.Token);
            await _client.StartAsync();

            await services.GetRequiredService<CommandHandler>().InstallCommandsAsync();

            await Task.Delay(Timeout.Infinite);
        }

        private async Task _client_Ready()
        {
            if (IsDebugRelease)
            {
                Console.WriteLine($"In debug mode, adding commands to {_testGuildId}...");
                await _interactionService.RegisterCommandsToGuildAsync(_testGuildId);
                //await _interactionService.RegisterCommandsGloballyAsync(true);
            }
            else
            {
                await _interactionService.RegisterCommandsGloballyAsync(true);
            }
            Console.WriteLine($"Connected as -> [{_client.CurrentUser}] :)");
        }

        private Task Log(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        public ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                {
                    MessageCacheSize = 500,
                    LogLevel = LogSeverity.Info,
                    AlwaysDownloadUsers = true,
                    GatewayIntents = GatewayIntents.MessageContent | GatewayIntents.AllUnprivileged
                }))
                .AddSingleton(new CommandService())
                .AddSingleton<CommandHandler>()
                .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
                .BuildServiceProvider();
        }

    }
}
