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
                Token = "NzMwMTMwNTMwNjYxODI2Nzky.Xwcf5Q.M7vui2XDVXVIJp0kRNu8LydkAEU",
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
            //Log if client is ready, guild is available, and if client errored
            discord.Ready += Discord_Ready;
            discord.GuildAvailable += Discord_GuildAvailable;
            discord.ClientErrored += Discord_ClientErrored;

            //Set prefix to commands
            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = "!",
                EnableDms = true,
                EnableMentionPrefix = true
            });
            commands.CommandExecuted += Commands_CommandExecuted;
            commands.CommandErrored += Commands_CommandErrored;

            //Using MyCommands class to hold potential Commands
            commands.RegisterCommands<MyCommands>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task Discord_Ready(DSharpPlus.EventArgs.ReadyEventArgs e)
        {
            throw new NotImplementedException();
        }
        private static Task Discord_GuildAvailable(DSharpPlus.EventArgs.GuildCreateEventArgs e)
        {
            throw new NotImplementedException();
        }
        private static Task Discord_ClientErrored(DSharpPlus.EventArgs.ClientErrorEventArgs e)
        {
            throw new NotImplementedException();
        }
        private static Task Commands_CommandErrored(CommandErrorEventArgs e)
        {
            throw new NotImplementedException();
        }
        private static Task Commands_CommandExecuted(CommandExecutionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
