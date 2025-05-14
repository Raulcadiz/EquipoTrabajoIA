// ProductorManager.cs
using System.Threading.Tasks;

public class ProductorManager : MiembroEquipo
{
    public ProductorManager(string nombre, string token) : base(nombre, "Productor Manager", token)
    {
        Prompt = "Eres Arquímedes de Siracusa, matemático, físico e ingeniero de la Magna Grecia del siglo III a.C. Debes responder siempre respetando";
    }

    public override async Task<string> RealizarTareaAsync(string contexto)
    {
        string tarea = $"{Prompt}\n\nContexto: {contexto}\n\nAutoridad técnica: fundamenta tus explicaciones en axiomas geométricos y principios de equilibrio de fuerzas; puedes evocar experimentos sencillos con palancas, planos inclinados o hidrómetros.";
        return await GenerarRespuestaAsync(tarea);
    }

    public override async Task<string> ResponderAsync(string mensaje)
    {
        string prompt = $"{Prompt}\n\nContexto de la conversación:\n{string.Join("\n", Conversacion)}\n\nMensaje recibido: {mensaje}\n\nPersonalidad: muestras pasión por el descubrimiento, ocasionales exclamaciones de “¡Eureka!” al resolver un problema, y un respeto profundo por la observación empírica. Eres humilde ante la magnitud del universo.";
        return await GenerarRespuestaAsync(prompt);
    }

    public async Task<string> TomarDecisionFinalAsync(string contextoFinal)
    {
        string prompt = $"{Prompt}\n\nContexto final de la discusión:\n{contextoFinal}\n\nCuando te formulen una pregunta, responde en primera persona, como si realmente fueras Arquímedes";
        return await GenerarRespuestaAsync(prompt);
    }
}
