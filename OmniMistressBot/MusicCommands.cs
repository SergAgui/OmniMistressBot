using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Net;
using DSharpPlus.Lavalink;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;
using DSharpPlus.VoiceNext.Codec;
using DSharpPlus.VoiceNext.EventArgs;

namespace OmniMistressBot
{
    class MusicCommands : BaseCommandModule
    {
        [RequireOwner]
        [Command("joinvoice"), Aliases("jv"), Description("Allows bot to join a specified voice channel {!jv [channel_name]}. If no channel is given then will default to channel user is currently in.")]
        public async Task Join(CommandContext context, DiscordChannel channel = null)
        {
            //Check if VoiceNext is configured or enabled
            var voiceNext = context.Client.UseVoiceNext();
            if (voiceNext == null)
            {
                await context.RespondAsync("Please enabled or configure VoiceNext");
                return;
            }

            //Check if already connected to the server
            var vconnected = voiceNext.GetConnection(context.Guild);
            if (vconnected != null)
            {
                await context.RespondAsync("Already connected to this server");
                return;
            }

            //Check if a channel was mentioned or if member is not in voice channel
            var vstatus = context.Member?.VoiceState;
            if (vstatus?.Channel == null && channel == null)
            {
                await context.RespondAsync("Please either define a channel or join one first.");
                return;
            }

            //If no channel was mentioned, join voice channel member is in
            if (channel == null)
            {
                channel = vstatus.Channel;
            }

            //Connect
            await context.RespondAsync($"Connected to {channel.Name}");
            await voiceNext.ConnectAsync(channel);
        }

        [RequireOwner]
        [Command("leave"), Aliases("dcv", "disconnect"), Description("Disconnects from voice channel")]
        public async Task Leave(CommandContext context)
        {
            //Check if VoiceNext is configured or enabled
            var voiceNext = context.Client.UseVoiceNext();
            if (voiceNext == null)
            {
                await context.RespondAsync("Please enabled or configure VoiceNext");
                return;
            }

            List<string> states = new List<string>();
            var voiceStates = context.Guild.VoiceStates;
            foreach (var item in voiceStates)
            {
                states.Add(item.ToString());
            }

            //Check if not connected to the server
            //Returning null atm, needs fix
            var vconnected = voiceNext.GetConnection(context.Guild);
            if (vconnected == null)
            {
                await context.RespondAsync($"{voiceStates}");
                await context.RespondAsync($"Not connected to this server");
                return;
            }

            //Disconnect
            vconnected.Disconnect();
            await context.RespondAsync("Successfully Disconnected");
        }
    }
}
