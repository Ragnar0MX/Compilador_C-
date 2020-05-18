using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class lectura_archivos
    {
        public void leer(List<string> X, List<string> Y, List<string> Z)
        {
            string[] archivos = { @"C:\Users\Moises Rafael\source\repos\WindowsFormsApp3\palabras_reservadas.txt", @"C:\Users\Moises Rafael\source\repos\WindowsFormsApp3\operadores.txt", @"C:\Users\Moises Rafael\source\repos\WindowsFormsApp3\simbolos.txt" };
            for(int i =0;i<3;i++){
                string[] lines = System.IO.File.ReadAllLines(archivos[i]);
                foreach (string line in lines)
                {
                    if (i == 0)
                        X.Add(line);
                    if (i == 1)
                        Y.Add(line);
                    if (i == 2)
                        Z.Add(line);
                }
            }
        }
    }
}
