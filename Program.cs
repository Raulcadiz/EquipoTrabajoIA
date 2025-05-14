// Program.cs
using System;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var equipo = new EquipoTrabajo();

            equipo.AgregarMiembro(new ResearcherDelFuturo("Ana", "7840063099:AAFEFRV0V4So8X8C2gxwLQ2tTQ3dXrFKD6s"));
            equipo.AgregarMiembro(new InvestigadorTendenciasDiseño("Carlos", "7672144104:AAFclClk5Aq2FUX0IyCYh62VvW5yVQNjJvM"));
            equipo.AgregarMiembro(new DiseñadorWeb("Elena", "7650214237:AAGt-SrWRFoBYWP-JBDgrxl8jNBUF1OsmTs"));
            equipo.AgregarMiembro(new Socrates("Sócrates", "7260236175:AAFYIwOLjhfESfpita1zA2GUmsOHBMm-VtY"));
            equipo.AgregarMiembro(new ProductorManager("Pablo", "7587547369:AAHI90vG5vU040noSAm3ns8kZVhZ1GcTHCo"));


            equipo.MostrarEquipo();

            Console.WriteLine("Ingrese el ID del grupo de Telegram:");
            if (!long.TryParse(Console.ReadLine(), out long groupChatId))
            {
                throw new ArgumentException("ID de grupo inválido. Debe ser un número entero.");
            }

            var coordinator = new TelegramCoordinator(equipo);
            await coordinator.IniciarAsync(groupChatId);

            Console.WriteLine("El sistema está en funcionamiento. Presiona Enter para salir.");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en la ejecución del programa: {ex.Message}");
            Console.WriteLine("Presiona Enter para salir.");
            Console.ReadLine();
        }
    }
}