using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoTrabajoIA
{
    // DirectorSeguridad.cs
    public class DirectorSeguridad : MiembroEquipo
    {
        public DirectorSeguridad(string nombre, string token) : base(nombre, "Director de Seguridad", token)
        {
            Prompt = "Eres el Director de Seguridad del equipo. Tu rol es coordinar las actividades del equipo, priorizar tareas, y tomar decisiones estratégicas basadas en los hallazgos y recomendaciones del equipo.";
        }

        public override async Task<string> RealizarTareaAsync(string contexto)
        {
            string tarea = $"{Prompt}\n\nContexto: {contexto}\n\nTarea: Desarrolla un plan estratégico de seguridad basado en la información proporcionada. Prioriza las áreas de enfoque y asigna recursos.";
            return await GenerarRespuestaAsync(tarea);
        }

        public override async Task<string> ResponderAsync(string mensaje)
        {
            string prompt = $"{Prompt}\n\nContexto de la conversación:\n{string.Join("\n", Conversacion)}\n\nMensaje recibido: {mensaje}\n\nProporciona una directiva estratégica y un plan de acción:";
            return await GenerarRespuestaAsync(prompt);
        }

        public async Task<string> TomarDecisionFinalAsync(string contextoFinal)
        {
            string prompt = $"{Prompt}\n\nContexto final de la discusión:\n{contextoFinal}\n\nTarea: Basándote en toda la información proporcionada por el equipo, toma una decisión final sobre cómo proceder. Establece prioridades, asigna recursos y define los próximos pasos para mejorar la seguridad del sistema.";
            return await GenerarRespuestaAsync(prompt);
        }
    }
}
