using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
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
        [Command("upgraderole"), Aliases("ur"), Description("Upgrade a user to any role [!ur @{user} {role}]")]
        [Hidden]
        public async Task UpgradeRole(CommandContext context, DiscordMember member, string role)
        {
            //ReadOnlyList to just names
            List<string> roleList = new List<string>();
            var check = context.Guild.Roles;
            foreach (var item in check)
            {
                roleList.Add(item.Name);
            }

            //check if role exists in server
            if (member.IsBot != true && roleList.Exists(r => r == role))
            {
                var upgradeRole = context.Guild.Roles.FirstOrDefault(x => x.Name == role);
                await member.GrantRoleAsync(upgradeRole);
                await context.RespondAsync($"Updated to {upgradeRole.Name}");
            }
            else
            {
                await context.RespondAsync($"Couldn't complete. The {role} role does not exist in this server.");
            }
        }

        /*public async Task DowngradeRole(CommandContext context, DiscordMember member, string role)
        {

        }*/
    }
}
