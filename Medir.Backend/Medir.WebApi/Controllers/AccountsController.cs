using AutoMapper;
using EmailService;
using Medir.WebApi.Entities.DataTransferObjects;
using Medir.WebApi.Entities.Models;
using Medir.WebApi.JwtFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using static IdentityServer4.Models.IdentityResources;

namespace Medir.WebApi.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;
        private readonly IEmailSender _emailSender;
        public AccountsController(UserManager<User> userManager, IMapper mapper, JwtHandler jwtHandler, IEmailSender emailSender)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _emailSender = emailSender;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            var user = _mapper.Map<User>(userForRegistration);

            var userCheck = await _userManager.FindByNameAsync(user.UserName);

            if (userCheck != null)
                if (userCheck.Registered == false)
                {
                    userCheck.Patronymic = user.Patronymic;
                    userCheck.Gender = user.Gender;
                    userCheck.FirstName = user.FirstName;
                    userCheck.LastName = user.LastName;
                    userCheck.Email = user.Email;
                    userCheck.UserName = user.Email;
                    userCheck.PolyceNumber = user.PolyceNumber;
                    userCheck.Address = user.Address;
                    userCheck.DateOfBirth = user.DateOfBirth;
                    userCheck.Country = user.Country;
                    userCheck.House = user.House;
                    userCheck.Region = user.Region;
                    userCheck.Street = user.Street;
                    userCheck.PhoneNumber = user.PhoneNumber;
                    userCheck.Registered = true;

                    await _userManager.ChangePasswordAsync(userCheck, "Unregistered778", userForRegistration.Password);

                    var resultupd = await _userManager.UpdateAsync(userCheck);

                    

                    if (!resultupd.Succeeded)
                    {
                        var errors = resultupd.Errors.Select(e => e.Description);

                        return BadRequest(new FastRegistrationResponseDto { Errors = errors });
                    }

                    await _userManager.AddToRoleAsync(userCheck, "Patient");

                    return StatusCode(201);
                }          

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }

            await _userManager.AddToRoleAsync(user, "Patient");

            return StatusCode(201);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = await _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }

        [HttpPost("FastRegistration")]
        public async Task<IActionResult> FastRegister([FromBody] UserForFastRegistrationDto userForFastRegistration)
        {
            if (userForFastRegistration == null || !ModelState.IsValid)
                return BadRequest();            

            var userCheck = await _userManager.FindByNameAsync(userForFastRegistration.Email);

            if (userCheck != null)
                if (userCheck.Registered == true)
                {
                    var errors = new List<string> { "Пользователь уже зарегестрирован" };

                    return BadRequest(new FastRegistrationResponseDto { Errors = errors });
                }
                else
                {
                    userCheck.Patronymic = userForFastRegistration.Patronymic;
                    userCheck.Gender = userForFastRegistration.Gender;
                    userCheck.FirstName = userForFastRegistration.FirstName;
                    userCheck.LastName = userForFastRegistration.LastName;
                    userCheck.Email = userForFastRegistration.Email;
                    userCheck.UserName = userForFastRegistration.Email;
                    userCheck.PolyceNumber = "";
                    userCheck.Address = userForFastRegistration.Address;
                    userCheck.DateOfBirth = Convert.ToDateTime(userForFastRegistration.DateOfBirth);
                    userCheck.Country = "";
                    userCheck.House = "";
                    userCheck.Region = "";
                    userCheck.Street = "";
                    userCheck.PhoneNumber = userForFastRegistration.PhoneNumber;

                    var resultupd = await _userManager.UpdateAsync(userCheck);

                    if (!resultupd.Succeeded)
                    {
                        var errors = resultupd.Errors.Select(e => e.Description);

                        return BadRequest(new FastRegistrationResponseDto { Errors = errors });
                    }

                    return StatusCode(201);
                }

            User user = new User
            {
                Patronymic = userForFastRegistration.Patronymic,
                Gender = userForFastRegistration.Gender,
                FirstName = userForFastRegistration.FirstName,
                LastName = userForFastRegistration.LastName,
                Email = userForFastRegistration.Email,
                UserName = userForFastRegistration.Email,
                PolyceNumber = "",
                Address = userForFastRegistration.Address,
                DateOfBirth = Convert.ToDateTime(userForFastRegistration.DateOfBirth),
                Country = "",
                House = "",
                Region = "",
                Street = "",
                PhoneNumber = userForFastRegistration.PhoneNumber,
                Registered =false
            };

            var result = await _userManager.CreateAsync(user, "Unregistered778");

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new FastRegistrationResponseDto { Errors = errors });
            }

            return StatusCode(201);
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
                return BadRequest("Invalid Request");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string?>
            {
                {"token", token },
                {"email", forgotPasswordDto.Email }
            };
            var callback = QueryHelpers.AddQueryString(forgotPasswordDto.ClientURI, param);
            var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);
            await _emailSender.SendEmailAsync(message);

            return Ok();
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                return BadRequest("Invalid Request");
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }
            return Ok();
        }
    }
}
