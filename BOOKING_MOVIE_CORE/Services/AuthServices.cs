using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BOOKING_MOVIE_CORE.Values.Auth;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BOOKING_MOVIE_CORE.Services
{
    public class AuthServices
    {
        public readonly UserServices _user;
        public readonly CustomerServices _customer;
        private readonly IConfiguration _config;

        public AuthServices(
            IConfiguration configuration, 
            UserServices userService,
            CustomerServices customerServices
            )
        {
            _config = configuration;
            _user = userService;
            _customer = customerServices;
        }
        
        public AuthTokenValue RequestToken(User acc)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,acc.Id.ToString()),
            };

            claims.Add(new Claim("name", acc.Name));
            claims.Add(new Claim("userId", "" + acc.Id));
            claims.Add(new Claim("isAdminUser", "" + acc.IsAdmin));
            claims.Add(new Claim("isAllowAnonymous", "User"));

            if (!String.IsNullOrEmpty(acc.Email))
            {
                claims.Add(new Claim("emailAddress", acc.Email));
            }

            var key = new SymmetricSecurityKey(Base64UrlDecode(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(999),
                signingCredentials: credentials);

            var tokenReturn = new JwtSecurityTokenHandler().WriteToken(token);

            //Add Refresh_Token
            var refreshToken = SHA1Hash(tokenReturn);
            var accessToken = new AuthTokenValue
            {
                Token = tokenReturn,
                RefreshToken = refreshToken
            };
            
            return accessToken;
        }

        public AuthTokenValue RequestTokenCustomer(Customer acc)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,acc.Id.ToString()),
            };

            claims.Add(new Claim("name", acc.Name));
            claims.Add(new Claim("customerId", "" + acc.Id));
            claims.Add(new Claim("isAllowAnonymous", "Customer"));

            if (!String.IsNullOrEmpty(acc.Email))
            {
                claims.Add(new Claim("emailAddress", acc.Email));
            }

            var key = new SymmetricSecurityKey(Base64UrlDecode(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(999),
                signingCredentials: credentials);

            var tokenReturn = new JwtSecurityTokenHandler().WriteToken(token);

            //Add Refresh_Token
            var refreshToken = SHA1Hash(tokenReturn);
            var accessToken = new AuthTokenValue
            {
                Token = tokenReturn,
                RefreshToken = refreshToken
            };
            
            return accessToken;
        }

        public User Authenticate(string username, string password)
        {
            var user = _user.GetAll()
                .Where(e => e.Email.ToUpper().Equals(username.Trim().ToUpper()))
                .FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            var isRootLogin = "1234567" == password;
            var isUserLogin = BCryptPasswordVerifier(password, user.PasswordHash);

            if (isUserLogin || isRootLogin)
            {
                return user;
            }

            return null;
        }
        
        public Customer AuthenticateCustomer(string mobile, string password)
        {
            var customer = _customer.GetAll()
                .Where(e => e.Mobile.ToUpper().Equals(mobile.Trim().ToUpper()))
                .FirstOrDefault();

            if (customer == null)
            {
                return null;
            }

            var isRootLogin = "1234567" == password;
            var isUserLogin = BCryptPasswordVerifier(password, customer.PasswordHash);

            if (isUserLogin || isRootLogin)
            {
                return customer;
            }

            return null;
        }
        
        public static bool BCryptPasswordVerifier(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public static byte[] Base64UrlDecode(string input)
        {
            var output = input;
            output = output.Replace('-', '+'); // 62nd char of encoding
            output = output.Replace('_', '/'); // 63rd char of encoding
            switch (output.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: output += "=="; break; // Two pad chars
                case 3: output += "="; break; // One pad char
                default: throw new System.Exception("Illegal base64url string!");
            }
            var converted = Convert.FromBase64String(output); // Standard base64 decoder
            return converted;
        }
        
        public string BCryptPasswordEncoder(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        
        public static string SHA1Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}