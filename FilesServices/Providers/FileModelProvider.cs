using Microsoft.EntityFrameworkCore;
using System;
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
            try
            {
                var files = await _db.FileModels.ToListAsync();
                return files;
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public async Task<FileModel> GetAsync(string fileId)
        {
            try
            {
                var fileModel = await _db.FileModels.FirstOrDefaultAsync(x => x.Id == fileId);

                return fileModel;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<FileModel> PostAsync(FileModel file)
        {
            try
            {
                _db.FileModels.Add(file);
                await _db.SaveChangesAsync();
                return file;
            }
            catch (Exception e)
            {
                throw;
            }
        }


        //TODO:try to remove ChangeTracker later
        public async Task<FileModel> PutAsync(FileModel file)
        {
            if (file is null || file.Id is null)
            {
                throw new Exception("Incorrect model");
            }

            try
            {
                var fileModel = await _db.FileModels.FirstOrDefaultAsync(x => x.Id == file.Id);

                if (fileModel is null)
                {
                    return null;
                }

                _db.ChangeTracker.Clear();

                _db.FileModels.Update(file);
                await _db.SaveChangesAsync();
                return file;
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public async Task<FileModel> DeleteAsync(string fileId)
        {
            if (fileId is null)
            {
                return null;
            }

            try
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
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
