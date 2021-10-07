using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.DTOs
{
    public class UserDocumentUploadDTO
    {
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public int UserId { get; set; }
        public int DocumentType { get; set; }
    }
}
