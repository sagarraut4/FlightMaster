namespace FlightBooking.Models.ResponseModels
{
    public class GetBookingDetailsResponseModel
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public int CustomerId { get; set; }
        public int Seats { get; set; }
        public string SeatsType { get; set; }
        public decimal Fare { get; set; }
        public string Status { get; set; }
        public string PNR { get; set; }
        public string PassengerId { get; set; }
        public GetCustomerResponseModel Customer { get; set; }
        public GetPassengerResponseModel Passenger { get; set; }
    }
}
