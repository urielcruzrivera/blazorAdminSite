using BlazorAdminApp.Models;
using BlazorAdminApp.Helpers;
using Microsoft.Extensions.Options;

namespace BlazorAdminApp.Services
{
    public interface IBitacoraService
    {
        Task<Bitacora[]> GetBitacoraVentas(string fechaInicio, string fechaFin);
    }

    public class BitacoraService:IBitacoraService
    {
        private readonly AppServices _appServices;
        private readonly IHttpService _httpService;

        public BitacoraService(IOptions<AppServices> options, IHttpService httpService)
        {
            _appServices = options.Value;
            _httpService = httpService;
        }

        public async Task<Bitacora[]> GetBitacoraVentas(string fechaInicio, string fechaFin)
        {
            //AppServices.Base
            string urlRequest = string.Format(_appServices.BaseAdress + _appServices.PostBitacora);
            TRequest request = new() { 
                LlaveSucursal = "66e3d9c2-e730-4041-a811-a6bf32de8690",
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };
            //return await _httpService.PostAsync<Bitacora[]>(urlRequest, request);
            return await _httpService.Post<Bitacora[]>(urlRequest, request);
        }
    }
}
