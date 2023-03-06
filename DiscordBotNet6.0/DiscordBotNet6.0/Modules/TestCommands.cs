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
        private OpenAIService gpt3
        {
            get
            {
                return new OpenAIService(new OpenAiOptions()
                {
                    ApiKey = configJsonAI.api
                });
            }
        }
        CommandHandler Handler { get; set; }
        public TestCommands(CommandHandler handler)
        {
            Handler = handler;
            configJsonAI = JsonConvert.DeserializeObject<ConfigJson>(File.ReadAllText("Config\\config.json"));
        }
        private ConfigJson configJsonAI { get; set; }
        [Command("echo")]
        public async Task TestAI(string input)
        {
            var completionResult = await gpt3.Completions.CreateCompletion(new CompletionCreateRequest()
            {
                Prompt = input,
                Model = Models.CodeDavinciV2,
                Temperature = 0.5F,
                MaxTokens = 150
            });
            string outputQuestion = String.Empty;
            if (completionResult.Successful)
            {
                foreach (var choice in completionResult.Choices)
                {
                    outputQuestion = choice.Text.ToString();
                }
                await ReplyAsync(outputQuestion);
                //await RespondAsync(outputQuestion);
            }
            else
            {
                await ReplyAsync($"{completionResult.Error.Code}: {completionResult.Error.Message}");
                Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
            }
        }
    }
}
