using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;


namespace OmniMistressBot
{
    public class DiceRolls
    {
        [Command("roll"), Aliases("r"), Description("Rolls any size die a given number of times")]
        public async Task Roll(CommandContext context, string dice)
        {
            await context.TriggerTypingAsync();

            string[] amtSize = dice.Split('d');
            int amountOfDice = Convert.ToInt32(amtSize[0]);
            string[] rolls = new string[amountOfDice];

            Random random = new Random();
            for (int i = 0; i < amountOfDice; i++)
            {
                rolls[i] = Convert.ToString(random.Next(1, Convert.ToInt32(amtSize[1])));
            }

            int sum = Array.ConvertAll(rolls, r => Convert.ToInt32(r)).Sum();

            await context.RespondAsync($"Rolls: [{String.Join(", ", rolls)}] Sum: {sum}");
        }
        [Command("roleoff"), Aliases("rc", "ro"), Description("Challenge another user to a roll off, highest roll upgrades role")]
        public async Task RoleRoll(CommandContext context, DiscordUser user)
        {
            InteractivityModule interactivity = context.Client.GetInteractivityModule();
            await context.TriggerTypingAsync();

            var embed = new DiscordEmbedBuilder
            {
                Title = $"{context.Message.Author} has challenged {user} to a Role Off! Do you accept?",
                Description = "React with Yes :+1: || No :-1:"
            };
            var response = await context.RespondAsync(embed: embed);
        }
    }
}
