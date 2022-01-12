using System.Threading.Tasks;

namespace FindYourFlix.Data.Interfaces
{
    public interface IUserInfo
    {
        public Task<string> GetUserId();
    }
}