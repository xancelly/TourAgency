using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourAgency.Entities
{
    public partial class Client
    {
        public string FullnameClient
        {
            get
            {
                return LastName + ' ' + FirstName + ' ' + MiddleName;
            }
        }
    }
}
