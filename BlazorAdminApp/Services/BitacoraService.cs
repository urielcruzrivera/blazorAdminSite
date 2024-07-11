using BlazorAdminApp.Models;
using BlazorAdminApp.Helpers;

namespace BlazorAdminApp.Services
{
    public interface IBitacoraService
    {
        Task<Bitacora[]> GetBitacoraVentas(string fechaInicio, string fechaFin);
    }

    public class BitacoraService : IBitacoraService
    {
        private readonly AppServices _appServices;
        private readonly IHttpService _httpService;
        private readonly ILocalStorageService _localStorageService;

        public BitacoraService(AppServices appServices, IHttpService httpService, ILocalStorageService localStorageService)
        {
            _appServices = appServices;
            _httpService = httpService;
            _localStorageService = localStorageService;
        }

        public async Task<Bitacora[]> GetBitacoraVentas(string fechaInicio, string fechaFin)
        {
            string urlRequest = _appServices.BaseAdress + _appServices.PostBitacora;
            var user = await _localStorageService.GetItem<Usuario>("user");
            TRequest request = new()
            {
                LlaveSucursal = user.LlaveSucursal.DecodeBase64(),
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };
            return await _httpService.PostAsync<Bitacora[]>(urlRequest, request);
        }
    }
}
