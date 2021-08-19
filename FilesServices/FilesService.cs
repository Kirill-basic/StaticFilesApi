using FolderService;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FilesServices
{
    public class FilesService : IFilesService
    {
        private readonly IFolderHandlerService _folderHandler;
        private readonly IFilesProvider _filesProvider;
        private readonly FileModelsContext _modelsContext;

        public FilesService(IFolderHandlerService folderHandler, IFilesProvider filesProvider, FileModelsContext modelsContext)
        {
            _folderHandler = folderHandler;
            _filesProvider = filesProvider;
            _modelsContext = modelsContext;
        }

        public async Task<IEnumerable<FileModel>> GetAsync()
        {
            return await _modelsContext.FileModels.ToListAsync();
        }


        public Stream Get(string fileId)
        {
            return null;
        }


        public FileModel Post(Stream file, FileModel model)
        {
            return null;
        }


        public FileModel Put(Stream file, FileModel model)
        {
            return null;
        }


        public FileModel Delete(string fileId)
        {
            return null;
        }
    }
}
