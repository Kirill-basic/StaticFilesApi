using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilesServices
{
    public class FileModelProvider : IFileModelProvider
    {
        private readonly FileModelsContext _db;

        public FileModelProvider(FileModelsContext fileModels)
        {
            _db = fileModels;
        }

        public async Task<IEnumerable<FileModel>> GetAsync()
        {
            var files = await _db.FileModels.ToListAsync();
            return files;
        }

        public async Task<FileModel> PostAsync(FileModel file)
        {
            _db.FileModels.Add(file);
            await _db.SaveChangesAsync();
            return file;
        }

        public async Task<FileModel> PutAsync(FileModel file)
        {
            var fileModel = await _db.FileModels.FirstOrDefaultAsync(x => x.Id == file.Id);
            if (fileModel is null)
            {
                return null;
            }

            _db.FileModels.Update(file);
            await _db.SaveChangesAsync();
            return fileModel;
        }

        public async Task<FileModel> DeleteAsync(string fileId)
        {
            var fileModel = await _db.FileModels.FirstOrDefaultAsync(x => x.Id == fileId);
            if (fileModel is null)
            {
                return null;
            }

            _db.FileModels.Remove(fileModel);
            await _db.SaveChangesAsync();
            return fileModel;
        }
    }
}
