using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Room : BaseEntity
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public bool IsAvailable => !InMaintenance && !HasGuest;
        public bool HasGuest => true;
        public Price Price { get; set; }
    }
}
