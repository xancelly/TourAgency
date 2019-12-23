using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourAgency.Entities
{
    public partial class Order
    {
        public string FullnameAgent
        {
            get
            {
                return Agent.LastName + ' ' + Agent.FirstName + ' ' + Agent.MiddleName;
            }
        }
    }
}
