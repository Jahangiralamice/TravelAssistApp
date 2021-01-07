using System.Data.Entity;
using TravelAssistApp.Infrastructure;
using TravelAssistApp.Models;

namespace TravelAssistApp.Repository
{
    public class TouristPlacesRepository : RepositoryBase<TouristPlace>, ITouristPlacesRepository
    {
        ApplicationDbContext _context;
        public TouristPlacesRepository(DbContext context) : base(context)
        {
            _context = (ApplicationDbContext)context;
        }
    }

    public interface ITouristPlacesRepository : IRepository<TouristPlace>
    {

    }
}