using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoTrabajoIA
{
    public class AnalistaVirus : MiembroEquipo
    {
        public AnalistaVirus(string nombre, string token) : base(nombre, "Analista de Virus y Malware", token)
        {
            Prompt = "Eres un experto en análisis de virus y malware. Tu tarea es examinar código malicioso, entender su funcionamiento y proponer estrategias de defensa y mitigación.";
        }

        public override async Task<string> RealizarTareaAsync(string contexto)
        {
            string tarea = $"{Prompt}\n\nContexto: {contexto}\n\nTarea: Analiza el comportamiento del malware descrito. Identifica sus métodos de propagación, payload, y posibles impactos. Sugiere medidas de protección.";
            return await GenerarRespuestaAsync(tarea);
        }

        public override async Task<string> ResponderAsync(string mensaje)
        {
            string prompt = $"{Prompt}\n\nContexto de la conversación:\n{string.Join("\n", Conversacion)}\n\nMensaje recibido: {mensaje}\n\nProporciona un análisis detallado y recomendaciones de seguridad:";
            return await GenerarRespuestaAsync(prompt);
        }
    }

}
