using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace application_programming_interface.DTOs
{
    public class UserDescriptorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Roles { get; set; }

        public void SetData(int id,string name,string surname,List<string> roles)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Roles = roles;
        }

    }
}
