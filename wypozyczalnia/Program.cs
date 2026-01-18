using System;
using WypozyczalniaNarciarska;

namespace WypozyczalniaNarciarska
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                var narty = new Narty("Atomic", 40m, 26, 170, TypNart.AllMountain, false, 2020);
                Console.WriteLine(narty.Opis());
                Console.WriteLine($"Koszt 3 dni: {narty.ObliczKoszt(3)} zł");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd Narty: {ex.Message}");
            }

            try
            {
                var snowboard = new Snowboard("Burton", 45m, 28, 150, true, 2019);
                Console.WriteLine(snowboard.Opis());
                Console.WriteLine($"Koszt 2 dni: {snowboard.ObliczKoszt(2)} zł");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd Snowboard: {ex.Message}");
            }

            try
            {
                var buty = new Buty("Salomon", 15m, 42, true, 2021);
                Console.WriteLine(buty.Opis());
                Console.WriteLine($"Koszt 5 dni: {buty.ObliczKoszt(5)} zł");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd Buty: {ex.Message}");
            }

            try
            {
                var zle = new Narty("X", 10m, -1, 50, TypNart.Slalom, false, 2050);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType().Name} - {ex.Message}");
            }
        }
    }
}
