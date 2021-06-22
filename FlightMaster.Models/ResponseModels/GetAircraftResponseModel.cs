namespace FlightMaster.Models.ResponseModels
{
    public class GetAircraftResponseModel
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public int AirlineId { get; set; }

        public int CarrierId { get; set; }

        public GetAirlineResponseModel Airline { get; set; }

        public GetCarrierTypeResponseModel CarrierType { get; set; }
    }
}
