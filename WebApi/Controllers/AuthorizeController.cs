using AutoMapper;
using Core.Entities;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace WebApi.Controllers;
[Route("api/")]
[ApiController]
public class AuthorizeController : ControllerBase
{
    private readonly IAuthorizeService _service;
    private readonly IMapper _mapper;


    public AuthorizeController(
        IAuthorizeService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] UserLogin userLogin)
    {
        var JwtToken = await _service.Login(userLogin);

        Response.Cookies.Append("jwt", JwtToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true
        });

        return Ok(userLogin.Email);
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserRegister>> Register(UserRegister userRegister)
    {
        var user = _mapper.Map<User>(userRegister);

        await _service.Register(user, userRegister.Password);

        return CreatedAtAction(nameof(Register), userRegister);
    }

    [HttpGet("authorize/user")]
    public async Task<ActionResult<User>> AuthorizedUser()
    {
        var jwt = Request.Cookies["jwt"];
        var token = _service.Verify(jwt!);

        int id = int.Parse(token.Claims.First(c => c.Type == "id").Value);

        var user = await _service.Get(id);

        return Ok(user);
    }
    [HttpPost("logout")]
    public async Task<ActionResult<string>> Logout()
    {

        Response.Cookies.Delete("jwt", new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true
        });
        return Ok("success");
    }
}
