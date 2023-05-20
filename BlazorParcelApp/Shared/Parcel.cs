namespace BlazorParcelApp.Shared {
    public class Parcel {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public State CurrentState { get; set; }
        public int? SenderId { get; set; }
        public User Sender { get; set; }
        
        public int? ReceiverId { get; set; }
        public User Receiver { get; set; }
        
        public int? DestId { get; set; }
        public Locker DestLocker { get; set; }
        
        public int? SrcId { get; set; }
        public Locker SrcLocker { get; set; }
    }
}
