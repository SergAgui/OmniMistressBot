using System;
using System.Collections.Generic;
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
        [Command("roleone"), Aliases("r1"), Description("Update to Role1")]
        [Hidden]
        public async Task AllRoles(CommandContext context)
        {
            var role1 = context.Guild.Roles.FirstOrDefault(x => x.Name == "Role1");
            await context.Member.GrantRoleAsync(role1);
            await context.RespondAsync($"Updated to {role1.Name}");
        }
    }
}
