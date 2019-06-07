using Newtonsoft.Json.Linq;

namespace StatistikDataBasen.Api.Core
{
    public class JsonQueryBuilder
    {
        public string BuildQuery(params QueryObject[] objects)
        {
            JArray queryobjects = JArray.FromObject(objects);

            JObject obj =
                new JObject(
                    new JProperty("query",
                            queryobjects),
                    new JProperty("response",
                            new JObject(
                                new JProperty("format", "json"))
                            ));

            return obj.ToString();
        }
    }
}
