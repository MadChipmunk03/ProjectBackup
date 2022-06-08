using JWT.Algorithms;
using JWT.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBackupAPI.Models;

namespace WebBackupAPI.Authorization
{
    public class AuthenticationService
    {
        const string SECRET = "foo-bar";

        const string AdminPassword = "123456Ab";

        private MyContext context = new MyContext();

        public string Authenticate(string password)
        {
            //Admin admin = this.context.Admins.Where(x => x.Username == credentials.Username && x.Password == credentials.Password).FirstOrDefault();

            //if (admin == null)
            //    throw new Exception("invalid admin");

            if (password != AdminPassword) throw new Exception("incorrect password");

            return JwtBuilder.Create()
                      .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                      .WithSecret(SECRET)
                      .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                      .Encode();
        }

        public bool VerifyToken(string token)
        {
            try
            {
                string json = JwtBuilder.Create()
                             .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                             .WithSecret(SECRET)
                             .MustVerifySignature()
                             .Decode(token);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
