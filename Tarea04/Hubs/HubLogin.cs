using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Tarea04.Model;

namespace Tarea04.Hubs
{
    public class HubLogin : Hub
    {
        private readonly ILogger<HubLogin> _logger;

        public HubLogin(ILogger<HubLogin> logger)
        {
            _logger = logger;
        }

        public void Login(String email, String pass)
        {
            _logger.LogInformation("SignalR identificacion del usuario: " + Context.ConnectionId);
            Usuario usr = new Usuario(email, pass);
            if (usr.EsUsuarioValido())
            {
                if (usr.NecesitarVerificacion())
                {
                    //TODO
                    //mando mail al cliente
                    string usrId = Context.ConnectionId;
                    _logger.LogInformation($"**** Copiar la siguiente url para probar");
                    //NOTA: esto es solo a fines demostrativos
                    //Enviar una identificación de usuario en la url no es una buena práctica
                    //Si bien la misma viaja encriptada, la url queda en el historial del browser
                    _logger.LogInformation($"curl https://localhost:7127/verificar/usuario/{usrId}");
                }
            };
        }

        public void EnviarVerificacionOk()
        {
            Clients.User(Context.UserIdentifier).SendAsync("VerificacionOk", "");
        }
    }
}