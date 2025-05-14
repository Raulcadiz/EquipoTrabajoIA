// MiembroEquipo.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

public abstract class MiembroEquipo
{
    public string Nombre { get; protected set; }
    public string Rol { get; protected set; }
    protected string Prompt { get; set; }
    public List<string> Conversacion { get; } = new List<string>();
    public ITelegramBotClient BotClient { get; }

    protected MiembroEquipo(string nombre, string rol, string token)
    {
        Nombre = nombre;
        Rol = rol;
        BotClient = new TelegramBotClient(token);
    }

    public abstract Task<string> RealizarTareaAsync(string contexto);
    public abstract Task<string> ResponderAsync(string mensaje);

    public async Task IniciarAsync()
    {
        try
        {
            var me = await BotClient.GetMeAsync();
            Console.WriteLine($"{Rol} {Nombre} (@{me.Username}) está en línea.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al iniciar {Rol} {Nombre}: {ex.Message}");
        }
    }

    protected async Task<string> GenerarRespuestaAsync(string prompt)
    {
        try
        {
            string respuesta = await AIApiUtility.GenerateResponseAsync(prompt);
            Conversacion.Add($"{Nombre}: {respuesta}");
            return respuesta;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al generar respuesta para {Nombre}: {ex.Message}");
            return "Lo siento, no pude generar una respuesta en este momento.";
        }
    }

    public async Task EnviarMensajeAsync(long chatId, string mensaje)
    {
        try
        {
            await BotClient.SendTextMessageAsync(chatId, $"{Nombre} ({Rol}): {mensaje}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al enviar mensaje de {Nombre}: {ex.Message}");
        }
    }
}
