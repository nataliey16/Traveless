using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveless
{
	public class Airport
	{
		//Properties
		public string Code
		{
			get; set;
		}

		public string Name
		{
			get;
			set;
		}

		public Airport(string code, string name)
		{
			this.Code = code;
			this.Name = name;
		}

		public override string ToString()
		{
			string airportInformation = $"{Code}, {Name}";
			return airportInformation;
		}
	}
}
