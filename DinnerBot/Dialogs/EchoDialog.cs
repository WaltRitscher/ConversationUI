using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;


namespace DinnerBot.SimpleEchoBot
{
  [Serializable]
  public class EchoDialog : IDialog<object>
  {
    // store state in class variables or properties
    // but doesn't scale across multiple channels or multiple conversations!
    protected int count = 1;

    public async Task StartAsync(IDialogContext context)
    {
      context.Wait(MessageReceivedAsync);
    }

    public async Task MessageReceivedAsync(IDialogContext context,
      IAwaitable<IMessageActivity> messageActivity)
    {
      var message = await messageActivity;

      if (message.Text == "reset")
      {
        PromptDialog.Confirm(
            context,
            AfterResetAsync,
            "Are you sure you want to reset the count?",
            "Didn't get that!",
            promptStyle: PromptStyle.Auto);
      }
      else
      {
        await context.PostAsync($"{this.count++}: You said {message.Text}");
        context.Wait(MessageReceivedAsync);
      }
    }

    public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
    {
      var confirm = await argument;
      if (confirm)
      {
        this.count = 1;
        await context.PostAsync("Reset count.");
      }
      else
      {
        await context.PostAsync("Did not reset count.");
      }
      context.Wait(MessageReceivedAsync);
    }

  }
}