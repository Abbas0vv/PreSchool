using PreSchool.Database.Models;

namespace PreSchool.Database.Repository
{
    public class SlideBannerRepository
    {
        private PreSchoolDbContext _dbContext;
        
        public SlideBannerRepository()
        {
            _dbContext = new PreSchoolDbContext();
        }

        public List<SlideBanner> GetAll()
        {
            return _dbContext.SlideBanners.OrderBy(x=>x.Id).ToList();
        }

        public async Task Insert(SlideBanner slideBanner)
        {
            await _dbContext.SlideBanners.AddAsync(slideBanner);
            await _dbContext.SaveChangesAsync();
        }

        public SlideBanner GetById(int id)
        {
            return _dbContext.SlideBanners.FirstOrDefault(p => p.Id == id);
        }

        public void RemoveById(int id)
        {
            try
            {
                var product = GetById(id);
                _dbContext.SlideBanners.Remove(product);
                _dbContext.SaveChanges();
            }
            catch (NullReferenceException e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(SlideBanner product)
        {
            _dbContext.SlideBanners.Update(product);
            _dbContext.SaveChanges();
        }
    }
}
