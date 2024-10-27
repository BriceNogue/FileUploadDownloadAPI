using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FileEntity : BaseEntity
    {
        public byte[] Contents { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
    }
}
