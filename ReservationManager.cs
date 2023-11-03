using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveless
{
    public class ReservationManager 
    {

		//Properties
		public List<Reservation> Reservations { get;}

		//Constructors

		public ReservationManager()
		{

		}

		//public MakeReservation(Flight flight, string name, string citizenship)
		//{

		//}

		//private void PopulateReservations()
		//{
		//    string[] lines = File.ReadAllLines(ReservationCSVFile);

		//    foreach (string line in lines)
		//    {
		//        string[] columns = line.Split(',');

		//        string reservationCode = columns[0];
		//        string flightCode = columns[1];
		//        string airlineName = columns[2];
		//        string costPerSeat = columns[3]; //FIX THIS LATER - NEEDS TO BE IN DECIMAL 
		//        string name = columns[4];
		//        string citizenship = columns[5];
		//        bool isActive = bool.Parse(columns[6]);

		//        Reservation addedReservation = new Reservation(reservationCode, flightCode, airlineName, costPerSeat, name, citizenship, isActive);

		//        Reservations.Add(addedReservation);
		//    }
		//}

		//public List<Reservation> GetReservations()
		//{
		//    return Reservations;
		//}




	}
}
