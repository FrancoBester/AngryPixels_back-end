using application_programming_interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.MockData
{
    public static class MockData
    {
        public static Users User { get { return new Users() { User_Id = 1, Address_Id = 1, User_Name = "Dave", User_Surname = "Dave", User_ID_Number = "74108520" };  } }

        public static Roles roles { get { return new Roles() {Role_Id = 1,Role_Name="User",Role_Code="WTF IS THIS",Role_Des="Not needed" }; } }
    }
}
