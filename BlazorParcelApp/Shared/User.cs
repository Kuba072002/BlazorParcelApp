using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlazorParcelApp.Shared {
    public class User {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; } = "Customer";
        public List<Parcel> SentParcels { get; set; } = new List<Parcel>();
        public List<Parcel> ReceivedParcels { get; set; } = new List<Parcel>();

    }
}
