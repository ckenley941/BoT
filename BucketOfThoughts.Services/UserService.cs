using System.Diagnostics;

namespace BucketOfThoughts.Services
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
