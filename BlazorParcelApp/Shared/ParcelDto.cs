namespace BlazorParcelApp.Shared {
    public class ParcelDto {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public State? CurrentState { get; set; }
        public string Sender { get; set; } = string.Empty;
        public string Receiver { get; set; } = string.Empty;
        public LockerDto DestLocker { get; set; }
        public LockerDto SrcLocker { get; set; }
        public NotificationDto[] NotificationDtos { get; set; } 

        public ParcelDto()
        {
            DestLocker = new LockerDto();
            SrcLocker = new LockerDto();
            NotificationDtos = new NotificationDto[0];
        }
    }
}
