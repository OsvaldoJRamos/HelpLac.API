using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace HelpLac.API.Controllers
{
    public class ApiController : ControllerBase
    {
        protected readonly IMapper _mapper;
        public int _userId { get; private set; }

        public ApiController(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected void GetToken()
        {
            var requestToken = Request.Headers["Authorization"];
            var token = requestToken.ToString().Replace("Bearer", "").Trim();

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);

                _userId = Convert.ToInt32(jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "nameid").Value);
            }
        }
    }
}
