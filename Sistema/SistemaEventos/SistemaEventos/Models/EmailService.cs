using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace SistemaEventos.Models
{
    public class EmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        public EmailService(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }

        public async Task EnviarCorreoAsync(string to, string subject, string body)
        {
            try
            {
                var message = new MailMessage(_smtpUser, to, subject, body);
                var client = new SmtpClient(_smtpServer, _smtpPort)
                {
                    Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                    EnableSsl = true // Asegurarse de que EnableSsl está configurado correctamente
                };
                await client.SendMailAsync(message);
            }
            catch (SmtpException ex)
            {
                // Manejo de errores específico para problemas SMTP
                throw new Exception("Error al enviar correo electrónico: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                // Manejo de errores general
                throw new Exception("Error general al enviar correo electrónico: " + ex.Message, ex);
            }
        }
    }
}