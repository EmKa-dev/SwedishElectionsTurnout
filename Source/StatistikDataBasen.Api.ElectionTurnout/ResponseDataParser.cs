using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;

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
            double? turnout = null;

            //For the cases where turnout is not a number.
            if (double.TryParse(token["values"][0].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
            {
                turnout = result;
            }

            return new ElectionTurnoutDataPoint(year, county, turnout);
        }
    }
}
