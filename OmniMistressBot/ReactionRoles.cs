using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace OmniMistressBot
{
    class ReactionRoles
    {
        [Command("newroll"), Aliases("nr"), Description("Create a new roll. [!newrole {RoleName} {Color (hex code without the #)} {Permissions (can be left blank)} {Mentionable (can be left blank)} {Reason (can be left blank)}]")]
        public async Task CreateRoll(CommandContext context, string name, string color, Permissions? permissions = null, bool mentionable = true, string reason = null)
        {
            var discordColor = new DiscordColor(color);

            //Figure permissions next

            await context.Guild.CreateRoleAsync(name, permissions, discordColor, null,mentionable, null);
            
            await context.RespondAsync($"Role {name} has been created. Color = {color} | Mentionable = {mentionable} | Permissions (if any) = {permissions}");
        }
    }
}
