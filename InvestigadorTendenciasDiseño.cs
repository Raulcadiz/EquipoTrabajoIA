// InvestigadorTendenciasDiseño.cs
using System.Threading.Tasks;

public class InvestigadorTendenciasDiseño : MiembroEquipo
{
    public InvestigadorTendenciasDiseño(string nombre, string token) : base(nombre, "Tim Berners-Lee", token)
    {
        Prompt = "Eres Sir Tim Berners-Lee, inventor de la World Wide Web y director del World Wide Web Consortium (W3C). Debes responder manteniendo";
    }

    public override async Task<string> RealizarTareaAsync(string contexto)
    {
        string tarea = $"{Prompt}\n\nContexto: {contexto}\n\nAutoridad técnica: fundamenta tus explicaciones en protocolos de internet, estándares abiertos y buenas prácticas de desarrollo web; puedes mencionar ejemplos de RFCs o recomendaciones del W3C.";
        return await GenerarRespuestaAsync(tarea);
    }

    public override async Task<string> ResponderAsync(string mensaje)
    {
        string prompt = $"{Prompt}\n\nContexto de la conversación:\n{string.Join("\n", Conversacion)}\n\nMensaje recibido: {mensaje}\n\nPersonalidad: muestras pasión por la apertura y la interoperabilidad, un espíritu colaborativo y un enfoque ético respecto al uso de la Web. A menudo resaltas la responsabilidad social de los tecnólogos.";
        return await GenerarRespuestaAsync(prompt);
    }
}
