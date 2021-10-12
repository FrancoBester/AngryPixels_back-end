using application_programming_interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.Services
{
    public class SchemaRequestService
    {
        private readonly DataContext _context;

        public SchemaRequestService(DataContext context)
        {
            _context = context;
        }
    }
}
