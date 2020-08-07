using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OmniMistressBot
{
    class MusicCommands
    {
        [RequireOwner]
        [Command("joinvoice"), Aliases("jv"), Description("Allows bot to join a specified voice channel. If no channel is given then will default to channel user is currently in.")]
        public async Task Join(CommandContext context, DiscordChannel channel = null)
        {
            //Check if VoiceNext is configured or enabled
            var voiceNext = context.Client.GetVoiceNextClient();
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
            vconnected = await voiceNext.ConnectAsync(channel);
        }

        [RequireOwner]
        [Command("leave"), Aliases("dcv", "disconnect"), Description("Disconnects voice")]
        public async Task Leave(CommandContext context)
        {
            //Check if VoiceNext is configured or enabled
            var voiceNext = context.Client.GetVoiceNextClient();
            if (voiceNext == null)
            {
                await context.RespondAsync("Please enabled or configure VoiceNext");
                return;
            }

            //Check if not connected to the server
            //Returning null atm, needs fix
            var vconnected = voiceNext.GetConnection(context.Guild);
            if (vconnected == null)
            {
                await context.RespondAsync("Not connected to this server");
                return;
            }

            //Disconnect
            await context.RespondAsync("Successfully Disconnected");
            vconnected.Disconnect();
        }

        public async Task Play(CommandContext context, [RemainingText] string filename)
        {
            //Check if VoiceNext is configured or enabled
            var voiceNext = context.Client.GetVoiceNextClient();
            if (voiceNext == null)
            {
                await context.RespondAsync("Please enabled or configure VoiceNext");
                return;
            }

            //Check if not connected to the server

            var vconnected = voiceNext.GetConnection(context.Guild);
            if (vconnected == null)
            {
                await context.RespondAsync("Not connected to this server");
                return;
            }

            if (!File.Exists(filename))
            {
                await context.RespondAsync($"{filename} does not exist");
                return;
            }

            while (vconnected.IsPlaying)
            {
                await vconnected.WaitForPlaybackFinishAsync();
            }
        }
    }
}
