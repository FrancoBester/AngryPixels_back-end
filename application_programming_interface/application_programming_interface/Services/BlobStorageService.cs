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

        public BlobStorageService(BlobServiceClient blobClient)
        {
            _blobClient = blobClient;
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
            var container = _blobClient.GetBlobContainerClient("documents");
            var blobClient = container.GetBlobClient("UserDocuments/"  +DateTime.Now.Year + '/' + DateTime.Now.Month + file.FileName);

            blobClient.Upload(file.File.OpenReadStream());

            var fileUri = blobClient.Uri.AbsoluteUri; //Url the blob storage gets back

            _context.Document.Add(new Document() 
            {
                File_Name = file.FileName,
                User_Id = file.UserId,
                File_Url = fileUri
            }); // adds this document to the db

            _context.SaveChanges();
        }
    }
}
