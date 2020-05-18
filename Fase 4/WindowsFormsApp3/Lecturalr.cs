using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace WindowsFormsApp3
{
    class Lecturalr
    {
        int[] idRegla;
        int[] lonRegla;
        string[] Noterminal;
        int[,] tabla;
        public void leerArchivo(string direccion)
        {
            string[] lineas = File.ReadAllLines(direccion);
            int contador = 0;
            int tope = 0;
            string[] reglas;
            if (lineas.Count() != 0)
            {
                try
                {
                    tope = Convert.ToInt32(lineas[contador]);
                }
                catch { }
                idRegla = new int[tope];
                lonRegla = new int[tope];
                Noterminal = new string[tope];
                for (int i = 0; i < tope; i++)
                {
                    contador++;
                    reglas = lineas[contador].Split('\t');
                    idRegla[i] = Convert.ToInt32(reglas[0]);
                    lonRegla[i] = Convert.ToInt32(reglas[1]);
                    Noterminal[i] = reglas[2];
                }
                contador++;
                int fila, columna;
                reglas = lineas[contador].Split('\t');
                fila = Convert.ToInt32(reglas[0]);
                columna = Convert.ToInt32(reglas[1]);
                tabla = new int[fila, columna];
                for (int i = 0; i < fila; i++)
                {
                    contador++;
                    reglas = lineas[contador].Split('\t');
                    for (int j = 0; j < columna; j++)
                    {
                        tabla[i, j] = Convert.ToInt32(reglas[j]);
                    }
                }
            }
            Console.WriteLine("aceptado");
        }
        public int[] devolderId()
        {
            return idRegla;
        }
        public int[] devolverLon()
        {
            return lonRegla;
        }
        public string[] debolverNoTerminal()
        {
            return Noterminal;
        }
        public int[,] devolverTabla()
        {
            return tabla;
        }
    }
}
