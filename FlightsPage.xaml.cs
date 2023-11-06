using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using static Traveless.Platforms.FlightManager;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

//using Intents;

namespace Traveless
{

	public partial class FlightsPage : ContentPage

	{
		//Create a list that stores all the flights
		public List<Flight> AllFlights { get; set; }

		//Create a collection of arrival/departure codes
		public ObservableCollection<string> AirportCodes { get; set; }
		
		//Create a collection of week days of flights
		public ObservableCollection<string> DayOfFlight { get; set; }


		//Create a collection of available flights that match users inputs
		public ObservableCollection<Flight> ShowAvailableFlights { get; set; }

		//Create a list of randomized reservation codes
		public List<string> RandomizedReservationCode { get; set; }


		public static List<Reservation> AllReservations { get; set; }

		//Prevent alert from appearing multiple times


		public string FlightsCSVFilePath
		{
			get
			{

				string csvFlightsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data\\flights.csv");

				return csvFlightsFilePath;
			}
		}

		public string AirportCSVFilePath
		{
			get
			{
				string csvAirportFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data\\airports.csv");

				return csvAirportFilePath;
			}


		}



		public FlightsPage()
		{
			InitializeComponent();

			//Find flights
			this.AllFlights = new List<Flight>();
			this.AirportCodes = new ObservableCollection<string>();
			this.DayOfFlight = new ObservableCollection<string>();
			this.ShowAvailableFlights = new ObservableCollection<Flight>();


			//RESERVATIONS
			this.RandomizedReservationCode = new List<string>();
			AllReservations = new List<Reservation>();


			//Binds this context to xaml page
			this.BindingContext = this;

			PopulateFlights(); //Or change name to PopulateFlightsFromCSV();
			ListAirportCodes();
			ListDayOfFlight();
			UpdateFlights();

			MakeReservation();
			GenerateRandomCode();


		}

		private void PopulateFlights()
		{
			this.AllFlights.Clear();

			string[] lines = File.ReadAllLines(this.FlightsCSVFilePath);

			foreach (string line in lines)
			{
				string[] columns = line.Split(',');

				string flightCode = columns[0];
				string airlineName = columns[1];
				string departFrom = columns[2];
				string arriveTo = columns[3];
				string weekDay = columns[4];
				string time = columns[5];
				string totalSeats = columns[6];
				string costPerSeat = columns[7];


				Flight flight = new Flight(flightCode, airlineName, departFrom, arriveTo, weekDay, time, Convert.ToInt32(totalSeats), Convert.ToDecimal(costPerSeat));

				this.AllFlights.Add(flight);
			}

		}


		//Create a method listing the airport codes from airport.csv
		private void ListAirportCodes()
		{
			this.AirportCodes.Clear();

			
				string[] lines = File.ReadAllLines(this.AirportCSVFilePath);

				this.AirportCodes.Add("Any");

				foreach (string line in lines)
				{
					string[] columns = line.Split(",");
					if (columns.Length > 0)
					{
						string airportCode = columns[0].Trim(); // Assuming the airport code is in the first column
						this.AirportCodes.Add(airportCode);
					}

				}
			
		

		}


		//List of week days to select flight day
		private void ListDayOfFlight()
		{

			this.DayOfFlight.Clear();
			this.DayOfFlight.Add("Any");
			this.DayOfFlight.Add("Monday");
			this.DayOfFlight.Add("Tuesday");
			this.DayOfFlight.Add("Wednesday");
			this.DayOfFlight.Add("Thursday");
			this.DayOfFlight.Add("Friday");
			this.DayOfFlight.Add("Saturday");
			this.DayOfFlight.Add("Sunday");

		
		}


		private void UpdateFlights()
		{
			//Clear the list when user clicks on find flights with a new selection
			this.ShowAvailableFlights.Clear();

			foreach (Flight newFlight in this.AllFlights)
			{
				this.ShowAvailableFlights.Add(newFlight);
			}


		}



