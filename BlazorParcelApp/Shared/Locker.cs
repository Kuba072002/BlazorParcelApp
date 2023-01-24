using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorParcelApp.Shared {
    public class Locker {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Parcel> ParcelsDest { get; set; } = new List<Parcel>();
        public List<Parcel> ParcelsSrc { get; set; } = new List<Parcel>();
    }
}
