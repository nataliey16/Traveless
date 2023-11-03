using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveless
{
    public class Reservation
    {
     
      
        public string ReservationCode //The generated code
        {
            get;
            set;
        }

		public Flight Flight
		{
			get;
			set;
		}


		//public string FlightCode // 
		//{
		//    get;
		//    set;
		//}

		//public string AirlineName
		//{
		//    get;
		//    set;
		//}

		//public string CostPerSeat
		//{
		//    get;
		//    set;
		//}

		public string Name
        {
            get;
            set;
        }

        public string Citizenship
        {
            get;
            set;
        }

		public bool IsActive
		{
			get;
			set;
		}


		//Constructors
		public Reservation()
        {

        }

        //public Reservation(string reservationCode, string flightCode, string airlineName, string costPerSeat, string name, string citizenship, bool isActive)
        //{
          
        //    this.ReservationCode = reservationCode;
        //    this.FlightCode = flightCode;
        //    this.AirlineName = airlineName;
        //    this.CostPerSeat = costPerSeat;
        //    this.Name = name;
        //    this.Citizenship = citizenship;
        //    this.IsActive = isActive;
        //}

		public Reservation(Flight flight, string reservationCode, string name, string citizenship, bool isActive)
		{

			this.ReservationCode = reservationCode;
			this.Flight = flight;
			//this.FlightCode = flightCode;
			//this.AirlineName = airlineName;
			//this.CostPerSeat = costPerSeat;
			this.Name = name;
			this.Citizenship = citizenship;
			this.IsActive = isActive;
		}

		//public string GenerateReservationCode(Flight flight)
		//{

		//}

		public override string ToString()
        {
            return $"{ReservationCode}, {Flight}, {Name}, {Citizenship}, {IsActive}";
		}
	}
}