		//Add event listener to find matching flights based on users inputs
		private void ClickFindFlights(object sender, EventArgs e)
		{

			//expected input values
			string expectedDepartFromAny;
			string expectedArriveToAny;
			string expectedWeekDayAny;

			string expectedDepartFrom;
			string expectedArriveTo;
			string expectedWeekDay;

			expectedDepartFromAny = (string)this.findFromFlightsPicker.SelectedItem;
			expectedArriveToAny = (string)this.findToFlightsPicker.SelectedItem;
			expectedWeekDayAny = (string)this.findWeekDayPicker.SelectedItem;

			expectedDepartFrom = (string)this.findFromFlightsPicker.SelectedItem;
			expectedArriveTo = (string)this.findToFlightsPicker.SelectedItem;
			expectedWeekDay = (string)this.findWeekDayPicker.SelectedItem;

			//By default the inputs are false
			bool findFlightsWithDepartFromAny = false;
			bool findFlightsWithArriveToAny = false;
			bool findFlightsWithWeekDayAny = false;

			bool findFlightsWithDepartFromCode = false;
			bool findFlightsWithArriveToCode = false;
			bool findFlightsWithWeekDay = false;



			//If user selects any departure, arrival, and week day
			if (expectedDepartFromAny != "Any")
			{
				findFlightsWithDepartFromAny = true;
			}

			if (expectedArriveToAny != "Any")
			{
				findFlightsWithArriveToAny = true;
			}
			
			if (expectedWeekDayAny != "Any")
			{
				findFlightsWithWeekDayAny = true;
			}
			




			if (expectedDepartFrom != null)
			{
				findFlightsWithDepartFromCode = true;
			}
			else
			{
				DisplayAlert("Error", "Please select departure location.", "Ok");
			}
			if (expectedArriveTo != null)
			{
				findFlightsWithArriveToCode = true;
			}
			else
			{
				DisplayAlert("Error", "Please select arrival location.", "Ok");
			}
			if (expectedWeekDay != null)
			{
				findFlightsWithWeekDay = true;
			}
			else
			{
				DisplayAlert("Error", "Please select day of flight.", "Ok");
			}



			ObservableCollection<Flight> foundSelectedFlight = new ObservableCollection<Flight>();


			foreach (Flight newFlight in this.AllFlights)
			{
				bool departFromAnyMatches = true;
				bool arriveToAnyMatches = true;
				bool weekDayAnyMatches = true;

				bool departFromMatches = true;
				bool arriveToMatches = true;
				bool weekDayMatches = true;


				string actualDepartFromAny = newFlight.DepartFrom;
				string actualArriveToAny = newFlight.ArriveTo;
				string actualWeekDayAny = newFlight.WeekDay;

				string actualDepartFrom = newFlight.DepartFrom;
				string actualArriveTo = newFlight.ArriveTo;
				string actualWeekDay = newFlight.WeekDay;


				if (findFlightsWithDepartFromAny && actualDepartFromAny != expectedDepartFromAny)
				{
					departFromAnyMatches = false;
				}

				if (findFlightsWithArriveToAny && actualArriveToAny != expectedArriveToAny)
				{
					arriveToAnyMatches = false;
				}
				if (findFlightsWithWeekDayAny && actualWeekDayAny != expectedWeekDayAny)
				{
					weekDayAnyMatches = false;
				}


				if (findFlightsWithDepartFromCode && actualDepartFrom != expectedDepartFrom)
				{
					departFromMatches = false;
				}
				if (findFlightsWithArriveToCode && actualArriveTo != expectedArriveTo)
				{
					arriveToMatches = false;
				}
				if (findFlightsWithWeekDay && actualWeekDay != expectedWeekDay)
				{
					weekDayMatches = false;
				}

				if (departFromMatches && arriveToMatches && weekDayMatches)
				{
					foundSelectedFlight.Add(newFlight);
				}
	

				//Select any for depart, arrive, weekday
				if (departFromAnyMatches && arriveToAnyMatches && weekDayAnyMatches)
				{
					foundSelectedFlight.Add(newFlight);
				}

				
			}





			this.ShowAvailableFlights.Clear();

			foreach (Flight flight in foundSelectedFlight)
			{
				
				this.ShowAvailableFlights.Add(flight);
			
			}


		}

