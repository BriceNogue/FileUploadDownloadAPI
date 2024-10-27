using Application.Repository;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository.IFileRepository
{
    public interface IFileRepository : IBaseRepository<FileEntity>
    {
        Task UploadFile(string fileName, byte[] fileContent);
        Task<FileEntity> GetFileByNameAsync(string fileName);
    }
}
