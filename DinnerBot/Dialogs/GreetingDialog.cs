using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace DinnerBot.Dialogs
{
	[Serializable]
	public class GreetingDialog : IDialog
	{
		public async Task StartAsync(IDialogContext context)
		{
			// The start of the code that represents the conversational dialog.
			await context.PostAsync("Welcome to the Dinner Bot. ");
			// now suspend the current dialog and wait for more dialog from the user.
			context.Wait(IncomingMessageAsync);
		}
		private static async Task Respond(IDialogContext context)
		{
			var userName = String.Empty;
			context.UserData.TryGetValue<string>("UserName", out userName);
			if (string.IsNullOrEmpty(userName))
			{
				await context.PostAsync("Tell me your name?");
				context.UserData.SetValue<bool>("readyToGetUserName", true);
			}
			else
			{
				await context.PostAsync($"Hello {userName}. Let's get started.");
			}
		}
		public virtual async Task IncomingMessageAsync(IDialogContext context, IAwaitable<IMessageActivity> currentActivity)
		{
			var message = await currentActivity;
			var userName = "";
			var readyToGetUserName = false;

			context.UserData.TryGetValue<string>("UserName", out userName);
			context.UserData.TryGetValue<Boolean>("Ready", out readyToGetUserName);
			if (readyToGetUserName)
			{
				userName = message.Text;
				context.UserData.SetValue<string>("UserName", userName);
				context.UserData.SetValue<Boolean>("Ready", false);
			}



			// change this part
			await Respond(context);
			context.Done(message);

		}
	}
}