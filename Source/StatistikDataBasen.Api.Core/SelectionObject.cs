using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StatistikDataBasen.Api.Core
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class SelectionObject
    {
        public string Filter { get; }
        public string[] Values { get;}

        public SelectionObject(string filter, string[] values)
        {
            Filter = filter;
            Values = values;
        }
    }
}
