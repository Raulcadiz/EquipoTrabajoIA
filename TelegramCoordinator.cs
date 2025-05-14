// TelegramCoordinator.cs
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;

public class TelegramCoordinator
{
    private readonly EquipoTrabajo _equipo;
    private readonly Dictionary<long, string> _tareasPendientes = new Dictionary<long, string>();

    public TelegramCoordinator(EquipoTrabajo equipo)
    {
        _equipo = equipo;
    }

    public async Task IniciarAsync(long groupChatId)
    {
        await _equipo.IniciarEquipoAsync(groupChatId);

        foreach (var miembro in _equipo.Miembros)
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new[] { UpdateType.Message }
            };

            miembro.BotClient.StartReceiving(
                updateHandler: (botClient, update, cancellationToken) => HandleUpdateAsync(botClient, update, cancellationToken, miembro),
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: CancellationToken.None
            );
        }

        Console.WriteLine("Todos los miembros del equipo están listos para recibir mensajes.");
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, MiembroEquipo miembro)
    {
        if (update.Message is not { } message)
            return;
        if (message.Text is not { } messageText)
            return;

        var chatId = message.Chat.Id;

        Console.WriteLine($"Received a '{messageText}' message in chat {chatId} for {miembro.Nombre}.");

        try
        {
            if (messageText.StartsWith("/tarea"))
            {
                _tareasPendientes[chatId] = "Esperando descripción de la tarea...";
               await botClient.SendTextMessageAsync(chatId, "Por favor, describe la tarea a realizar.", cancellationToken: cancellationToken);
            }
            else if (_tareasPendientes.ContainsKey(chatId))
            {
                _tareasPendientes[chatId] = messageText;
                await botClient.SendTextMessageAsync(chatId, "Tarea recibida. Iniciando discusión en equipo...", cancellationToken: cancellationToken);
                await _equipo.DiscutirTareaAsync(messageText);
                _tareasPendientes.Remove(chatId);
            }
           
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al procesar el mensaje para {miembro.Nombre}: {ex.Message}");
            await botClient.SendTextMessageAsync(chatId, "Lo siento, ocurrió un error al procesar tu mensaje. Por favor, intenta de nuevo más tarde.", cancellationToken: cancellationToken);
        }
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}
