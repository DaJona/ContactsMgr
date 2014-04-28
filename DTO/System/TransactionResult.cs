
namespace DTO.System
{
    public class TransactionResult
    {
        public enum transactionResultCode
        {
            Failed = 0,
            Success = 1
        }

        public transactionResultCode code { get; set; }
        public string failureReason { get; set; }
        public int affectedId { get; set; }
        public object object1 { get; set; }
        public object object2 { get; set; }
    }
}
