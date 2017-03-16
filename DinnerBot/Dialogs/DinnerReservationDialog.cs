using DinnerBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace DinnerBot.Dialogs
{
	public class DinnerReservationDialog
	{

		public static readonly IDialog<string> dialog = Chain.PostToChain()
			.Select(m => m.Text)
			.Switch(
						new RegexCase<IDialog<string>>(new Regex("^hi", RegexOptions.IgnoreCase),
				(context, text) =>
				{
					return Chain.ContinueWith(new GreetingDialog(), AfterGreetingContinuation);
				}),
			new DefaultCase<string, IDialog<string>>((context, text) =>
			{
				return Chain.ContinueWith(FormDialog.FromForm(DinnerReservation.BuildForm,
				FormOptions.PromptInStart), AfterGreetingContinuation);
			}))
			.Unwrap()
			.PostToUser();

		private async static Task<IDialog<string>> AfterGreetingContinuation(IBotContext context, IAwaitable<object> item)
		{
			var token = await item;
			var userName = "User";
			context.UserData.TryGetValue<string>("UserName", out userName);
			return Chain.Return($"Thank you for using Dinner Bot.");
		}
	}
}

