using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Users.Services.API.Models.Dtos;
using Users.Services.API.Services.IService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Users.Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;

        public AuthController(
            IAuthService authService, 
            ILogger<AuthController> logger, 
            IConfiguration configuration)
        {
            _authService = authService;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var response = await _authService.LoginAsync(loginRequest);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDTO>> Register([FromBody] RegisterRequestDTO registerRequest)
        {
            var response = await _authService.RegisterAsync(registerRequest);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPut("update/{userId}")]
        public async Task<ActionResult<ResponseDto<UserDTO>>> UpdateUser(int userId, [FromBody] UpdateUserDTO updateUserDto)
        {
            _logger.LogInformation("Authorization header: {Header}", Request.Headers["Authorization"].ToString());
            _logger.LogInformation("User ID from token: {UserId}", User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var response = await _authService.UpdateUserAsync(userId, updateUserDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            try
            {
                var clientId = _configuration["GoogleAuthSettings:ClientId"];
                if (string.IsNullOrEmpty(clientId))
                {
                    return BadRequest(new { error = "Google Client ID is not configured" });
                }

                var redirectUri = "https://localhost:7071/api/auth/google-callback";
                var scope = "email profile";
                
                var googleAuthUrl = $"https://accounts.google.com/o/oauth2/v2/auth?" +
                                  $"client_id={clientId}&" +
                                  $"redirect_uri={redirectUri}&" +
                                  $"response_type=token&" +
                                  $"scope={scope}&" +
                                  $"access_type=offline&" +
                                  $"prompt=consent";

                _logger.LogInformation("Generated Google Auth URL with redirect URI: {RedirectUri}", redirectUri);
                return Ok(new { 
                    url = googleAuthUrl,
                    redirectUri = redirectUri,
                    message = "Please make sure this redirect URI is added to your Google Cloud Console project"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating Google Auth URL");
                return BadRequest(new { 
                    error = "Error generating Google Auth URL",
                    details = ex.Message
                });
            }
        }

        [HttpPost("verify-otp")]
        public async Task<ActionResult<AuthResponseDTO>> VerifyOtp([FromBody] VerifyOtpDTO verifyOtp)
        {
            _logger.LogInformation("Attempting to verify OTP for email: {Email}", verifyOtp.Email);
            var response = await _authService.VerifyOtpAsync(verifyOtp);
            
            if (!response.IsSuccess)
            {
                _logger.LogWarning("OTP verification failed: {Message}", response.Message);
                return BadRequest(response);
            }

            _logger.LogInformation("OTP verification successful for email: {Email}", verifyOtp.Email);
            return Ok(response);
        }

        [HttpDelete("delete/{email}")]
        public async Task<ActionResult<ResponseDto<bool>>> DeleteUser(string email)
        {
            _logger.LogInformation("Attempting to delete user with email: {Email}", email);
            var response = await _authService.DeleteUserAsync(email);
            
            if (!response.IsSuccess)
            {
                _logger.LogWarning("Failed to delete user: {Message}", response.Message);
                return BadRequest(response);
            }

            _logger.LogInformation("Successfully deleted user with email: {Email}", email);
            return Ok(response);
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<ResponseDto<bool>>> ForgotPassword([FromBody] ForgotPasswordDTO forgotPassword)
        {
            _logger.LogInformation("Attempting to send forgot password OTP to email: {Email}", forgotPassword.Email);
            var response = await _authService.ForgotPasswordAsync(forgotPassword);
            
            if (!response.IsSuccess)
            {
                _logger.LogWarning("Failed to send forgot password OTP: {Message}", response.Message);
                return BadRequest(response);
            }

            _logger.LogInformation("Successfully sent forgot password OTP to email: {Email}", forgotPassword.Email);
            return Ok(response);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<ResponseDto<bool>>> ResetPassword([FromBody] ResetPasswordDTO resetPassword)
        {
            _logger.LogInformation("Attempting to reset password for email: {Email}", resetPassword.Email);
            var response = await _authService.ResetPasswordAsync(resetPassword);
            
            if (!response.IsSuccess)
            {
                _logger.LogWarning("Failed to reset password: {Message}", response.Message);
                return BadRequest(response);
            }

            _logger.LogInformation("Successfully reset password for email: {Email}", resetPassword.Email);
            return Ok(response);
        }

        [HttpPost("change-password")]
        public async Task<ActionResult<ResponseDto<bool>>> ChangePassword([FromBody] ChangePasswordDTO changePassword)
        {
            _logger.LogInformation("Attempting to change password for email: {Email}", changePassword.Email);
            var response = await _authService.ChangePasswordAsync(changePassword);
            
            if (!response.IsSuccess)
            {
                _logger.LogWarning("Failed to change password: {Message}", response.Message);
                return BadRequest(response);
            }

            _logger.LogInformation("Successfully changed password for email: {Email}", changePassword.Email);
            return Ok(response);
        }
    }
} 