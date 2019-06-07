using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace StatistikDataBasen.Api.ElectionTurnout
{
    internal class MetaDataParser
    {
        internal MetaData CreateMetaData(string metastring)
        {
            JObject obj = JObject.Parse(metastring);

            var prop = obj.Property("variables").Value;

            var countycodesdata = GetCountyPairsData(FindJsonPropertyWithCode(prop, "Region"));
            var yearsdata = GetYearsData(FindJsonPropertyWithCode(prop, "Tid"));

            var metadata = new MetaData(countycodesdata, yearsdata);

            return metadata;

        }

        private List<string> GetYearsData(JToken token)
        {
            List<string> yearsdata = new List<string>();

            foreach (var year in token["values"].Values())
            {
                yearsdata.Add(year.ToString());
            }

            return yearsdata;
        }

        private Dictionary<string, string> GetCountyPairsData(JToken token)
        {
            var pairs = new Dictionary<string, string>();

            List<string> countycodes = new List<string>();
            List<string> countynames = new List<string>();

            var codesproperty = token["values"];
            var countynamesproperty = token["valueTexts"];

            foreach (var code in codesproperty.Values())
            {
                countycodes.Add(code.ToString());
            }

            foreach (var name in countynamesproperty.Values())
            {
                countynames.Add(name.ToString());
            }

            for (int i = 0; i < countycodes.Count; i++)
            {
                pairs.Add(countycodes[i], countynames[i]);
            }

            //Remove first entry ["00", "Riket"]
            pairs.Remove("00");

            return pairs;
        }

        //Should probably be an extension-method
        private JToken FindJsonPropertyWithCode(JToken token, string propertycode)
        {
            var childrenobjects = token.Children();

            foreach (var childobject in childrenobjects)
            {
                if (childobject["code"].ToString() == propertycode)
                {
                    return childobject;
                }
            }

            return null;
        }
    }

}
