using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace StatistikDataBasen.Api.ElectionTurnout
{
    internal class ResponseDataParser
    {
        internal IEnumerable<ElectionTurnoutDataPoint> CreateDataPoints(string jsonstring)
        {
            var data = new List<ElectionTurnoutDataPoint>();

            JObject jobject = JObject.Parse(jsonstring);

            foreach (var key in jobject["data"].Children())
            {
                data.Add(CreateDataPoint(key));
            }

            return data;
        }

        private ElectionTurnoutDataPoint CreateDataPoint(JToken token)
        {
            string county = token["key"][0].ToString();
            int year = int.Parse(token["key"][1].ToString());

            //For the cases where turnout is not a number.
            if (!double.TryParse(token["values"][0].ToString(), out double turnout))
            {
                turnout = -1;
            }

            return new ElectionTurnoutDataPoint(year, county, turnout);
        }
    }
}
