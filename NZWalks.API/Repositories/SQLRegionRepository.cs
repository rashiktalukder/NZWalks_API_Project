using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext dbContext;
        public SQLRegionRepository(NZWalksDBContext dbContext)
        {
             this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
