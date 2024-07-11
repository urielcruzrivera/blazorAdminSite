using BlazorAdminApp.Helpers;
using BlazorAdminApp.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAdminApp.Services
{
    public interface IAuthenticationService
    {
        Usuario User { get; }
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

        public Usuario User { get; private set; }

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
            User = await _localStorageService.GetItem<Usuario>("user");
        }

        public async Task Login(string username, string password)
        {
            string urlRequest = _appServices.BaseAdress + string.Format(_appServices.GetLogin, username, password);
            User = await _httpService.GetAsync<Usuario>(urlRequest);

            if (User.StatusCode == 401)
                throw new Exception("Credenciales inválidas");
            else if (User.StatusCode != 200)
                throw new Exception("Se ha producido un error durante el proceso, consulte al administrador de sitio");

            User.LlaveSucursal = $"{User.LlaveSucursal}".EncodeBase64();
            await _localStorageService.SetItem("user", User);
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("login");
        }
    }
}