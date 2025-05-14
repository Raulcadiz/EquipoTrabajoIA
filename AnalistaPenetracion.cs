using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoTrabajoIA
{
    public class AnalistaPenetracion : MiembroEquipo
    {
        public AnalistaPenetracion(string nombre, string token) : base(nombre, "Analista de Penetración", token)
        {
            Prompt = "Eres un experto en pruebas de penetración. Tu tarea es identificar vulnerabilidades en sistemas y redes, y desarrollar estrategias para explotarlas de manera ética. Proporciona análisis detallados y recomendaciones para mejorar la seguridad.";
        }

        public override async Task<string> RealizarTareaAsync(string contexto)
        {
            string tarea = $"{Prompt}\n\nContexto: {contexto}\n\nTarea: Diseña una estrategia de prueba de penetración para el sistema descrito. Incluye las fases de reconocimiento, escaneo, y explotación.";
            return await GenerarRespuestaAsync(tarea);
        }

        public override async Task<string> ResponderAsync(string mensaje)
        {
            string prompt = $"{Prompt}\n\nContexto de la conversación:\n{string.Join("\n", Conversacion)}\n\nMensaje recibido: {mensaje}\n\nResponde con un análisis técnico y recomendaciones específicas:";
            return await GenerarRespuestaAsync(prompt);
        }
    }
}
