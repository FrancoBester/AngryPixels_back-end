using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Models;
using application_programming_interface.Services;
using Microsoft.AspNetCore.Mvc;


namespace application_programming_interface.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExceptionFilter]

    public class EmailController : Controller
    {
        private readonly IMailService mailService;
        public EmailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        [Route("~/api/Send")]
        [HttpPost]
        public JsonResult Send( MailRequest request)
        {
            try
            {
                mailService.SendEmail(request);
                return new JsonResult("Mail sent");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
