using BlazorAdminApp.Helpers;
using BlazorAdminApp.Models;

namespace BlazorAdminApp.Services
{
    public interface ICajaService
    {
        Task<CajaResponse[]> GetCajas();
    }

    public class CajaService : ICajaService
    {
        private readonly AppServices _appServices;
        private readonly IHttpService _httpService;
        private readonly ILocalStorageService _localStorageService;

        public CajaService(AppServices appServices, IHttpService httpService, ILocalStorageService localStorageService)
        {
            _appServices = appServices;
            _httpService = httpService;
            _localStorageService = localStorageService;
        }

        public async Task<CajaResponse[]> GetCajas()
        {
            var user = await _localStorageService.GetItem<UsuarioSessionResponse>("user");
            string urlRequest = _appServices.BaseAdress + string.Format(_appServices.GetCajas, user.LlaveSucursal.DecodeBase64());
            return await _httpService.GetAsync<CajaResponse[]>(urlRequest);
        }
    }
}
