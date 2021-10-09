using application_programming_interface.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.Interfaces
{
    public interface IBlobStorageService
    {
        void UploadDocument(FileDTO file);
        void UploadDocumentForUser(UserDocumentUploadDTO file);
        void DeleteDocumentForUser(int userId, int docId);
    }
}
