using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.DTOs
{
    public class ProfileDTO
    {
        public UserInfoDTO User { get; set; }

        public List<ProfileFileDTO> Files { get; set; }
    }

    public class ProfileFileDTO
    {
        public string FileName { get; set; }
        public int FileTypeId { get; set; }
        public string FileUrl { get; set; }
    }
}
