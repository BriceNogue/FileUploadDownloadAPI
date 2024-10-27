using Application.IRepository.IFileRepository;
using Domain.Entities;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.FileRepository
{
    public class FileRepository : BaseRepository<FileEntity>, IFileRepository
    {
        public FileRepository(DataContext context) : base(context) { }

        public async Task<FileEntity> GetFileByNameAsync(string fileName)
        {
            var res = await GetAll();
            var file = res.FirstOrDefault(x => x.FileName == fileName);

            if (file == null)
                return file;

            return null;
        }

        public async Task UploadFile(string fileName, byte[] fileContent)
        {
            if (fileContent == null) 
            {
                throw new ArgumentNullException(nameof(fileContent));
            }

            FileEntity file = new FileEntity
            {
                FileName = fileName,
                Contents = fileContent,
                FileSize = fileContent.Length
            };

            Create(file);
        }
    }
}
