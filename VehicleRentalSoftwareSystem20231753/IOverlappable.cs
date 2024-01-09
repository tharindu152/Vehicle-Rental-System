using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem20231753
{
    internal interface IOverlappable
    {
        bool Overlaps(Schedule other);

    }
}
