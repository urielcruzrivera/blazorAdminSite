using BlazorAdminApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAdminApp.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Usuario>> GetAll();
    }

    public class UserService : IUserService
    {
        private IHttpService _httpService;

        public UserService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _httpService.GetAsync<IEnumerable<Usuario>>("/users");
        }
    }
}