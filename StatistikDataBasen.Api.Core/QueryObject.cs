using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StatistikDataBasen.Api.Core
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class QueryObject
    {
        public string Code { get; }
        public SelectionObject Selection { get; }


        public QueryObject(string code, string filter, string[] values)
        {
            this.Code = code;
            Selection = new SelectionObject(filter, values);
        }
    }
}
