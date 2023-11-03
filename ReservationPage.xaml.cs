using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Traveless;

public partial class ReservationPage : ContentPage
{
	//Create a list of all reservations
	public List <Reservation> AllReservations { get; set; }
	
	//Create a collection of found reservations that match users inputs 
	public ObservableCollection <Reservation> FoundReservations { get; set; }

	//Create a collection of shown reservations that match users user inputs 
	public ObservableCollection <Reservation> ShowReservations { get; set; }
	


	public ReservationPage()
	{
		InitializeComponent();

		this.AllReservations = new List <Reservation>();
		this.FoundReservations = new ObservableCollection <Reservation>();
		this.ShowReservations = new ObservableCollection <Reservation>();

		this.BindingContext = this;


		


	}

	private void ClickFindReservations(object sender, EventArgs e)
	{
		this.AllReservations.Clear();

		//inputs to find reservation

		string reservationCode = findReservationCode.Text;
		string airlineName = findReservationAirlineName.Text;
		string name = findReservationName.Text;

		if (!string.IsNullOrEmpty(reservationCode) && (!string.IsNullOrEmpty(airlineName) && (!string.IsNullOrEmpty(name)) {
			

		
		}
	}
}