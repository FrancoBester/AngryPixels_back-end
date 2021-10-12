using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.DTOs
{
    public class ClientSchemaRequestDTO
    {
        //What i need for this
        //The actual policy and its docs
        public PolicyInfoDTO PolicyInfo { get; set; }
        //The user Profile and their docs
        public ClientInformationDTO ClientInformation { get; set; }

        //The Request information
        public SchemaRequestDTO SchemaRequest { get; set; }
    }

    public class ClientInformationDTO
    {
        public string Fullname { get; set; }
        public string IDNumber { get; set; }
        public string Email { get; set; }
        public string Cell { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }

        public UserFileDTO MedicalCertificate { get; set; }
        public UserFileDTO BirthCertificate { get; set; }
        public UserFileDTO Passport { get; set; }
    }

    public class UserFileDTO
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
    }
}
