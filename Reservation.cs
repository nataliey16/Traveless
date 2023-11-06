using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Traveless
{
    public class Reservation
    {

		//private string name;
		//private string citizenship;
      
        public string ReservationCode 
        {
            get;
            set;
        }

		public Flight Flight
		{
			get;
			set;
		}



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



		//Constructors
		public Reservation()
        {

        }


		public Reservation(Flight flight, string reservationCode, string name, string citizenship)
		{

			if (string.IsNullOrEmpty(citizenship))
			{
				throw new InvalidCitizenshipException("Citizenship field is empty or null. Please enter a valid value.");
			}

			if(string.IsNullOrEmpty(name))
			{
				throw new InvalidNameException("Name field is empty or null. Please enter a valid value.");
			}

			this.ReservationCode = reservationCode;
			this.Flight = flight;
			this.Name = name;
			this.Citizenship = citizenship;
	
		}

		public override string ToString()
        {
            return $"{ReservationCode}, {Flight}, {Name}, {Citizenship}";
		}
	}
}
