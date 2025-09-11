namespace TuProyecto.Helpers
{
    public static class NumeroHelper
    {
        public static string NumeroALetras(int numero)
        {
            if (numero < 0 || numero > 9999)
                return "Número fuera de rango";

            string[] unidades = { "", "UNO", "DOS", "TRES", "CUATRO", "CINCO", "SEIS", "SIETE", "OCHO", "NUEVE" };
            string[] decenas = { "", "DIEZ", "VEINTE", "TREINTA", "CUARENTA", "CINCUENTA", "SESENTA", "SETENTA", "OCHENTA", "NOVENTA" };
            string[] especiales = { "DIEZ", "ONCE", "DOCE", "TRECE", "CATORCE", "QUINCE", "DIECISÉIS", "DIECISIETE", "DIECIOCHO", "DIECINUEVE" };

            if (numero < 10)
                return unidades[numero];

            if (numero < 20)
                return especiales[numero - 10];

            if (numero < 100)
            {
                int decena = numero / 10;
                int unidad = numero % 10;
                return decenas[decena] + (unidad > 0 ? " Y " + unidades[unidad] : "");
            }

            if (numero == 100)
                return "CIEN";

            if (numero < 200)
                return "CIENTO " + NumeroALetras(numero - 100);

            if (numero < 1000)
            {
                int centenas = numero / 100;
                int resto = numero % 100;
                string[] cientos = { "", "", "DOSCIENTOS", "TRESCIENTOS", "CUATROCIENTOS", "QUINIENTOS", "SEISCIENTOS", "SETECIENTOS", "OCHOCIENTOS", "NOVECIENTOS" };
                return cientos[centenas] + (resto > 0 ? " " + NumeroALetras(resto) : "");
            }

            if (numero == 1000)
                return "MIL";

            if (numero < 2000)
                return "MIL " + NumeroALetras(numero - 1000);

            if (numero < 10000)
            {
                int miles = numero / 1000;
                int resto = numero % 1000;
                return NumeroALetras(miles) + " MIL" + (resto > 0 ? " " + NumeroALetras(resto) : "");
            }

            return "";
        }
    }
}
