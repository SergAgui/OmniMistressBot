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
        public async Task DiceRoll(CommandContext context, string dice)
        {
            await context.TriggerTypingAsync();
            
            //Spit string, ID values, and create array the size of the amount of dice being rolled
            string[] amtSize = dice.Split('d');
            int amountOfDice = Convert.ToInt32(amtSize[0]);
            string[] rolls = new string[amountOfDice];
            
            //Roll any size dice for 'i' times
            Random random = new Random();
            for (int i = 0; i < amountOfDice; i++)
            {
                rolls[i] = Convert.ToString(random.Next(1, Convert.ToInt32(amtSize[1])));
            }

            int sum = Array.ConvertAll(rolls, r => Convert.ToInt32(r)).Sum();

            await context.RespondAsync($"Rolls: [{String.Join(", ", rolls)}] Sum: {sum}");
        }
        
        [Command("rolloff"), Aliases("rc", "ro"), Description("Challenge another user to a roll off (ex. !rolloff @username)")]
        public async Task RollOff(CommandContext context, DiscordUser user)
        {
            InteractivityModule interactivity = context.Client.GetInteractivityModule();
            await context.TriggerTypingAsync();

            //Yes and No emojis
            DiscordEmoji yes = DiscordEmoji.FromName(context.Client, ":+1:");
            DiscordEmoji nope = DiscordEmoji.FromName(context.Client, ":-1:");
            var embed = new DiscordEmbedBuilder
            {
                Title = $"{context.Message.Author.Username} has challenged {user.Username} to a Roll Off! Do you accept?",
                Description = "React with Yes :+1: || No :-1:"
            };
            await context.RespondAsync(embed: embed);

            var agreeEmbed = new DiscordEmbedBuilder
            {
                Title = $"{user.Username} has agreed! Roll Off begins!!",
                Description = "Rolling..."
            };

            //Starts the roll off and announces the victor unless there is a tie
            //Maybe... Refactor
            var emote = await interactivity.WaitForReactionAsync(e => e == e.Name, user, TimeSpan.FromSeconds(30));
            if (emote.Emoji.Name == yes)
            {
                await context.RespondAsync(embed: agreeEmbed);
                await context.TriggerTypingAsync();
                Random rand = new Random();
                int rollOne = rand.Next(1, 100);
                int rollTwo = rand.Next(1, 100);
                if (rollOne > rollTwo)
                {
                    await context.RespondAsync($"Looks like {context.Message.Author.Username} rolled a {rollOne} against {user.Username}'s {rollTwo} and won!");
                }
                else if (rollTwo > rollOne)
                {
                    await context.RespondAsync($"{user.Username} rolled a {rollTwo} to {context.Message.Author.Username}'s {rollOne}! Better luck next time!");
                }
                else
                {
                    await context.RespondAsync($"There was a tie roll of {rollOne}.");
                }
            }
            else if (emote.Emoji.Name == nope || emote == null)
            {
                await context.RespondAsync("Tough cookies :scream: They said NOPE!!");
            }
        }
         
    }
}
