namespace RealEstate.ViewModel
{
    public class TransactionChartViewModel
    {
        public List<string> Labels { get; set; }
        public List<decimal> Amounts { get; set; }
        public string FilterType { get; set; } // Daily, Weekly, etc.
    }
}
