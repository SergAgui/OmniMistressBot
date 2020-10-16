using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OmniMistressBot
{
    class Program
    {
        static DiscordClient discord;
        static CommandsNextExtension commands;
        static InteractivityExtension interactivity;
        
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
                MinimumLogLevel = LogLevel.Debug,
                AutoReconnect = true
            });

            //Log if client is ready, guild is available, and if client errored
            discord.Ready += Discord_Ready;
            discord.GuildAvailable += Discord_GuildAvailable;
            discord.ClientErrored += Discord_ClientErrored;

            //Defaults: pagination behaviour, pagination timeout, timeout for other actions
            interactivity = discord.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(5)
            });

            string[] prefixes = new string[] {"!", "."};
            //Set prefix to commands
            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = prefixes,
                IgnoreExtraArguments = true,
                CaseSensitive = false,
                EnableDms = false,
                EnableMentionPrefix = true
            });

            //Log command executed and if a command errored
            commands.CommandExecuted += Commands_CommandExecuted;
            commands.CommandErrored += Commands_CommandErrored;

            //Command classes in use
            commands.RegisterCommands<InteractiveCommands>();
            commands.RegisterCommands<DiceRolls>();
            commands.RegisterCommands<RoleCommands>();
            commands.RegisterCommands<MemeCommands>();
            commands.RegisterCommands<Lavalink>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task Discord_Ready(DSharpPlus.EventArgs.ReadyEventArgs e)
        {
            e.Client.Logger.Log(LogLevel.Information, "MemeJam", "Ready = Yes", DateTime.Now);
            return Task.CompletedTask;
        }
        private static Task Discord_GuildAvailable(DSharpPlus.EventArgs.GuildCreateEventArgs e)
        {
            e.Client.Logger.Log(LogLevel.Information, "MemeJam", $"Guild available: {e.Guild.Name}", DateTime.Now);
            return Task.CompletedTask;
        }
        private static Task Discord_ClientErrored(DSharpPlus.EventArgs.ClientErrorEventArgs e)
        {
            e.Client.Logger.Log(LogLevel.Error, "MemeJame", $"Exception found: {e.Exception.GetType()}", DateTime.Now);
            return Task.CompletedTask;
        }
        private static async Task Commands_CommandErrored(CommandErrorEventArgs e)
        {
            e.Context.Client.Logger.Log(LogLevel.Error, "MemeJam", $"{e.Context.User.Username} tried executing '{e.Command?.QualifiedName ?? "<unknown command>"}' but it errored: {e.Exception.GetType()}: {e.Exception.Message ?? "<no message>"}", DateTime.Now);
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
            e.Context.Client.Logger.Log(LogLevel.Information, "MemeJame", $"{e.Context.User.Username} successfully executed '{e.Command.QualifiedName}", DateTime.Now);
            return Task.CompletedTask;
        }
    }
}
