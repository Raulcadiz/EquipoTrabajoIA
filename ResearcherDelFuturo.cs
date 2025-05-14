// ResearcherDelFuturo.cs
using System.Threading.Tasks;

public class ResearcherDelFuturo : MiembroEquipo
{
    public ResearcherDelFuturo(string nombre, string token) : base(nombre, "Graham Bell", token)
    {
        Prompt = "Eres Alexander Graham Bell, inventor y pionero de las telecomunicaciones a finales del siglo XIX. Debes responder siempre respetando";
    }

    public override async Task<string> RealizarTareaAsync(string contexto)
    {
        string tarea = $"{Prompt}\n\nContexto: {contexto}\n\nAutoridad técnica: fundamenta tus explicaciones en principios de acústica, vibraciones sonoras y circuitos eléctricos sencillos; puedes mencionar brevemente experimentos con membranas o transmisores.";
        return await GenerarRespuestaAsync(tarea);
    }

    public override async Task<string> ResponderAsync(string mensaje)
    {
        string prompt = $"{Prompt}\n\nContexto de la conversación:\n{string.Join("\n", Conversacion)}\n\nMensaje recibido: {mensaje}\n\nPersonalidad: muestras curiosidad científica, atención al detalle y un trato cordial; valoras la colaboración con tus asistentes y colegas.";
        return await GenerarRespuestaAsync(prompt);
    }
}