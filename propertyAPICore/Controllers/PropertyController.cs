using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository.Services;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace propertyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private IPropertyForSaleRepository _propertyForSaleRepository;

        public PropertyController()
        {
            _propertyForSaleRepository = new PropertyForSaleRepository();
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            try
            {
                var properties = _propertyForSaleRepository.GetAll();
                return Ok(properties);
            }
            catch
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var property = _propertyForSaleRepository.GetById(id);

                if (property == null)
                    return NotFound();

                return Ok(property);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetSpecificUserProperties")]
        public IActionResult GetSpecificUserProperties([FromQuery] string email)
        {
            try
            {
                var properties = _propertyForSaleRepository.GetPropertiesByEmail(email);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] PropertyForSaleDTO propertyForSaleDTO)
        {
            try
            {
                _propertyForSaleRepository.Add(propertyForSaleDTO);
                _propertyForSaleRepository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [Authorize]
        [HttpPut]
        public IActionResult Put([FromBody] PropertyForSaleDTO propertyForSaleDTO)
        {
            try
            {
                _propertyForSaleRepository.Update(propertyForSaleDTO);
                _propertyForSaleRepository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [Authorize]
        [HttpDelete("{ID:int}")]
        public IActionResult Delete(int ID)
        {
            try
            {
                _propertyForSaleRepository.Delete(ID);
                _propertyForSaleRepository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        //---------------------User-----------------------------


        //------------------------User End----------------------------
        //--------------------Email----------------------------------
        [HttpPost]
        [Route("Email")]
        public IActionResult Email()
        {
            try
            {
                var httpRequest = System.Web.HttpContext.Current.Request;
                string phoneNo = httpRequest["phoneNo"];
                string toMail = httpRequest["email"];
                string buyerMail = httpRequest["buyerMail"];
                string body = httpRequest["body"];
                body += "<br>Get in touch with me at<br>Email: " + buyerMail + "<br>";
                body += "Phone : " + phoneNo;
                if (buyerMail != "" && phoneNo != "")
                {
                    MailMessage mailMessage = new MailMessage("*******", toMail);
                    mailMessage.IsBodyHtml = true;
                    // Specify the email body
                    mailMessage.Body = body;
                    // Specify the email Subject
                    mailMessage.Subject = "Buyer Mail from Property Website";

                    // Specify the SMTP server name and post number
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    // Specify your gmail address and password
                    smtpClient.Credentials = new System.Net.NetworkCredential()
                    {
                        UserName = "********",
                        Password = "*******"
                    };
                    // Gmail works on SSL, so set this property to true
                    smtpClient.EnableSsl = true;
                    // Finall send the email message using Send() method
                    smtpClient.Send(mailMessage);

                    return Ok();
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        //--------------------Email End----------------------------------
        //--------------------Whatsapp----------------------------------
        [HttpPost]
        [Route("Whatsapp")]
        public IActionResult Whatsapp([FromBody] WhatsappMessage whatsappMessage)
        {
            string msg = "ok";
            try
            {
                var phone = HttpContext.User.Identities.First().FindFirst(ClaimTypes.MobilePhone).Value;
                whatsappMessage.Body += "\nGet in touch with me at this number: " + phone;
                msg = "before";
                TwilioClient.Init("AC146b60466a7dc9ce4038eb3b43cf4ab2", "715b56702c570f372ce640c0aa3364f4");
                // var client = new TwilioRestClient(accountSid, authToken);


                // Pass the client into the resource method
                var message = MessageResource.Create(
                    to: new Twilio.Types.PhoneNumber($"whatsapp:" + whatsappMessage.ToPhone),
                    from: new Twilio.Types.PhoneNumber($"whatsapp:+14155238886"),
                    body: whatsappMessage.Body);
                msg = message.Sid;
                return Ok(message);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        //--------------------Whatsapp end----------------------------------=

    }
}
