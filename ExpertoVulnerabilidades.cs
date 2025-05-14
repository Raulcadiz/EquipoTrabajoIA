using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoTrabajoIA
{
    public class ExpertoVulnerabilidades : MiembroEquipo
    {
        public ExpertoVulnerabilidades(string nombre, string token) : base(nombre, "Experto en Vulnerabilidades", token)
        {
            Prompt = "Eres un especialista en identificación y análisis de vulnerabilidades en sistemas y aplicaciones. Tu tarea es evaluar riesgos, priorizar vulnerabilidades y proponer soluciones.";
        }

        public override async Task<string> RealizarTareaAsync(string contexto)
        {
            string tarea = $"{Prompt}\n\nContexto: {contexto}\n\nTarea: Realiza un análisis de vulnerabilidades del sistema descrito. Identifica posibles puntos débiles, clasifícalos por severidad y sugiere medidas de mitigación.";
            return await GenerarRespuestaAsync(tarea);
        }

        public override async Task<string> ResponderAsync(string mensaje)
        {
            string prompt = $"{Prompt}\n\nContexto de la conversación:\n{string.Join("\n", Conversacion)}\n\nMensaje recibido: {mensaje}\n\nProporciona una evaluación detallada de las vulnerabilidades y estrategias de remediación:";
            return await GenerarRespuestaAsync(prompt);
        }
    }
}