		private void UpdateSelectedFlightDetails(Flight selectedFlight)
		{
			if (selectedFlight != null)
			{
				selectedFlightCode.Text = selectedFlight.FlightCode;
				selectedAirlineName.Text = selectedFlight.AirlineName;
				selectedFlightDay.Text = selectedFlight.WeekDay;
				selectedFlightTime.Text = selectedFlight.Time;
				selectedFlightCost.Text = selectedFlight.CostPerSeat.ToString("C"); // Display cost as currency
			}
			else
			{
				// Handle case where no flights are found, clear the entry fields
				selectedFlightCode.Text = "";
				selectedAirlineName.Text = "";
				selectedFlightDay.Text = "";
				selectedFlightTime.Text = "";
				selectedFlightCost.Text = "";
			}

		}

		private void OnPickerSelectedFlightChanged(object sender, EventArgs e)
		{
			var picker = (Picker)sender;
			int selectedIndex = picker.SelectedIndex;

			if (selectedIndex != -1)
			{
				// Get the selected flight from ShowAvailableFlights collection.
				Flight selectedFlight = ShowAvailableFlights[selectedIndex];

				// Call the method to update the flight details.
				UpdateSelectedFlightDetails(selectedFlight);
			}
		}



		//<!--RESERVATION METHODS-->



		private void ClickReservation(object sender, EventArgs e)
		{
			

				//Input values
				string name;
				string citizenship;



				if (string.IsNullOrEmpty(addNameToReservation.Text) == false)
				{
					name = addNameToReservation.Text;
				}


				else
				{

				DisplayAlert("Error", "The name field is empty. Please enter your name.", "Ok");

				name = "";

				}



				if (string.IsNullOrEmpty(addCitizenshipToReservation.Text) == false)
				{
					citizenship = addCitizenshipToReservation.Text;

				}
				else
				{
					DisplayAlert("Error", "The citizenship field is empty. Please enter your citizenship.", "Ok");

					citizenship = "";

				}

				if (string.IsNullOrEmpty(addNameToReservation.Text) == false && string.IsNullOrEmpty(citizenship) == false)
				{

					//Display the generated reservation code in your UI
					if (RandomizedReservationCode.Count > 0)
					{
						string reservationCode = RandomizedReservationCode[0]; // Assuming you want to display the first generated code
						reservationCodeLabel.Text = reservationCode;

					}

					// Display the generated reservation code in UI

					MakeReservation();

				}
			


		}


		private void GenerateRandomCode()
		{
			this.RandomizedReservationCode.Clear();

			
			Random random = new Random();

			string charSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";


			int randomNumberCode = random.Next(1000, 9999);
			int randomIndex = random.Next(0, charSet.Length);

			char randomChar = charSet[randomIndex];

			string randomReservationCode = randomChar + randomNumberCode.ToString();

			this.RandomizedReservationCode.Add(randomReservationCode);

		}

		private void MakeReservation()
		{

			
				//Clear all reservations each time user refreshes page
				AllReservations.Clear();

				//User's inputs to make reservation: reservation code, name, citizenship and combine it with flight information
				string reservationCode = reservationCodeLabel.Text;
				string name = addNameToReservation.Text;
				string citizenship = addCitizenshipToReservation.Text;

				if (!string.IsNullOrEmpty(reservationCode) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(citizenship))
				{
					Flight selectedFlightFromPicker = GetSelectedFlight();

					if (selectedFlightFromPicker != null)
					{

						Reservation reservation = new Reservation();

						reservation.ReservationCode = reservationCode;
						reservation.Flight = selectedFlightFromPicker;
						reservation.Name = name;
						reservation.Citizenship = citizenship;

						AllReservations.Add(reservation);

					}


				}
			
		}
	



		// Method to get the selected flight based on the picker selection
		private Flight GetSelectedFlight()
		{
			var selectedIndex = findAvailableFlightsPicker.SelectedIndex;

			if (selectedIndex >= 0 && selectedIndex < ShowAvailableFlights.Count)
			{
				return ShowAvailableFlights[selectedIndex];
			}

			return null; // Return null if no flight is selected

		}

		

	}
}