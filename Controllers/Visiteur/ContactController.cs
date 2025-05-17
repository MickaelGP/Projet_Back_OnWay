using BackOnWay.Dtos.Visiteur;
using BackOnWay.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Visiteur
{
    [Route("[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private SendMailUtils _utilsMail = new SendMailUtils();

        [HttpPost("/contact")]
        public IActionResult SendEmail([FromBody] ContactDto infoContact)
        {
            try
            {
                _utilsMail.SendEmailContact(infoContact);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Une erreur est survenue lors de lenvoie du mail !");
            }
        }
    }
}
