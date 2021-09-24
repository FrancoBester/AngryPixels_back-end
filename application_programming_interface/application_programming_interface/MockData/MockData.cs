using application_programming_interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.MockData
{
    public static class MockData
    {
        public static Users User { get { return new Users() { User_id = 1, Address_id = 1, User_name = "Dave", User_surname = "Dave", User_ID_Number = "74108520" };  } }

        public static Roles roles { get { return new Roles() {Role_id = 1,Role_name="User",Role_code="WTF IS THIS",Role_des="Not needed" }; } }
    }
}
