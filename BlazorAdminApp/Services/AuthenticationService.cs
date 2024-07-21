using BlazorAdminApp.Helpers;
using BlazorAdminApp.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAdminApp.Services
{
    public interface IAuthenticationService
    {
        UsuarioSessionResponse usuarioSession { get; }
        Task Initialize();
        Task Login(string username, string password);
        Task Logout();
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpService _httpService;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;
        private readonly AppServices _appServices;

        public UsuarioSessionResponse usuarioSession { get; private set; }

        public AuthenticationService(IHttpService httpService, NavigationManager navigationManager,
            ILocalStorageService localStorageService, AppServices appServices)
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _appServices = appServices;
        }

        public async Task Initialize()
        {
            usuarioSession = await _localStorageService.GetItem<UsuarioSessionResponse>("user");
        }

        public async Task Login(string username, string password)
        {
            string urlRequest = _appServices.BaseAdress + string.Format(_appServices.GetLogin, username, password);
            usuarioSession = await _httpService.GetAsync<UsuarioSessionResponse>(urlRequest);

            if (usuarioSession.StatusCode == 401)
                throw new Exception("Credenciales inválidas");
            else if (usuarioSession.StatusCode != 200)
                throw new Exception("Se ha producido un error durante el proceso, consulte al administrador de sitio");

            usuarioSession.LlaveSucursal = $"{usuarioSession.LlaveSucursal}".EncodeBase64();
            await _localStorageService.SetItem("user", usuarioSession);
        }

        public async Task Logout()
        {
            usuarioSession = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("login");
        }
    }
}