using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
	public class Finance : IEntity
	{
		public int Id { get; set; }
		public decimal Price { get; set; }
		public string CurrencyType { get; set; }
		public bool IsCredit { get; set; }
		public bool IsBarter { get; set; }
		public bool IsGuarantee { get; set; }
	}

}
