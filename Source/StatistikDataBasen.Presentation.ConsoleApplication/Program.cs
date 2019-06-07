using System;
using System.Collections.Generic;
using System.Linq;
using StatistikDataBasen.Api.ElectionTurnout;

namespace StatistikDataBasen.Presentation.ConsoleApplication
{
    class Program
    {

        static void Main(string[] args)
        {
            var data = GetElectionTurnOutData();

            PrintHighestPerYear(data);


            Console.WriteLine("\nPress any key to close...");
            Console.ReadKey();
        }

        private static IEnumerable<ElectionTurnoutDataPoint> GetElectionTurnOutData()
        {
            var data = new ElectionTurnoutDataProvider().GetDataPoints();

            return data;
        }

        private static void PrintHighestPerYear(IEnumerable<ElectionTurnoutDataPoint> turnoutdata)
        {

            foreach (var year in GetAvailableYears(turnoutdata))
            {
                var yearresults = FilterByYear(turnoutdata, year);

                var orderedlist = OrderByTurnout(yearresults).ToList();

                var highestturnout = orderedlist.FirstOrDefault();

                var topcounties = GetNamesOfTopCounties(orderedlist).ToList();

                string resultstring = $"{ highestturnout.Year} { string.Join(", ", topcounties) } { highestturnout.Turnout}%";

                Console.WriteLine(resultstring);
            }
        }

        private static IEnumerable<string> GetNamesOfTopCounties(IEnumerable<ElectionTurnoutDataPoint> orderedlist)
        {
            var topresults = orderedlist.Where((x) => x.Turnout == orderedlist.FirstOrDefault().Turnout);

            List<string> names = new List<string>();

            foreach (var result in topresults)
            {
                names.Add(result.County);
            }
            return names;
        }

        private static IEnumerable<ElectionTurnoutDataPoint> OrderByTurnout(IEnumerable<ElectionTurnoutDataPoint> datapoints)
        {
            return datapoints.OrderByDescending((y) => y.Turnout);
        }

        private static IEnumerable<ElectionTurnoutDataPoint> FilterByYear(IEnumerable<ElectionTurnoutDataPoint> datapoints, int year)
        {
            return datapoints.Where((x) => x.Year == year).ToList();
        }

        private static List<int> GetAvailableYears(IEnumerable<ElectionTurnoutDataPoint> turnoutdata)
        {
            List<int> years = new List<int>();

            foreach (var item in turnoutdata)
            {
                if (!years.Contains(item.Year))
                {
                    years.Add(item.Year);
                }
            }

            return years;
        }
    }
}
