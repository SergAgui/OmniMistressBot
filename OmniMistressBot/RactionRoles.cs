using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace OmniMistressBot
{
    class RactionRoles
    {
        [Command("newroll"), Aliases("nr"), Description("Create a new roll. [!newrole {RoleName} {Color} {Permissions (can be left blank)} {Mentionable (can be left blank} {Reason (can be left blank)}]")]
        public async Task CreateRoll(CommandContext context, string name, DiscordColor color, Permissions? permissions = null, bool mentionable = true, string reason = null)
        {
            InteractiveCommands interactiveCommands = new InteractiveCommands();
            await context.Guild.CreateRoleAsync(name, permissions, color,null,mentionable, null);
            
            await context.RespondAsync($"Role {name} has been created. Color = {color} | Mentionable = {mentionable} | Permissions (if any) = {permissions}");
        }

        [Command("rolecolors"), Description("Displays possible colors that can be used for Roles")]
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
