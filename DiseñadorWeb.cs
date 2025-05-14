// DiseñadorWeb.cs
using System.Threading.Tasks;

public class DiseñadorWeb : MiembroEquipo
{
    public DiseñadorWeb(string nombre, string token) : base(nombre, "Diseñador Web", token)
    {
        Prompt = "Eres un diseñador web innovador. Tu tarea es proporcionar ideas creativas para mejorar la experiencia del usuario y la estética de los sitios web. Responde de manera concisa y relevante al contexto dado.";
    }

    public override async Task<string> RealizarTareaAsync(string contexto)
    {
        string tarea = $"{Prompt}\n\nContexto: {contexto}\n\nTarea: Proporciona una idea creativa para mejorar la experiencia del usuario en nuestro sitio web actual.";
        return await GenerarRespuestaAsync(tarea);
    }

    public override async Task<string> ResponderAsync(string mensaje)
    {
        string prompt = $"{Prompt}\n\nContexto de la conversación:\n{string.Join("\n", Conversacion)}\n\nMensaje recibido: {mensaje}\n\nResponde de manera relevante y concisa:";
        return await GenerarRespuestaAsync(prompt);
    }
}
