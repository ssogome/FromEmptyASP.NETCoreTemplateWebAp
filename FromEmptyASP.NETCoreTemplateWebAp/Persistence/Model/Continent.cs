using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FromEmptyASP.NETCoreTemplateWebAp.Persistence.Model
{
    public enum Continent
    {
        Unknown = 0,
        Europe = 1,
        Asia = 2,
        [Description("North America")]
        NorthAmerica = 3,
        [Description("South America")]
        SouthAmerica = 4,
        Africa = 5,
        Oceania = 6,
        Antarctica = 7
    }
}
