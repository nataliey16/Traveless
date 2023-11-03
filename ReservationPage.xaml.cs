using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Traveless;

public partial class ReservationPage : ContentPage
{
	//Access the made reservations by creating a static method
	List<Reservation> findMadeReservations = FlightsPage.AllReservations;


	//Create a collection of found reservations that match users inputs 
	public ObservableCollection <Reservation> FoundReservations { get; set; }

	//Create a collection of shown reservations that match users user inputs 
	public ObservableCollection <Reservation> ShowAvailableReservations { get; set; }
	


	public ReservationPage()
	{
		InitializeComponent();
		
		this.FoundReservations = new ObservableCollection <Reservation>();
		this.ShowAvailableReservations = new ObservableCollection <Reservation>();

		this.BindingContext = this;


	
	}

	private void ClickFindReservations(object sender, EventArgs e)
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
		}
		if (string.IsNullOrEmpty(this.findReservationCitizenship.Text) ==false)
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



		if (expectedReservationCode.Length > 0)
		{
			findReservationWithCode = true;
		}
		if(expectedReservationName.Length > 0)
		{
			findReservationWithName = true;
		}
		if(expectedReservationCitizenship.Length > 0)
		{
			findReservationWithCitizenship = true;
		}


		ObservableCollection<Reservation> foundMadeReservations = new ObservableCollection<Reservation>();


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
				reservationNameMatches=false;
			}
			if (findReservationWithCitizenship && actualReservationCitizenship != expectedReservationCitizenship)
			{
				reservationCitizenshipMatches = false;
			}

			if (reservationCodeMatches && reservationNameMatches && reservationCitizenshipMatches)
			{
				foundMadeReservations.Add(reservation);
			}
		}

		this.ShowAvailableReservations.Clear();

		foreach (Reservation reservation in foundMadeReservations)
		{

			this.ShowAvailableReservations.Add(reservation);

		}

	}

	private void UpdatedSelectedReservationDetails(Reservation selectedReservation, Flight selectedFlight)
	{
		if (selectedReservation != null)
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

}