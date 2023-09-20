using AutoMapper.Configuration;
using HomeBanking.API.Application.Commands;
using HomeBanking.API.Application.Commands.User;
using HomeBanking.API.Application.Models;
using HomeBanking.API.Application.ViewModels;
using HomeBanking.API.Infrastructure.Auth;
using HomeBanking.Domain.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HomeBanking.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;
        private readonly AuthorizationParameters _authorization;

        public UsersController(IMediator mediator,
            IOptionsSnapshot<AuthorizationParameters> options,
            ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _authorization = options.Value;
            _logger = logger;
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="updateUserCommand">Data to update</param>
        /// <param name="requestId"></param>
        /// <returns>Returns the updated user</returns>
        [Route("update")]
        [HttpPut]
        //[Authorize]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserDTO>> UpdateUserAsync([FromBody] UpdateUserCommand updateUserCommand, [FromHeader(Name = "x-requestid")] string requestId)
        {

            bool commandResult = false;

            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestCancelOrder = new IdentifiedCommand<UpdateUserCommand, UserDTO>(updateUserCommand, guid);

                _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                updateUserCommand.GetGenericTypeName(),
                nameof(updateUserCommand.Name),
                updateUserCommand.Name,
                updateUserCommand);

                return await _mediator.Send(requestCancelOrder);
            }

            if (!commandResult)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>Returns the user</returns>
        //[Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserDTO>> GetUserByIdAsync(int id)
        {
            var user = await _mediator.Send(new GetUserByIdCommand(id));

            return Ok(user);
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteUserByIdAsync(int id)
        {
            await _mediator.Send(new DeleteUserByIdCommand(id));
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginViewModel">Data to login</param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var getUserByLoginCommnad = new GetUserByLoginCommand(loginViewModel.Login, loginViewModel.Password);
            var user = await _mediator.Send(getUserByLoginCommnad);
            if (user != null)
            {
                #region One role

                //TODO: Сделать разграничение по ролям

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var userRoles = user.UserRoles.Select(ur=>ur.Role);

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole.Name));
                }

                #endregion

                var token = CreateToken(authClaims);
                var refreshToken = GenerateRefreshToken();

                int.TryParse(_authorization.RefreshTokenValidityInDays, out int refreshTokenValidityInDays);

                var updateUserTokenCommand = new UpdateUserTokenCommand(user.Id, refreshToken, DateTime.Now.AddDays(refreshTokenValidityInDays));

                await _mediator.Send(updateUserTokenCommand);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }


        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="createUserCommand">Data to create a user</param>
        /// <returns>Returns the created user</returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand createUserCommand)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                createUserCommand.GetGenericTypeName(),
                nameof(createUserCommand.Login),
                createUserCommand.Login,
                createUserCommand);

            var result = await _mediator.Send(createUserCommand);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseViewModel { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return  new ObjectResult(new { OkObjectResult = new ResponseViewModel { Status = "Success", Message = "User created successfully!" }, result});
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <param name="tokenModel">Data to refresh token</param>
        /// <returns></returns>
        [HttpPut]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenViewModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string login = principal.Identity.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            var user = await _mediator.Send(new GetUserByLoginCommand(login, string.Empty));

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            await _mediator.Send(new UpdateUserTokenCommand(user.Id, newRefreshToken, user.RefreshTokenExpiryTime));

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }

        /// <summary>
        /// Revoke token by login
        /// </summary>
        /// <param name="login">User's login</param>
        /// <returns></returns>
        //[Authorize]
        [HttpPut]
        [Route("revoke/{login}")]
        public async Task<IActionResult> Revoke(string login)
        {
            var user = await _mediator.Send(new GetUserByLoginCommand(login, string.Empty));
            if (user == null) return BadRequest("Invalid user name");

            await _mediator.Send(new UpdateUserTokenCommand(user.Id, null, null));

            return NoContent();
        }

        /// <summary>
        /// Revoke token
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            await _mediator.Send(new UpdateUserTokenCommand(null, null, null));

            return NoContent();
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authorization.Secret));
            _ = int.TryParse(_authorization.TokenValidityInMinutes, out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _authorization.ValidIssuer,
                audience: _authorization.ValidAudience,
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authorization.Secret)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
    }
}
