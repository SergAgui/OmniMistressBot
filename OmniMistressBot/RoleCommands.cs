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
    [RequireUserPermissions(Permissions.Administrator)]
    class RoleCommands
    {
        //Create a new role
        [Command("newroll"), Aliases("nr"), Description("Create a new roll. [!newrole {RoleName} {Color (hex code without the #)} {Mentionable (can be left blank)} {Reason (can be left blank)}]")]
        public async Task CreateRoll(CommandContext context, string roleName, string color, bool mentionable = true, string reason = null)
        {
            var discordColor = new DiscordColor(color);

            await context.Guild.CreateRoleAsync(roleName, null, discordColor, null,mentionable, null);
            
            await context.RespondAsync($"Role {roleName} has been created. Color = {color} | Mentionable = {mentionable}");
        }

        //Delete an existing role
        [Command("deleterole"), Aliases("dr"), Description("Delete a specified role. [!deleterole {RoleName} {Reason (can be left blank)}]")]
        public async Task DeleteRole(CommandContext context, string roleName, string reason = null)
        {
            //ReadOnlyList of roles in Guild to string list of names
            var guildRoles = context.Guild.Roles;
            List<string> guildRoleList = guildRoles.Select(item => item.Name).ToList();

            //Reason for deleting role
            string givenReason = reason == null ? "None given" : reason;

            //Check if role exists in server, respond with error if not
            if (guildRoleList.Exists(r => r == roleName))
            {
                var discordRole = context.Guild.Roles.FirstOrDefault(x => x.Name == roleName);
                await context.Guild.DeleteRoleAsync(discordRole, reason);
                await context.RespondAsync($"{discordRole.Name} has been deleted. Reason: {givenReason}");
            }
            else
            {
                await context.RespondAsync($"Could not complete deletion. Either the {roleName} role does not exist in this server or role was misspelled. Roles are case sensitive.");
            }
        }

        //Assign a role to a member
        [Command("giverole"), Aliases("gr"), Description("Owner can assign a user to any role [!ur @{user} {role}]")]
        public async Task UpgradeRole(CommandContext context, DiscordMember member, string role)
        {
            //ReadOnlyList of roles in Guild to string list of names
            var guildRoles = context.Guild.Roles;
            List<string> guildRoleList = guildRoles.Select(item => item.Name).ToList();

            //ReadOnlyList of roles user is part of to string list
            var userRoles = context.Member.Roles;
            List<string> userRoleList = userRoles.Select(item => item.Name).ToList();

            //check if role exists in server, @user isn't a bot, and @user isn't already in role
            if (member.IsBot != true && guildRoleList.Exists(r => r == role) && userRoleList.Exists(u => u == role) == false)
            {
                var upgradeRole = context.Guild.Roles.FirstOrDefault(x => x.Name == role);
                await member.GrantRoleAsync(upgradeRole);
                await context.RespondAsync($"{member.Username} has been the role {upgradeRole.Name}");
            }
            else
            {
                await context.RespondAsync($"Couldn't complete. Either the {role} role does not exist in this server and bot's aren't people or {member.Username} is already part of that role.");
            }
        }

        //Remove role from member
        [Command("takerole"), Aliases("tr", "removerole"), Description("Take away a role from a user [!dr @{user} {role}]")]
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
