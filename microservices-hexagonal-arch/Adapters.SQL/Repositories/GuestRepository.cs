using Domain.Entities;
using Domain.Ports;

namespace Adapters.SQL.Repositories
{
    public class GuestRepository(HotelDbContext hotelDbContext) : IGuestRepository
    {
        public Task<Guest> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Create(Guest guest)
        {
            hotelDbContext.Guests.Add(guest);
            await hotelDbContext.SaveChangesAsync();

            return guest.Id;
        }

        public async Task<bool> Update(Guest guest)
        {
            hotelDbContext.Guests.Update(guest);
            await hotelDbContext.SaveChangesAsync();

            return true;
        }
    }
}
