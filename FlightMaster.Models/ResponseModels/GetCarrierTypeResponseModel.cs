using System;

namespace FlightMaster.Models.ResponseModels
{
    public class GetCarrierTypeResponseModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string RangeNMI { get; set; }

        public Int16 EconomySeats { get; set; }

        public Int16 BusinessSeats { get; set; }

        public Int16 PremiumEconomySeats { get; set; }

        public Int16 FirstClassSeats { get; set; }
    }
}
