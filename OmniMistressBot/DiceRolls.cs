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
    }
}
