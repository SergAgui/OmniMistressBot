using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace OmniMistressBot
{
    public class MemeCommands
    {
        [Command("hi"), Aliases("hello", "greeting", "greetings")]
        public async Task Hi(CommandContext context)
        {
            await context.RespondAsync($"👋 Hi, {context.User.Mention}... Does that make you feel good?");
        }
        [Command("pepe"), Aliases("feelsbad"), Description("Feels bad, man")]
        public async Task Pepe(CommandContext context)
        {
            await context.TriggerTypingAsync();

            var embed = new DiscordEmbedBuilder
            {
                Title = "Pepe",
                ImageUrl = "http://i.imgur.com/44SoSqS.jpg"
            };
            await context.RespondAsync(embed: embed);
        }
        [Command("nou"), Description("Reverse insult")]
        public async Task NoU(CommandContext context)
        {
            await context.TriggerTypingAsync();
            var embed = new DiscordEmbedBuilder()
            {
                Title = "No U",
                ImageUrl = "https://media1.tenor.com/images/51d79d265bf6217deff0ab30f033d27b/tenor.gif?itemid=16059691"
            };
            await context.RespondAsync(embed: embed);
        }
        [Command("nut"), Description("Nut Face")]
        public async Task Nut(CommandContext context)
        {
            await context.TriggerTypingAsync();

            var embed = new DiscordEmbedBuilder
            {
                Title = "NUT",
                ImageUrl = "https://yt3.ggpht.com/a/AATXAJzayYdH1djzPQWu-kHG6UOWdc-k31GcHtyF9A=s900-c-k-c0xffffffff-no-rj-mo"
            };
            await context.RespondAsync(embed: embed);
        }
        [Command("vom"), Description("Gross reaction")]
        public async Task Gross(CommandContext context)
        {
            await context.TriggerTypingAsync();

            var embed = new DiscordEmbedBuilder
            {
                Title = "Barf",
                ImageUrl = "https://i.imgur.com/UzM2IUf.gif"
            };
            await context.RespondAsync(embed: embed);
        }
        [Command("givemethechocolate"), Description("Choco Drop")]
        public async Task ChocolateDrop(CommandContext context)
        {
            await context.TriggerTypingAsync();

            var embed = new DiscordEmbedBuilder
            {
                Title = "The Chocolate",
                ImageUrl = "https://i.giphy.com/media/20RluqwU3ewnK/giphy.webp"
            };
            await context.RespondAsync(embed: embed);
        }
        [Command("evilol"), Aliases("evilhue"), Description("Evil laughing duck")]
        public async Task EvilLaugh(CommandContext context)
        {
            var embed = new DiscordEmbedBuilder
            {
                Title = "Evil Laugh",
                ImageUrl = "https://i.gifer.com/DQ.gif"
            };
            await context.RespondAsync(embed: embed);
        }
        [Command("chuckle"), Description("A Sensible Chuckle")]
        public async Task Chuckle(CommandContext context)
        {
            await context.TriggerTypingAsync();

            var embed = new DiscordEmbedBuilder
            {
                Title = "A single lol",
                ImageUrl = "https://cdn.discordapp.com/attachments/550860148176977941/731024866765045801/AwAIbt7.gif"
            };
            await context.RespondAsync(embed: embed);
        }
        [Command("omg"), Description("OMG Reaction")]
        public async Task Omg(CommandContext context)
        {
            await context.TriggerTypingAsync();

            var embed = new DiscordEmbedBuilder
            {
                Title = "OMG",
                ImageUrl = "https://cdn.discordapp.com/attachments/550860148176977941/731025127432781834/VQLGJOL.gif"
            };
            await context.RespondAsync(embed: embed);
        }
    }
}
