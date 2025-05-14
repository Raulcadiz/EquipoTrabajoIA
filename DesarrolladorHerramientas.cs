using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoTrabajoIA
{
    public class DesarrolladorHerramientas : MiembroEquipo
    {
        public DesarrolladorHerramientas(string nombre, string token) : base(nombre, "Desarrollador de Herramientas", token)
        {
            Prompt = "Eres un desarrollador especializado en crear herramientas de ciberseguridad. Tu enfoque está en desarrollar scripts y programas para pruebas de fuerza bruta, análisis de vulnerabilidades y otras tareas de seguridad.";
        }

        public override async Task<string> RealizarTareaAsync(string contexto)
        {
            string tarea = $"{Prompt}\n\nContexto: {contexto}\n\nTarea: Diseña un script o herramienta para automatizar las pruebas de fuerza bruta en el escenario descrito. Incluye consideraciones de eficiencia y manejo de errores.";
            return await GenerarRespuestaAsync(tarea);
        }

        public override async Task<string> ResponderAsync(string mensaje)
        {
            string prompt = $"{Prompt}\n\nContexto de la conversación:\n{string.Join("\n", Conversacion)}\n\nMensaje recibido: {mensaje}\n\nProporciona un diseño técnico o pseudocódigo para la herramienta solicitada:";
            return await GenerarRespuestaAsync(prompt);
        }
    }
}
