namespace TzRoutim.Interfaces
{
    public interface ISearchServices
    {
        /// <summary>
        /// Производит поиск по ключевому слову
        /// </summary>
        /// <param name="stringSearch">Ключевое слово для поиска</param>
        /// <returns>Возвращает найденные записи</returns>
        public Task<Search> SearchAsync(string stringSearch);

        /// <summary>
        /// Вывод всех записей
        /// </summary>
        /// <returns>Возвращает все записи из бд</returns>
        public Task<IEnumerable<Search>> GetSearchAsync();

        /// <summary>
        /// Удаление записей из бд
        /// </summary>
        /// <param name="id">Id записи</param>
        /// <returns>Возвращает null</returns>
        public Task<Search> DeleteSearchAsync(Guid id);
    }
}

