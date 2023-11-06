using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.IO; // Add this for System.IO
using System.Text.Json;
namespace Traveless {


	public partial class ReservationPage : ContentPage
	{
		//Access the made reservations by creating a static method
		List<Reservation> findMadeReservations = FlightsPage.AllReservations;

		//Create a collection of found reservations that match users inputs 
		public ObservableCollection<Reservation> FoundReservations { get; set; }

		//Create a collection of shown reservations that match users user inputs 
		public ObservableCollection<Reservation> ShowAvailableReservations { get; set; }


		//Create a JSON file to save reservations 


		public string reservation_JSON_file
		{
			get
			{
			
				string jsonReservationFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\Data\reservations.json");

				return jsonReservationFilePath;
			
			}
		}



		public ReservationPage()
		{
			InitializeComponent();

			this.findMadeReservations = FlightsPage.AllReservations;
			//this.foundAvailableReservations = new List<Reservation>();
			this.ShowAvailableReservations = new ObservableCollection<Reservation>();
			this.BindingContext = this;

			App.Current.MainPage.Window.Destroying += Window_Destroying;


		}

		//Method to find reservations
		private void ClickFindReservations(object sender, EventArgs e)
		{

			try
			{

				string expectedReservationCode;
				string expectedReservationName;
				string expectedReservationCitizenship;

				if (string.IsNullOrEmpty(this.findReservationCode.Text) == false)
				{
					expectedReservationCode = this.findReservationCode.Text;
				}
				else
				{
					expectedReservationCode = "";
				}

				if (string.IsNullOrEmpty(this.findReservationName.Text) == false)
				{
					expectedReservationName = this.findReservationName.Text;
				}
				else
				{

					expectedReservationName = "";
					throw new InvalidNameException("Error");

				}
				if (string.IsNullOrEmpty(this.findReservationCitizenship.Text) == false)
				{
					expectedReservationCitizenship = this.findReservationCitizenship.Text;
				}
				else
				{
					expectedReservationCitizenship = "";
				}


				bool findReservationWithCode = false;
				bool findReservationWithName = false;
				bool findReservationWithCitizenship = false;


				//Validate that all expected inputs are inputted 
				if (expectedReservationCode.Length > 0)
				{
					findReservationWithCode = true;
				}
				if (expectedReservationName.Length > 0)
				{
					findReservationWithName = true;
				}
				if (expectedReservationCitizenship.Length > 0)
				{
					findReservationWithCitizenship = true;
				}


				//Create a list of found reservations that are made
				List<Reservation> foundMadeReservations = new List<Reservation>();


				foreach (Reservation reservation in FlightsPage.AllReservations)
				{
					bool reservationCodeMatches = true;
					bool reservationNameMatches = true;
					bool reservationCitizenshipMatches = true;


					string actualReservationCode = reservation.ReservationCode;
					string actualReservationName = reservation.Name;
					string actualReservationCitizenship = reservation.Citizenship;


					if (findReservationWithCode && actualReservationCode != expectedReservationCode)
					{
						reservationCodeMatches = false;
					}
					if (findReservationWithName && actualReservationName != expectedReservationName)
					{
						reservationNameMatches = false;
					}
					if (findReservationWithCitizenship && actualReservationCitizenship != expectedReservationCitizenship)
					{
						reservationCitizenshipMatches = false;
					}

					//If the reservation code, name and citizenship matches the list, add to the list
					if (reservationCodeMatches && reservationNameMatches && reservationCitizenshipMatches)
					{
						foundMadeReservations.Add(reservation);
					}
				}

				this.ShowAvailableReservations.Clear();


				//Go through each found reservation and add to show available list
				foreach (Reservation reservation in foundMadeReservations)
				{

					this.ShowAvailableReservations.Add(reservation);
				}

			}
			catch (InvalidNameException ex)
			{
				DisplayAlert("InvalidNameException", ex.Message, "OK");

			}
			catch (InvalidCitizenshipException ex)
			{
				DisplayAlert("InvalidCitizenshipException", ex.Message, "Ok");
			}



		}

		private void UpdatedSelectedReservationDetails(Reservation selectedReservation, Flight selectedFlight)
		{
			//If selected reservations and flight are not null then bind them to xaml
			if (selectedReservation != null && selectedFlight != null)
			{
				selectedReservationCode.Text = selectedReservation.ReservationCode;
				selectedFlightCode.Text = selectedFlight.FlightCode;
				selectedAirlineName.Text = selectedFlight.AirlineName;
				selectedFlightDay.Text = selectedFlight.WeekDay;
				selectedFlightTime.Text = selectedFlight.Time;
				selectedFlightCost.Text = selectedFlight.CostPerSeat.ToString("C"); // Display cost as currency
				selectedReservationName.Text = selectedReservation.Name;
				selectedReservationCitizenship.Text = selectedReservation.Citizenship;

				

			}
			//If they are null then keep them empty
			else
			{
				selectedReservationCode.Text = "";
				selectedFlightCode.Text = "";
				selectedAirlineName.Text = "";
				selectedFlightDay.Text = "";
				selectedFlightTime.Text = "";
				selectedFlightCost.Text = "";
				selectedReservationName.Text = "";
				selectedReservationCitizenship.Text = "";
			}
		}


		private void OnPickerSelectedReservationChanged(object sender, EventArgs e)
		{
			var picker = (Picker)sender;
			int selectedIndex = picker.SelectedIndex;

			if (selectedIndex != -1)
			{
				// Get the selected flight from ShowAvailableReservations collection.
				Reservation selectedReservation = ShowAvailableReservations[selectedIndex];
				Flight selectedFlight = ShowAvailableReservations[selectedIndex].Flight;

				// Call the method to update the reservation details.
				UpdatedSelectedReservationDetails(selectedReservation, selectedFlight);
			}
		}




		private void SaveReservationToJSONFile()
		{
			// Create a list of JSON strings
			List<string> jsonList = new List<string>();

			//For each reservation in the show available list
			foreach (Reservation reservation in this.ShowAvailableReservations)
			{
				//encode them into JSON objects
				string jsonEncoded = JsonSerializer.Serialize(reservation);
				jsonList.Add(jsonEncoded);
				// add them to the jsonList
			}

			// Append the list of JSON strings to the file
			File.AppendAllLines(this.reservation_JSON_file, jsonList);
		}



		private void Window_Destroying(object sender, EventArgs e)
		{

			DisplayAlert("Update", "Your flight reservation has been saved.", "Ok");
			// Save employees to file before app closes.
			this.SaveReservationToJSONFile();
		}

	}



}

		
