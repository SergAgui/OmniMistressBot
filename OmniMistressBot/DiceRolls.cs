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
    public class DiceRolls
    {
        [Command("rolld4"), Description("Rolls a d4 die")]
        public async Task Roll4(CommandContext context)
        {
            await context.TriggerTypingAsync();

            Random random = new Random();
            int die = random.Next(1, 4);
            await context.RespondAsync($"Rolled a {die}");
        }
        [Command("rolld6"), Description("Rolls a d6 die")]
        public async Task Roll6(CommandContext context)
        {
            await context.TriggerTypingAsync();

            Random random = new Random();
            int die = random.Next(1, 6);
            await context.RespondAsync($"Rolled a {die}");
        }
        [Command("rolld8"), Description("Rolls a d8 die")]
        public async Task Roll8(CommandContext context)
        {
            await context.TriggerTypingAsync();

            Random random = new Random();
            int die = random.Next(1, 8);
            await context.RespondAsync($"Rolled a {die}");
        }
        [Command("rolld10"), Description("Rolls a d10 die")]
        public async Task Roll10(CommandContext context)
        {
            await context.TriggerTypingAsync();

            Random random = new Random();
            int die = random.Next(1, 10);
            await context.RespondAsync($"Rolled a {die}");
        }
        [Command("rolld12"), Description("Rolls a d12 die")]
        public async Task Roll12(CommandContext context)
        {
            await context.TriggerTypingAsync();

            Random random = new Random();
            int die = random.Next(1, 12);
            await context.RespondAsync($"Rolled a {die}");
        }
        [Command("rolld20"), Description("Rolls a d20 die")]
        public async Task Roll20(CommandContext context)
        {
            await context.TriggerTypingAsync();

            Random random = new Random();
            int die = random.Next(1, 20);
            await context.RespondAsync($"Rolled a {die}");
        }
        [Command("rolld50"), Description("Rolls a d50 die")]
        public async Task Roll50(CommandContext context)
        {
            await context.TriggerTypingAsync();

            Random random = new Random();
            int die = random.Next(1, 50);
            await context.RespondAsync($"Rolled a {die}");
        }
        [Command("rolld100"), Description("Rolls a d100 die")]
        public async Task Roll100(CommandContext context)
        {
            await context.TriggerTypingAsync();

            Random random = new Random();
            int die = random.Next(1, 100);
            await context.RespondAsync($"Rolled a {die}");
        }
    }
}
