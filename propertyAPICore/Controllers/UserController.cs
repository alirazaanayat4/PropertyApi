using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.EFModels;
using Repository.Interfaces;
using Repository.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace propertyAPICore.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        public UserController()
        {
            _userRepository = new UserRepository();
        }

        [HttpPost]
        [Route("RegisterUser")]
        public IActionResult RegisterUser([FromBody] UserDTO user)
        {
            try
            {
                var entity = _userRepository.GetByEmail(user.Email);
                if (entity == null)
                {
                    _userRepository.Add(user);
                    _userRepository.Save();
                    return Ok(user);
                }
                else
                    return new ConflictObjectResult("User with email " +
                        user.Email + " already exists");
            }
            catch
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        [Route("Login")]
        public IActionResult Login([FromQuery] string email, [FromQuery] string password)
        {
            try
            {
                var user = _userRepository.GetByEmail(email);
                if (user.Password.Equals(password))
                {
                    string token = GenerateAccessToken(user.Name, user.Email, user.Phone);

                    UserWithTokenDTO userWithToken = new UserWithTokenDTO();
                    userWithToken = user.ToTokenUserDTO(token);

                    return Ok(userWithToken);
                }
                else
                    return new NotFoundObjectResult("Invalid email or password.");
            }
            catch
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
        [Authorize]
        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            try
            {
                var user = _userRepository.GetByEmail(changePasswordDTO.Email);
                if (user != null && user.Password.Equals(changePasswordDTO.OldPassword))
                {
                    _userRepository.ResetPassword(changePasswordDTO.Email, changePasswordDTO.NewPassword);
                    _userRepository.Save();
                    return Ok();
                }
                else
                    return new NotFoundObjectResult("Invalid email or password.");
            }
            catch
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789qwertyuiopasdfghjklzxcvbnm";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] EmailDTO emailDto)
        {
            try
            {
                var entity = _userRepository.GetByEmail(emailDto.Email);
                if (entity != null)
                {
                    string token = RandomString(6); //WebSecurity.GeneratePasswordResetToken(toMail);

                    MailMessage mailMessage = new MailMessage("razagujjar535@gmail.com", emailDto.Email);
                    mailMessage.IsBodyHtml = true;
                    // Specify the email body
                    mailMessage.Body = "Your The Real Estate account recovery code is here<br>" + token;
                    // Specify the email Subject
                    mailMessage.Subject = "Password Reset Link";

                    // Specify the SMTP server name and post number
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    // Specify your gmail address and password
                    smtpClient.Credentials = new System.Net.NetworkCredential()
                    {
                        UserName = "***********",
                        Password = "***********"
                    };
                    // Gmail works on SSL, so set this property to true
                    smtpClient.EnableSsl = true;
                    // Finall send the email message using Send() method
                    smtpClient.Send(mailMessage);

                    return Ok(token);
                }
                else
                    return new NotFoundObjectResult("User with " + emailDto.Email + " not found");
            }
            catch
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword()
        {
            var httpRequest = System.Web.HttpContext.Current.Request;
            string email = httpRequest["email"];
            string password = httpRequest["password"];
            try
            {
                var user = _userRepository.GetByEmail(email);
                if (user != null)
                {
                    _userRepository.ResetPassword(email, password);
                    _userRepository.Save();
                    return Ok();
                }
                else
                    return new NotFoundObjectResult("User with " + email + " not found");
            }
            catch
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
        private string GenerateAccessToken(string name, string email, string phone)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Repository.Configurations.AppConfigurations.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.MobilePhone, phone)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
