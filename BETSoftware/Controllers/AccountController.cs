using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BETSoftware.Data;
using BETSoftware.DTO;
using BETSoftware.Entities;
using BETSoftware.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BETSoftware.Controllers
{
    public class AccountController: BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto) 
        {
            if (await UserIsExist(registerDto.Username))
            {
                return BadRequest("Username already exists, choose another one");
            }

            using var hmac = new HMACSHA512();

            var user = new User {
                UserName = registerDto.Username,
                PassHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PassSalt = hmac.Key
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return new UserDTO {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]

        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto) 
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) 
            {
                return Unauthorized("Account does not exist or incorrect account entered");
            }

            using var hmac = new HMACSHA512(user.PassSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++) 
            {
                if (computedHash[i] != user.PassHash[i])

                return Unauthorized("Invalid password");
            }

            return new UserDTO {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserIsExist(string username) 
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

    }
}