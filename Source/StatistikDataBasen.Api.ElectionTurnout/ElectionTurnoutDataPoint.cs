namespace StatistikDataBasen.Api.ElectionTurnout
{
    public class ElectionTurnoutDataPoint
    {
        public int Year { get; }
        public string County { get; set; }
        public double? Turnout { get; }

        public ElectionTurnoutDataPoint(int year, string county, double? turnout)
        {
            Year = year;
            County = county;
            Turnout = turnout;
        }
    }
}
