using System.Net;
using System.Net.Mail;

namespace ManejoPresupuesto.Servicios
{
    public interface IServicioEmail
    {
        Task EnviarEmailCambioPassword(string receptor, string enlace);
    }

    public class ServicioEmail : IServicioEmail
    {
        private readonly IConfiguration configuration;
        public ServicioEmail(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task EnviarEmailCambioPassword(string receptor, string enlace)
        {
            var email = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:EMAIL");
            var password = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:PASSWORD");
            var host = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:HOST");
            var puerto = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:PUERTO");

            var cliente = new SmtpClient(host, int.Parse(puerto));

            cliente.EnableSsl = true;
            cliente.UseDefaultCredentials = false;
            cliente.Credentials = new NetworkCredential(email, password);

            var emisor = email;
            var subject = "¿Has olvidado tu contraseña?";
            var contenidoHtml = $@"Saludos, Este mensaje le llega por que usted ha solicitado un cambio de contraseña.
                Si esta solicitud no fue hecha por usted, puede ignorar este mensaje.

                Para cambiar su contraseña, haga click en el siguiente enlace:

                {enlace}
                Atentamente, Equipo de soporte.";

            var mensaje = new MailMessage(emisor, receptor, subject, contenidoHtml);
            await cliente.SendMailAsync(mensaje);

        }
    }
}
