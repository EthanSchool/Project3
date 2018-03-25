using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    class AthleteResident : Resident
    {
        public override float Rent => 1_200;
    }
}
