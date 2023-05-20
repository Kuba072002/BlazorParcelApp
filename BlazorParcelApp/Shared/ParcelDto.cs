namespace BlazorParcelApp.Shared {
    public class ParcelDto {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public State? CurrentState { get; set; }
        public string Sender { get; set; } = string.Empty;
        public string Receiver { get; set; } = string.Empty;
        public string DestLocker { get; set; } = string.Empty;
        public string SrcLocker { get; set; } = string.Empty;
    }
}
