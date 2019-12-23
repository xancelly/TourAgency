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
             //   Convert.ToString(((trip.Price * decimal.Parse(DayCountTextBox.Text)) + trip.Way.Price) + ((trip.Price + trip.Way.Price) / 100 * CurrentTaxation));
             
                string Price;
                Price = Convert.ToString(((Trip.Price * Convert.ToInt32(DayCount)) + Trip.Way.Price) + ((Trip.Price + Trip.Way.Price) / 100 * Convert.ToDecimal(Client.Taxation.Tax)));
                return Price;
            }
        }
    }
}
