﻿using System.Net.Mail;
using System.Net;

namespace OMD_TechX.Helpers
{
    public static class CorreoElectronico
    {
        public static void enviarCorreo(string toAdresss, string name, string password)
        {
            var fromAddress = new MailAddress("techxdevteam@gmail.com", "TechX Team");
            var toAddress = new MailAddress(toAdresss, name);
            const string fromPassword = "tknjoravwdnmxzrt";
            const string subject = "¡Oh My Dog! - Registro exitoso";
            string body = $"Hola {name}! Este es un correo automático de ¡Oh My Dog! " +
                $"Su contraseña fue generada con éxito, para su primer inicio de sesión utilice" +
                $"este correo y la contraseña: {password}" +
                $" Al ingresar, se le solicitara cambiarla." +
                $"Muchas gracias por usar nuestros servicios!";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })

            smtp.Send(message);
        }
        public static void enviarCorreoDesdeHasta(string fromAdress,string toAdresss, string name, string nombrePerro)
        {
            var fromAddress = new MailAddress("techxdevteam@gmail.com", "TechX Team");
            var toAddress = new MailAddress(toAdresss, name);
            const string fromPassword = "tknjoravwdnmxzrt";

            const string subject = $"Sobre tu publicacion de adopcion!";
            string body = $"Hola {name}! Este es un correo automatico de ¡Oh My Dog! \n" + 
                $"Estoy interesado en adoptar a {nombrePerro} \n" +
                $"Por favor contactame a el siguiente correo: {fromAdress} \n" +
                "Espero tu respuesta con ansias.\n " + 
                "Saludos!";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })

                smtp.Send(message);
        }

        public static void enviarCorreoGenerico(string fromAdress, string toAdresss, string name, string nombrePerro, string mensaje)
        {
            var fromAddress = new MailAddress("techxdevteam@gmail.com", "TechX Team");
            var toAddress = new MailAddress(toAdresss, name);
            const string fromPassword = "tknjoravwdnmxzrt";

            const string subject = $"Sobre tu publicacion en Oh My Dog!";
            string body = mensaje;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })

                smtp.Send(message);
        }

        public static void enviarCorreoDescuento(string toAdresss, string name, string mensaje)
        {
            var fromAddress = new MailAddress("techxdevteam@gmail.com", "TechX Team");
            var toAddress = new MailAddress(toAdresss, name);
            const string fromPassword = "tknjoravwdnmxzrt";

            const string subject = $"Gracias por tu donación en Oh My Dog!";
            string body = mensaje;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })

                smtp.Send(message);
        }
    }
}
