using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        List<string> palabras_reservadas = new List<string>();
        List<string> simbolos = new List<string>();
        List<string> operadores = new List<string>();
        List<string> coincidencias = new List<string>();
        lectura_archivos p = new lectura_archivos();
        public Form1()
        {
            InitializeComponent();
            p.leer(palabras_reservadas, operadores, simbolos);
        }


        private void analizar_cadena()
        {
            Lexico querer = new Lexico(textBox1.Text);
            List<string> elemento = querer.leerCadena();
            
            for (int i = 0; i < elemento.Count(); i++)
            {
                string[] palabra = elemento[i].Split('#');
                dataGridView1.Rows.Add(palabra[0], palabra[1], palabra[2]);
            }
         
            /*
            foreach (string palabra in texto)
            {
                int numero = 0;
                float numero2 = 0;
                bool result=false;
                bool result2=false;
                try
                {
                    result= int.TryParse(palabra, out numero);
                    result2 = float.TryParse(palabra, out numero2);
                } catch { };

                if (palabras_reservadas.Contains(palabra))
                    coincidencias.Add(palabra + ":\t es palabra reservada.");
                else if (simbolos.Contains(palabra))
                    coincidencias.Add(palabra + ":\t es un simbolo.");
                else if (operadores.Contains(palabra))
                    coincidencias.Add(palabra + ":\t es un operador.");
                else if (result)
                    coincidencias.Add(palabra + ":\t es un numero.");
                else if (result2)
                    coincidencias.Add(palabra + ":\t es un real");
                else if(palabra!= "")
                    coincidencias.Add(palabra + ":\t es un identificador.");
            }*/

        }


        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                dataGridView1.Rows.Clear();
                analizar_cadena();
            }
        }

        
    }
}
