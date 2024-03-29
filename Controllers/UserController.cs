﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OhJwt.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        /// <summary>
        /// 
        /// user login and response his token 
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("Login")]
        public ActionResult Login(string user, string password)
        {
            if (user != "mytone" || password != "mytone")
            {
                return Ok("Error!");
            }
            string userid = "55";
            string rolez = "Admin"; // or main , common

            string token=GenerateToke(userid,user,password,rolez);
            return Ok(token);
        }

        /// <summary>
        /// 
        /// made token and return token.  function.
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private string GenerateToke(string userid,string user, string password,string rolez)
        {
            string secret = "1235234242384721424214328789"; //token , key must >16 chars
            string issuer = "mytone";
            string audience = "mytoneto";
            var securityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var sigingCredentials=new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim("userid", userid),
                new Claim("user", user),
                new Claim(ClaimTypes.Role,rolez)    //add to roles of mirosoft system 

            };
            SecurityToken securityToken = new JwtSecurityToken(
                issuer:issuer,
                audience:audience,
                claims:claims,
                signingCredentials:sigingCredentials,
                expires:DateTime.Now.AddMinutes(30)
            );
            return new JwtSecurityTokenHandler().WriteToken(securityToken);

        }

        [HttpGet("GetUserInfo")]
        [Authorize]
        public ActionResult GetUserInfo() 
        {
            return Ok("Token access!");
        }


        [HttpGet("TestNoAuth")]
        public ActionResult TestNoAuth()
        {
            return Content("No Token but Access!");
        }


    }
}
