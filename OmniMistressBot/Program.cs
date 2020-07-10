using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
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
            commands.RegisterCommands<MemeCommands>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task Discord_Ready(DSharpPlus.EventArgs.ReadyEventArgs e)
        {
            e.Client.DebugLogger.LogMessage(LogLevel.Info, "Mistress", "Ready = Yes", DateTime.Now);
            return Task.CompletedTask;
        }
        private static Task Discord_GuildAvailable(DSharpPlus.EventArgs.GuildCreateEventArgs e)
        {
            e.Client.DebugLogger.LogMessage(LogLevel.Info, "Mistress", $"Guild available: {e.Guild.Name}", DateTime.Now);
            return Task.CompletedTask;
        }
        private static Task Discord_ClientErrored(DSharpPlus.EventArgs.ClientErrorEventArgs e)
        {
            e.Client.DebugLogger.LogMessage(LogLevel.Error, "Mistress", $"Exception found: {e.Exception.GetType()}", DateTime.Now);
            return Task.CompletedTask;
        }
        private static async Task Commands_CommandErrored(CommandErrorEventArgs e)
        {
            e.Context.Client.DebugLogger.LogMessage(LogLevel.Error, "Mistress", $"{e.Context.User.Username} tried executing '{e.Command?.QualifiedName ?? "<unknown command>"}' but it errored: {e.Exception.GetType()}: {e.Exception.Message ?? "<no message>"}", DateTime.Now);
            if (e.Exception is ChecksFailedException ex)
            {
                var emoji = DiscordEmoji.FromName(e.Context.Client, ":no_entry:");

                var embed = new DiscordEmbedBuilder
                {
                    Title = "Access Denied",
                    Description = $"{emoji} You don't have permission to execute this command",
                    Color = new DiscordColor(0xFF0000)
                };
                await e.Context.RespondAsync("", embed: embed);
            }
        }
        private static Task Commands_CommandExecuted(CommandExecutionEventArgs e)
        {
            e.Context.Client.DebugLogger.LogMessage(LogLevel.Info, "Mistress", $"{e.Context.User.Username} successfully executed '{e.Command.QualifiedName}", DateTime.Now);
            return Task.CompletedTask;
        }
    }
}
