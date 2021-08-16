using Microsoft.EntityFrameworkCore;

namespace FilesServices
{
    class FileModelsContext : DbContext
    {
        public DbSet<FileModel> FileModels { get; set; }

        public FileModelsContext(DbContextOptions<FileModelsContext> options)
            : base(options)
        {

        }
    }
}
