using Discord.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Config
{
    public struct ConfigJson
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
        [JsonProperty("testGuild")]
        public ulong testGuildId { get; private set; }
        [JsonProperty("api")]
        public string api { get; private set; }
    }
}
