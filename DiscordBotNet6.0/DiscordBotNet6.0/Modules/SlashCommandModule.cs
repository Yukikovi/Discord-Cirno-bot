using ConsoleApp1.Entites;
using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
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
        private OpenAIService gpt3 { get {
                return new OpenAIService(new OpenAiOptions()
                {
                    ApiKey = configJsonAI.api
                });
            } }
        private ConfigJson configJsonAI { get; set; }
        public SlashCommandModule(CommandHandler handler)
        {
            _handler = handler;
            configJsonAI = JsonConvert.DeserializeObject<ConfigJson>(File.ReadAllText("Config\\config.json"));
        }

        [SlashCommand("echo", "echo an input")]
        public async Task PingSlash(string input)
        {
            //await RespondAsync($"{input}");
            var completionResult = await gpt3.Completions.CreateCompletion(new CompletionCreateRequest()
            {
                Prompt = input,
                Model = Models.TextDavinciV3,
                Temperature = 0.5F,
                MaxTokens = 1500
            });
            string outputQuestion = String.Empty;
            if (completionResult.Successful)
            {
                foreach (var choice in completionResult.Choices)
                {
                    outputQuestion += choice.Text;
                }
                await ReplyAsync($"Question:{input}.\n" +
                    $"{outputQuestion}");
                //await RespondAsync(outputQuestion);
            }
            else
            {
                await RespondAsync($"{completionResult.Error.Code}: {completionResult.Error.Message}");
                Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
            }
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
        //[SlashCommand("testAI", "question to ai")]
        //public async Task testAI(string question)
        //{
        //    var completionResult = await gpt3.Completions.CreateCompletion(new CompletionCreateRequest()
        //    {
        //        Prompt = question,
        //        Model = Models.TextDavinciV2,
        //        Temperature = 0.5F,
        //        MaxTokens = 100
        //    });
        //    string outputQuestion = String.Empty;
        //    if (completionResult.Successful)
        //    {
        //        foreach (var choice in completionResult.Choices)
        //        {
        //            outputQuestion += choice.ToString();
        //        }
        //        await RespondAsync(outputQuestion);
        //    }
        //    else
        //    {
        //        await RespondAsync($"{completionResult.Error.Code}: {completionResult.Error.Message}");
        //        Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
        //    }
        //}
    }
}
