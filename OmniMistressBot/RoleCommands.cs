using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;

namespace OmniMistressBot
{
    class RoleCommands
    {
        [Command("rolefacts"), Aliases("rf"), Description("Check facts about a specific role [!rc role]")]
        [Hidden]
        public async Task RoleFacts(CommandContext context, DiscordRole role)
        {
            var embed = new DiscordEmbedBuilder
            {
                Title = $"{role.Name} Facts:",
                Description = $"Color: {Convert.ToString(role.Color)}, " +
                $"IsHoisted?: {Convert.ToString(role.IsHoisted)}, " +
                $"Permissions: {Convert.ToString(role.Permissions)}, " +
                $"Position: {Convert.ToString(role.Position)}"
            };
            var member = context.Member;
            await context.Guild.GrantRoleAsync(member,role);
            await context.RespondAsync(embed: embed);
        }
    }
}
