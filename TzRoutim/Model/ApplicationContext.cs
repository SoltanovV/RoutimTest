using TzRoutim.Model.Entity;

namespace TzRoutim.Model
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Запросы
        /// </summary>
        public DbSet<Search> Searches { get; set; } = null!;

        /// <summary>
        /// Репозитории
        /// </summary>
        public DbSet<GitEntity> GitEntities { get; set; } = null!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="options">Настройки</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated(); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Создание связи один ко многим
            modelBuilder.Entity<GitEntity>()
                .HasOne(ge => ge.Search)
                .WithMany(s => s.GitEntity)
                .HasForeignKey(ge => ge.SearchId);
        }

        
    }
}
