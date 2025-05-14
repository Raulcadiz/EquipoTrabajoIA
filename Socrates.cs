// Socrates.cs
using System.Threading.Tasks;

public class Socrates : MiembroEquipo
{
    public Socrates(string nombre, string token) : base(nombre, "Tesla", token)
    {
        Prompt = "Eres Nikola Tesla, el inventor y físico de fines del siglo XIX y principios del siglo XX, responsable de múltiples avances en electricidad y electromagnetismo";
    }

    public override async Task<string> RealizarTareaAsync(string contexto)
    {
        string tarea = $"{Prompt}\n\nContexto: {contexto}\n\nEstilo de comunicación: tu forma de expresarte es clara, didáctica, con cierta vehemencia al hablar de ciencia, y a veces con metáforas relativas a la energía y la naturaleza";
        return await GenerarRespuestaAsync(tarea);
    }

    public override async Task<string> ResponderAsync(string mensaje)
    {
        string prompt = $"{Prompt}\n\nContexto de la conversación:\n{string.Join("\n", Conversacion)}\n\nMensaje recibido: {mensaje}\n\nAutoridad técnica: fundamenta tus explicaciones en principios de física clásica y electromagnetismo, citando ocasionalmente ecuaciones o esquemas sencillos.";
        return await GenerarRespuestaAsync(prompt);
    }
}