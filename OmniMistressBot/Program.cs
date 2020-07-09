using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.Threading.Tasks;

namespace OmniMistressBot
{
    class Program
    {
        static DiscordClient discord;
        static CommandsNextModule commands;
        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        static async Task MainAsync(string[] args)
        {
            discord = new DiscordClient(new DiscordConfiguration
            {
                //Configuration of Discord Client
                Token = "",
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });
            //Respond to "ping" with "pong"
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("ping"))
                    await e.Message.RespondAsync(":ping_pong:");
            };
            //Set prefix to commands
            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = "!"
            });
            //Using MyCommands class to hold potential Commands
            commands.RegisterCommands<MyCommands>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
