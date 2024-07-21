using BlazorAdminApp.Helpers;
using BlazorAdminApp.Models;

namespace BlazorAdminApp.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioResponse[]> GetUsuarios();
    }

    public class UsuarioService : IUsuarioService
    {
        private readonly AppServices _appServices;
        private readonly IHttpService _httpService;
        private readonly ILocalStorageService _localStorageService;

        public UsuarioService(AppServices appServices, IHttpService httpService, ILocalStorageService localStorageService)
        {
            _appServices = appServices;
            _httpService = httpService;
            _localStorageService = localStorageService;
        }

        public async Task<UsuarioResponse[]> GetUsuarios()
        {
            var user = await _localStorageService.GetItem<UsuarioSessionResponse>("user");
            string urlRequest = _appServices.BaseAdress + string.Format(_appServices.GetUsuarios, user.LlaveSucursal.DecodeBase64());
            return await _httpService.GetAsync<UsuarioResponse[]>(urlRequest);
        }
    }
}
