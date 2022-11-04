using System;

namespace TzRoutim.Services;

public class SearchServices : ISearchServices
{
    private readonly ApplicationContext _db;
    public SearchServices(ApplicationContext db)
    {
        _db = db;
    }
  
    public async Task<Search> SearchAsync(string stringSearch)
    {
        try
        {
            var search = await _db.Searches.FirstOrDefaultAsync(x => x.StringSearch == stringSearch);
            if (search != null)
            {
                return await _db.Searches.Include(s => s.GitEntity).SingleOrDefaultAsync(s => s.StringSearch == stringSearch); 
            }
            return null;
            
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Search>> GetSearchAsync()
    {
        try
        {
            return _db.Searches.Include(s => s.GitEntity);
        }
        catch
        {

            throw;
        }
    }

    public async Task<Search> DeleteSearchAsync(Guid id)
    {
        try
        {
            var search = await _db.Searches.FirstOrDefaultAsync(s => s.Id == id);
            if (search != null)
            {
                _db.Searches.Remove(search);
                await _db.SaveChangesAsync();
            }
            return search;  
        }
        catch
        {

            throw;
        }
    }
}
