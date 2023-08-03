namespace UseCase2.Models
{
    public class Balance
    {
        public Dictionary<string, float> AvailableFunds { get; set; }

        public Dictionary<string, float> PendingFunds { get; set; }
    }
}
