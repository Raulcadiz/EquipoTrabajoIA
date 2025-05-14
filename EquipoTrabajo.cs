// EquipoTrabajoIA.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EquipoTrabajo
{
    public List<MiembroEquipo> Miembros { get; } = new List<MiembroEquipo>();
    private long _groupChatId;

    public void AgregarMiembro(MiembroEquipo miembro)
    {
        Miembros.Add(miembro);
    }

    public void MostrarEquipo()
    {
        Console.WriteLine("Equipo de Trabajo IA:");
        foreach (var miembro in Miembros)
        {
            Console.WriteLine($"- {miembro.Nombre}: {miembro.Rol}");
        }
    }

    public async Task IniciarEquipoAsync(long groupChatId)
    {
        _groupChatId = groupChatId;
        foreach (var miembro in Miembros)
        {
            await miembro.IniciarAsync();
        }
    }

    public async Task DiscutirTareaAsync(string tarea)
    {
        foreach (var miembro in Miembros)
        {
            string respuesta = await miembro.RealizarTareaAsync(tarea);
            await miembro.EnviarMensajeAsync(_groupChatId, respuesta);
            await Task.Delay(2000); // Esperar 2 segundos entre mensajes
        }

        for (int i = 0; i < 3; i++) // 3 rondas de discusión
        {
            foreach (var miembro in Miembros)
            {
                string contexto = string.Join("\n", Miembros.SelectMany(m => m.Conversacion).TakeLast(10));
                string mensaje = await miembro.ResponderAsync(contexto);
                await miembro.EnviarMensajeAsync(_groupChatId, mensaje);
                await Task.Delay(2000);
            }
        }

        await TomarDecisionFinalAsync();
    }

    private async Task TomarDecisionFinalAsync()
    {
        var productorManager = Miembros.Find(m => m is ProductorManager) as ProductorManager;
        if (productorManager != null)
        {
            string contextoFinal = string.Join("\n", Miembros.SelectMany(m => m.Conversacion).TakeLast(20));
            string decisionFinal = await productorManager.TomarDecisionFinalAsync(contextoFinal);
            await productorManager.EnviarMensajeAsync(_groupChatId, decisionFinal);
        }
        else
        {
            Console.WriteLine("Error: No se encontró un Productor Manager en el equipo.");
        }
    }
}