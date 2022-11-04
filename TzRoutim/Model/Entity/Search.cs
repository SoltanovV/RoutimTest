namespace TzRoutim.Model.Entity;
/// <summary>
/// Поиск
/// </summary>
public class Search
{
    /// <summary>
    /// Id поиска
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Строка поиска
    /// </summary>
    public string StringSearch { get; set; } = string.Empty;

    /// <summary>
    /// Нацигационное свойство
    /// </summary>
    public IEnumerable<GitEntity> GitEntity { get; set; }
}
