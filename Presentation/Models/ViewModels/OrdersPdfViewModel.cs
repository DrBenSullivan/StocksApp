using System.ComponentModel.DataAnnotations;
using StocksApp.Domain.Models;

namespace StocksApp.Presentation.Models.ViewModels
{
	public class OrdersPdfViewModel
	{
		public List<Order> Orders { get; set; } = new List<Order>();
	}
}

