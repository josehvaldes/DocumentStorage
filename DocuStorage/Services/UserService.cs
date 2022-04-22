namespace DocuStorage.Services;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DocuStorate.Data.Model;
using DocuStorate.Data.Services;
using DocuStorage.Models;
using DocuStorage.Helpers;



public class UserService : IUserService
{
    private readonly AppSettings _appSettings;
    private IUserDataService _userdata;

    public UserService(IOptions<AppSettings> appSettings, IUserDataService userdata )
    {
        _appSettings = appSettings.Value;
        _userdata = userdata;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _userdata.Get(new User()
        {
            Username = model.Username,
            Password = model.Password,
        });

        if (user == null) 
            return null;

        var token = generateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    public User Create(User user)
    {
        return _userdata.Create(user);
    }

    public User Update(User user)
    {
        return _userdata.Update(user);
    }

    public IEnumerable<User> GetAll()
    {
        return _userdata.GetAll();
    }

    public User GetById(int id)
    {
        return _userdata.Get(id);
    }

    private string generateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public void Delete(int userId)
    {
        _userdata.Delete(userId);
    }
}

