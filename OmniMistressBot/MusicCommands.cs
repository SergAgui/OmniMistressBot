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
            var voiceNext = context.Client.GetVoiceNextClient();
            if (voiceNext == null)
            {
                await context.RespondAsync("Please enabled or configure VoiceNext");
                return;
            }

            var vconnected = voiceNext.GetConnection(context.Guild);
            if (vconnected != null)
            {
                await context.RespondAsync("Already connected to this server");
                return;
            }

            var vstatus = context.Member?.VoiceState;
            if (vstatus?.Channel == null && channel == null)
            {
                await context.RespondAsync("Please either define a channel or join one first.");
                return;
            }

            if (channel != null)
            {
                channel = vstatus.Channel;
            }

            await voiceNext.ConnectAsync(channel);
            await context.RespondAsync($"Connected to {channel.Name}");
        }

        [RequireOwner]
        [Command("leave"), Aliases("dcv"), Description("Disconnects voice")]
        public async Task Leave(CommandContext context)
        {
            var voiceNext = context.Client.GetVoiceNextClient();
            if (voiceNext == null)
            {
                await context.RespondAsync("Please enabled or configure VoiceNext");
                return;
            }

            var vconnected = voiceNext.GetConnection(context.Guild);
            if (vconnected == null)
            {
                await context.RespondAsync("Not connected to this server");
                return;
            }

            vconnected.Disconnect();
            await context.RespondAsync("Successfully Disconnected");
        }
    }
}
