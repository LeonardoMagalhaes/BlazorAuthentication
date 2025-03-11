using BlazorApp.Api.Models;
using BlazorApp.Models.DTOs;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorApp.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly string _key;

        public UserService(IConfiguration configuration)
        {
            var client = new MongoClient("mongodb://root:example@localhost:27017/");
            var database = client.GetDatabase("BlazorApp");
            _users = database.GetCollection<User>("Users");
            //_key = configuration.GetSection("JwtKey").ToString();
            _key = "this is my custom Secret key for authentication";
        }

        public List<User> GetUsers() => _users.Find(user => true).ToList();

        public User GetUser(string id) => _users.Find<User>(user => user.Id == id).FirstOrDefault();

        public User CreateUser(UserRequestDTO request)
        {
            var userExists = _users.Find<User>(user => user.Email == request.Email).FirstOrDefault();
            if (userExists != null)
            {
                return null;
            }

            var user = new User
            {
                Email = request.Email,
                Password = request.Password,
                Role = request.Role
            };

            _users.InsertOne(user);
            return user;
        }

        public string AuthenticateUser(string email, string password)
        {
            var userInDb = _users.Find<User>(u => u.Email == email && u.Password == password).FirstOrDefault();

            if (userInDb == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication" /*_key*/);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, userInDb.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
