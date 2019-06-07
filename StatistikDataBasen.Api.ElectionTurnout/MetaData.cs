using System.Collections.Generic;

namespace StatistikDataBasen.Api.ElectionTurnout
{
    internal class MetaData
    {
        internal Dictionary<string, string> CountyCodeNamePairs { get; }
        internal List<string> Years { get; }

        public MetaData(Dictionary<string, string> citycodenamepairs, List<string> years)
        {
            CountyCodeNamePairs = citycodenamepairs;
            Years = years;
        }
    }
}
