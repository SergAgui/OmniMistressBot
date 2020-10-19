using DSharpPlus;
using System;
using System.Threading.Tasks;

namespace OmniMistressBot
{
    class Program
    {
        static DiscordClient discord;
        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        static async Task MainAsync(string[] args)
        {
            discord = new DiscordClient(new DiscordConfiguration
            {
                //Regenerated Token
                Token = "",
                TokenType = TokenType.Bot
            });
            
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
