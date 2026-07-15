using Microsoft.AspNetCore.Mvc;
using ProductSolution.Authentication;

namespace ProductSolution.Controllers;

using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using ProductSolution.DAL.Data;
using ProductSolution.DTO;
using ProductSolution.Model;

//using ProductSolution.Data;
//using ProductSolution.Model;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    //private readonly JwtService _jwtService;

    //public AuthController(JwtService jwtService)
    //{
    //    _jwtService = jwtService;
    //}

    private readonly JwtService _jwtService;
    private readonly ApplicationDbContext _context;

    public AuthController(JwtService jwtService,
                          ApplicationDbContext context)
    {
        _jwtService = jwtService;
        _context = context;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public IActionResult Login(LoginModel model)
    {
        if (model.Username == "admin" && model.Password == "123")
        {
            var accessToken = _jwtService.GenerateToken(model.Username);

            var refreshToken = _jwtService.GenerateRefreshToken();

            _context.RefreshTokens.Add(new RefreshToken
            {
                Username = model.Username,
                Token = refreshToken,
                ExpiryDate = DateTime.Now.AddDays(7)
            });

            _context.SaveChanges();

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        return Unauthorized("Invalid Username or Password");
    }

    [HttpPost("RefreshToken")]
    public IActionResult RefreshToken(RefreshTokenRequest request)
    {
        var token = _context.RefreshTokens
            .FirstOrDefault(x => x.Token == request.RefreshToken);

        if (token == null)
        {
            return Unauthorized("Invalid Refresh Token");
        }

        if (token.ExpiryDate < DateTime.Now)
        {
            return Unauthorized("Refresh Token Expired");
        }

        var accessToken = _jwtService.GenerateToken(token.Username);

        var newRefreshToken = _jwtService.GenerateRefreshToken();

        token.Token = newRefreshToken;
        token.ExpiryDate = DateTime.Now.AddDays(7);

        _context.SaveChanges();

        return Ok(new
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken
        });
    }
}