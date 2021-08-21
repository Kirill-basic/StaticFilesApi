using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilesServices
{
    public interface IFileModelProvider
    {
        public Task<IEnumerable<FileModel>> GetAsync();

        public Task<FileModel> GetAsync(string fileId);

        public Task<FileModel> PostAsync(FileModel file);

        public Task<FileModel> PutAsync(FileModel file);

        public Task<FileModel> DeleteAsync(string fileId);
    }
}
