using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using ConsoleApp1.Config;
using Newtonsoft.Json;

namespace DiscordBotNet6._0.Factory
{
    public class GitFactory
    {
        private List<GitApiConfig> gitProfile { get; set; }
        public GitFactory() { }

        public List<Release> GetVersionProject()
        {

        }

        private void GetGitProfile()
        {
            var iJson = JsonConvert.DeserializeObject<GitApiConfig>(File.ReadAllText("Factory\\gitconfig.json"));
            gitProfile.Add(iJson);
        }
    }
    #region --структуры листа--
    public struct Release
    {
        public string accept;
        public string owner;
        public string repo;
        public string tag;
    }
    public struct GitApiConfig
    {
        public string username;
        public string password;
    }
    #endregion
}
