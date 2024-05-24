using Newtonsoft.Json;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotNet6._0.Entites
{
    public class GitApiHandler
    {
        private GitHubClient Client;
        public GitApiHandler() {
            Client = LoadGitHubClient();
        }

        private GitHubClient LoadGitHubClient()
        {
            var iJson = JsonConvert.DeserializeObject<GitApiConfig>(File.ReadAllText("D:\\Program\\Github\\Repos\\DiscordTest\\DiscordBotNet6.0\\DiscordBotNet6.0\\Config\\gitconfig.json"));
            var basicAuth = new Credentials(iJson.token);
            var client = new GitHubClient(new ProductHeaderValue(iJson.username));
            client.Credentials = basicAuth;
            return client;
        }

        public string GetGitVersion()
        {
            var releses = Client.Repository.Release.GetAll("Yukikovi", "Discord-Cirno-bot");
            return releses.Result[0].TagName;
        }
        public string GetGitProjectURL()
        {
            var releses = Client.Repository.Release.GetAll("Yukikovi", "Discord-Cirno-bot");
            return releses.Result[0].HtmlUrl;
        }
        public string GetGitLastUpdate()
        {
            var releses = Client.Repository.Release.GetAll("Yukikovi", "Discord-Cirno-bot");
            return releses.Result[0].PublishedAt.ToString();
        }

    }
    struct GitApiConfig
    {
        public string username { get; set; }
        public string token { get; set; }
    }
}
