using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Interfaces;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using application_programming_interface.DTOs;
using application_programming_interface.Models;

namespace application_programming_interface.Services
{
    public class BlobStorageService: IBlobStorageService
    {
        private readonly BlobServiceClient _blobClient;
        private readonly DataContext _context;
        private readonly IAuthenticationService _authenticationService;

        public BlobStorageService(BlobServiceClient blobClient, DataContext context,IAuthenticationService authentication)
        {
            _blobClient = blobClient;
            _context = context;
            _authenticationService = authentication;
        }


        public void UploadDocument(FileDTO file)
        {
            var container = _blobClient.GetBlobContainerClient("documents");
            var blobClient = container.GetBlobClient(DateTime.Now.Year+'/'+DateTime.Now.Month + file.FileName);

            blobClient.Upload(file.File.OpenReadStream());

            var fileUri = blobClient.Uri.AbsoluteUri; //Url the blob storage gets back

            //TODO: use data context to save the URI to the data base.
            //Use auth info to get the user information.
        }

        public void UploadDocumentForUser(UserDocumentUploadDTO file)
        {
            var id = _authenticationService.GetUser().Id;
            var container = _blobClient.GetBlobContainerClient("documents");
            Guid g = Guid.NewGuid();
            var fileNameToSave = "UserDocuments/" + $"{id}/" + DateTime.Now.Year + '/' + DateTime.Now.Month + $"/{g.ToString().Substring(0,4)}{file.FileName}";
            var blobClient = container.GetBlobClient(fileNameToSave);

            blobClient.Upload(file.File.OpenReadStream());

            var fileUri = blobClient.Uri.AbsoluteUri; //Url the blob storage gets back

            _context.Add<Document>(new Document() 
            {
                File_Name = fileNameToSave,
                User_Id = file.UserId,
                File_Url = fileUri,
                Doc_Type_Id = file.DocumentType
            }); // adds this document to the db

            _context.SaveChanges();
        }

        public void DeleteDocumentForUser(int userId, int docId)
        {
            //Delete the file from blobStorage First
            var doc = (from d in _context.Document
                       where d.Doc_Id == docId
                       select d).FirstOrDefault();

            var container = _blobClient.GetBlobContainerClient("documents");
            var blobClient = container.GetBlobClient(doc.File_Name);
            blobClient.Delete();
            //Delete the entry from db

            _context.Remove<Document>(doc);
            _context.SaveChanges();
        }
    }
}
