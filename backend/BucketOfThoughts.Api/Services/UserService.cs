using System.Diagnostics;

namespace BucketOfThoughts.Api.Services
{
    public class UserService : IUserService
    {
        public void GetUserInfo()
        {
            Trace.WriteLine("Getting user info.");
            return;
        }
    }
}
