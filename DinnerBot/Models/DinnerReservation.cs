using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DinnerBot.Models
{
	[Serializable]
	public class DinnerReservation
	{
		// add a property for each dialog choice in the bot.
		// Bot Frameworks asks the questions in the order
		// declared in the class.

		public string VenueName { get; set; }
		public int? PersonCount { get; set; }
		public DateTime? ReservationDay { get; set; }
		public DinnerTimeOptions DinnerTime { get; set; }
		public List<ExtraOptions> Extras { get; set; }
		public static IForm<DinnerReservation> BuildForm()
		{
			return new FormBuilder<DinnerReservation>()
				.Message("Welcome to the Dinner Bot.  Let's get started.")
				.Build();

			  
		}

		public enum ExtraOptions {
			Flowers,
			OutsideTable,
			WatersideTable
		}

		public enum DinnerTimeOptions {
			Breakfast,
			Lunch,
			Dinner,
			LateDinner
		}
	}
}