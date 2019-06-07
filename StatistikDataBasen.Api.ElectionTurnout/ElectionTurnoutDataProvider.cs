using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using StatistikDataBasen.Api.Core;

namespace StatistikDataBasen.Api.ElectionTurnout
{
    public class ElectionTurnoutDataProvider : IDataProvider<ElectionTurnoutDataPoint>
    {
        const string _url = "http://api.scb.se/OV0104/v1/doris/sv/ssd/START/ME/ME0104/ME0104D/ME0104T4";
        const string _table = "ME0104B8"; //Supposing we only want to get results from a single table.

        public IEnumerable<ElectionTurnoutDataPoint> GetDataPoints()
        {
            var metadata = GetMetaData();

            string responsestring = QueryDatabase(metadata).GetAwaiter().GetResult();

            var turnoutdata = new ResponseDataParser().CreateDataPoints(responsestring);

            ConvertCountyCodeToCountyName(turnoutdata, metadata);

            return turnoutdata;
        }

        private void ConvertCountyCodeToCountyName(IEnumerable<ElectionTurnoutDataPoint> datapoints, MetaData metadata)
        {
            foreach (var datapoint in datapoints)
            {
                datapoint.County = metadata.CountyCodeNamePairs[datapoint.County];
            }
        }

        private MetaData GetMetaData()
        {
            using (var apirequest = new ApiRequest(HttpMethod.Get, new Uri(_url)))
            {
                string re = apirequest.ExecuteQuery().GetAwaiter().GetResult();

                MetaData metadata = new MetaDataParser().CreateMetaData(re);

                return metadata;
            }
        }

        private async Task<string> QueryDatabase(MetaData metadata)
        {
            using (var apirequest = new ApiRequest(HttpMethod.Post, new Uri(_url)))
            {
                var queryobject = new QueryObject("Region", "vs:RegionKommun07+BaraEjAggr", metadata.CountyCodeNamePairs.Keys.ToArray());
                var queryobject1 = new QueryObject("ContentsCode", "item", new[] { _table });
                var queryobject2 = new QueryObject("Tid", "item", metadata.Years.ToArray());

                string jsonresponse = new JsonQueryBuilder().BuildQuery(queryobject, queryobject1, queryobject2);

                string re = await apirequest.ExecuteQuery(jsonresponse);

                return re;
            }
        }
    }
}
