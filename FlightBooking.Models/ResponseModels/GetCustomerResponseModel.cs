﻿namespace FlightBooking.Models.ResponseModels
{
    public class GetCustomerResponseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
