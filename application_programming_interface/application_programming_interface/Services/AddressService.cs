using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Models;

namespace application_programming_interface.Services
{
    public class AddressService : IAddressService
    {
        private readonly DataContext _context;
        public void WriteMsg(string msg)
        {
            Console.WriteLine("test" + msg.ToString());
        }
    }
}
