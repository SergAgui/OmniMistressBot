using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;

namespace OmniMistressBot
{
    class RactionRoles
    {
        public async Task CreateRoll(CommandContext context, string name, DiscordColor color, Permissions? permissions = null, bool mentionable = true, string reason = null)
        {
            InteractiveCommands interactiveCommands = new InteractiveCommands();
            await context.Guild.CreateRoleAsync(name, permissions, color,null,mentionable, null);
            
            await context.RespondAsync($"Role {name} has been created. Color = {color} | Mentionable = {mentionable} | Permissions (if any) = {permissions}");
        }

        public async Task Colors(CommandContext context)
        {
            await context.TriggerTypingAsync();

            var embed = new DiscordEmbedBuilder
            {
                Title = "Possible Role Colors:",
                ImageUrl = "https://imgur.com/4Hfn9Zt"
            };

            await context.RespondAsync(embed: embed);
        }
    }
}
