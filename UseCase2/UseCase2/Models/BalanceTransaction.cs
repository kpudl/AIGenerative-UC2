namespace UseCase2.Models
{
    public class BalanceTransaction
    {
        public string Id { get; set; }

        public float Amount { get; set; }

        public string Currency { get; set; }

        public float NettoAmount { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }

        public DateTime Created { get; set; }
    }
}
