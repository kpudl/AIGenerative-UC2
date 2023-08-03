namespace UseCase2.Models
{
    public class Balance
    {
        public Dictionary<string, long> AvailableFunds { get; set; }

        public Dictionary<string, long> PendingFunds { get; set; }
    }
}
