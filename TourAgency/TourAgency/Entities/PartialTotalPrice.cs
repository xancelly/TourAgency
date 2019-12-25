using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourAgency.Entities
{
    public partial class Order
    {
        public string TotalPrice
        {
            get
            {
                decimal Visa = Convert.ToDecimal(Trip.Way.Country.VisaPrice);
                decimal Type = Convert.ToDecimal(TypeTrip.Price);
                decimal DayPrice = Convert.ToDecimal(DayCount) * Convert.ToDecimal(Trip.Price);
                decimal WayPrice = Convert.ToDecimal(Trip.Way.Price);
                decimal Taxation = Convert.ToDecimal(Client.Taxation.Tax);
                decimal Price = (DayPrice + WayPrice + Visa + Type) + ((DayPrice + WayPrice + Visa + Type) / 100 * Taxation);
                return Price.ToString("N2");
            }
        }
    }
}
