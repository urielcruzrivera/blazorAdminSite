using BlazorAdminApp.Helpers;
using BlazorAdminApp.Models;

namespace BlazorAdminApp.Services
{
    public interface ICorteCajaService
    {
        Task<CorteCajaResponse> GetCorteCaja(CorteCajaRequest corteCajaRequest);
    }

    public class CorteCajaService : ICorteCajaService
    {
        private readonly AppServices _appServices;
        private readonly IHttpService _httpService;
        private readonly ILocalStorageService _localStorageService;

        public CorteCajaService(AppServices appServices, IHttpService httpService, ILocalStorageService localStorageService)
        {
            _appServices = appServices;
            _httpService = httpService;
            _localStorageService = localStorageService;
        }

        public async Task<CorteCajaResponse> GetCorteCaja(CorteCajaRequest corteCajaRequest)
        {
            Usuario user = await _localStorageService.GetItem<Usuario>("user");
            corteCajaRequest.ClaveSucursal = user.LlaveSucursal.DecodeBase64();
            string urlRequest = _appServices.BaseAdress + string.Format(_appServices.GetCorteCaja, corteCajaRequest.EquipoId, corteCajaRequest.FechaDesde,
                corteCajaRequest.FechaHasta, corteCajaRequest.UsuarioId, corteCajaRequest.ClaveSucursal);
            return await _httpService.GetAsync<CorteCajaResponse>(urlRequest);
        }
    }
}
