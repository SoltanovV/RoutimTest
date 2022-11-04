namespace TzRoutim.Model.Entity;

/// <summary>
/// Репозиторий
/// </summary>
public class GitEntity
{
    /// <summary>
    /// Id репозитория
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Имя проекта
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Имя автора
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Ссылка на проект
    /// </summary>
    public string HtmlUrl { get; set; }

    /// <summary>
    /// Количество звезд 
    /// </summary>
    public int? SubscribersCount { get; set; }

    /// <summary>
    /// Количество просмотров 
    /// </summary>
    public int? StargazersCount { get; set; }

    /// <summary>
    /// Id поиска
    /// </summary>
    public Guid SearchId { get; set; }

    /// <summary>
    /// Навигационное свойство 
    /// </summary>
    public Search Search { get; set; } 

}
