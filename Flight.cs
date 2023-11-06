using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveless
{
	public class Flight
	{


		//Properties

		public string FlightCode //Change to FlightCode
		{
			get;
		}

		public string AirlineName
		{
			get;
			set;
		}

		public string DepartFrom
		{
			get;
			set;
		}

		public string ArriveTo
		{
			get;
			set;
		}

		public string WeekDay
		{
			get;
			set;
		}

		public string Time
		{
			get;
			set;
		}


		public decimal CostPerSeat
		{
			get;
			set;
		}
		public int TotalSeats
		{
			get;
			set;
		}




		//Constructors


		public Flight(string flightCode, string airlineName, string departFrom, string arriveTo, string weekDay, string time, int totalSeats, decimal costPerSeat)
		{
			this.FlightCode = flightCode;
			this.AirlineName = airlineName;
			this.DepartFrom = departFrom;
			this.ArriveTo = arriveTo;
			this.WeekDay = weekDay;
			this.Time = time;
			this.TotalSeats = totalSeats;
			this.CostPerSeat = costPerSeat;


		}



		//Methods

		public override string ToString()
		{
			string availableFlights = $"{FlightCode}, {AirlineName},{DepartFrom}, {ArriveTo}, {WeekDay}, {Time}, {CostPerSeat}";
			return availableFlights;
		}
	}
}
