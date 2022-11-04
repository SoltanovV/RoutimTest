using AutoMapper;
using Newtonsoft.Json;
using Octokit;
namespace TzRoutim.Utilities;

public class AddDataDb
{
    private readonly ApplicationContext _db;
    
    public AddDataDb(ApplicationContext db)
    {
        _db = db;
    }
   
    public async Task<Search> Write(string url)
    {
        try
        {
            GitEntity gitEntity = new();
            Search search= new();
            // Настройка 
            var client = new GitHubClient(new ProductHeaderValue("GetusersRepositories"));

            // поиск репозитория по ключевому слову
            var request = new SearchRepositoriesRequest(url);
            //Получение репозиториев
            var json = await client.Search.SearchRepo(request);            

            // Запись в бд
            search.StringSearch = url;
            await _db.Searches.AddAsync(search);
            await _db.SaveChangesAsync();

            // Запись в бд
            gitEntity.SearchId = search.Id;
            await _db.GitEntities.AddAsync(gitEntity);

            //Добавление репозиториев в бд
            foreach (var res in json.Items)
            {
                gitEntity.Id = res.Id;
                gitEntity.Name = res.Name;
                gitEntity.Login = res.Owner.Login;
                gitEntity.SubscribersCount = res.SubscribersCount;
                gitEntity.StargazersCount = res.StargazersCount;
                gitEntity.HtmlUrl = res.HtmlUrl;

                await _db.GitEntities.AddAsync(gitEntity);
                await _db.SaveChangesAsync();
            }

            return await _db.Searches.Include(s => s.GitEntity).SingleOrDefaultAsync(s => s.StringSearch == url);

        }
        catch
        {
            throw;
        }
    }
}
