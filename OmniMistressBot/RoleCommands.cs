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
        [RequireOwner]
        [Command("upgraderole"), Aliases("ur"), Description("Upgrade a user to any role [!ur @{user} {role}]")]
        [Hidden]
        public async Task UpgradeRole(CommandContext context, DiscordMember member, string role)
        {
            //ReadOnlyList of roles in Guild to string list of names
            List<string> guildRoleList = new List<string>();
            var guildRoles = context.Guild.Roles;
            foreach (var item in guildRoles)
            {
                guildRoleList.Add(item.Name);
            }

            //ReadOnlyList of roles user is part of to string list
            List<string> userRoleList = new List<string>();
            var userRoles = context.Member.Roles;
            foreach (var item in userRoles)
            {
                userRoleList.Add(item.Name);
            }

            //check if role exists in server, @user isn't a bot, and @user isn't already in role
            if (member.IsBot != true && guildRoleList.Exists(r => r == role) && userRoleList.Exists(u => u == role) == false)
            {
                var upgradeRole = context.Guild.Roles.FirstOrDefault(x => x.Name == role);
                await member.GrantRoleAsync(upgradeRole);
                await context.RespondAsync($"Updated to {upgradeRole.Name}");
            }
            else
            {
                await context.RespondAsync($"Couldn't complete. Either the {role} role does not exist in this server and bot's aren't people or {member.Username} is already part of that role.");
            }
        }
        [RequireOwner]
        [Command("downgraderole"), Aliases("dr", "downgrade"), Description("Take away a role from a user [!dr @{user} {role}]")]
        [Hidden]
        public async Task DowngradeRole(CommandContext context, DiscordMember member, string role)
        {
            //ReadOnlyList of roles in Guild to string list of names
            List<string> guildRoleList = new List<string>();
            var guildRoles = context.Guild.Roles;
            foreach (var item in guildRoles)
            {
                guildRoleList.Add(item.Name);
            }

            //ReadOnlyList of roles user is part of to string list
            List<string> userRoleList = new List<string>();
            var userRoles = context.Member.Roles;
            foreach (var item in userRoles)
            {
                userRoleList.Add(item.Name);
            }

            //check if role exists in server, @user isn't a bot, and @user is in role
            if (member.IsBot != true && guildRoleList.Exists(r => r == role) && userRoleList.Exists(u => u == role))
            {
                var takenRole = context.Guild.Roles.FirstOrDefault(x => x.Name == role);
                await member.RevokeRoleAsync(takenRole);
                await context.RespondAsync($"{member.Username} was removed from the {takenRole.Name} role");
            }
            else
            {
                await context.RespondAsync($"Couldn't complete. Either the {role} role does not exist in this server and bot's aren't people or {member.Username} doesn't belong to that role.");
            }
        }
    }
}
