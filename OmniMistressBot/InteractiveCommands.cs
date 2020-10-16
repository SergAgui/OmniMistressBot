using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;

namespace OmniMistressBot
{
    public class InteractiveCommands : BaseCommandModule
    {
        [Command("coderace"), Description("First to respond with generated code wins")]
        public async Task CodeRace(CommandContext context)
        {
            var interactivity = context.Client.GetInteractivity();

            await context.RespondAsync("First to type the code within 30 seconds wins! Ready?");
            for (int i = 3; i >= 0; i--)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                if (i!= 0)
                {
                    await context.RespondAsync($"{i}");
                }
            }
            byte[] codebytes = new byte[4];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(codebytes);
            string code = BitConverter.ToString(codebytes).ToLower().Replace("-", "");
            
            await context.RespondAsync($"GO!! Code: {code}");

            var message = await interactivity.WaitForMessageAsync(c => c.Content.Contains(code), TimeSpan.FromSeconds(30));

            //Below prevents bot from accepting itself stating the code as a response,
            //but may still accept code from self and go to 'else'
            if (message.Result != null && message.Result.Author.IsBot == false)
            {
                await context.RespondAsync($"The winner is: {message.Result.Author.Mention}");
            }
            else
            {
                await context.RespondAsync("No one played?! Shame...");
            }
        }
    }
}
