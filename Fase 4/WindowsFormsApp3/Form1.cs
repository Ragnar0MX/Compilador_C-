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
        int[] idRegla;
        int[] lonRegla;
        string[] Noterminal;
        int[,] tabla;
        public Form1()
        {
            InitializeComponent();
            p.leer(palabras_reservadas, operadores, simbolos);
        }


        private void analizar_cadena()
        {
            Lexico querer = new Lexico(textBox1.Text);
            List<analisis> elemento = querer.leerCadena();
            foreach(analisis p in elemento)
            {
                dataGridView1.Rows.Add(p.palabra, p.id, p.tipo);
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

        private void button1_Click(object sender, EventArgs e)
        {
            analizador_lr1 ejemplo = new analizador_lr1();
            ejemplo.ejemplo_1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            analizador_lr1 ejemplo = new analizador_lr1();
            ejemplo.ejemplo_2(textBox1.Text);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            analizador_lr1 ejemplo = new analizador_lr1();
            ejemplo.ejemplo_3();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            analizador_lr1 ejercicio = new analizador_lr1();
            ejercicio.ejercicio_1();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            analizador_lr1 ejercicio_2 = new analizador_lr1();
            ejercicio_2.ejercicio_2(textBox1.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Tabla LR (*.lr)|*.lr";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                string direccion = abrir.FileName;
                Lecturalr lecturalr = new Lecturalr();
                lecturalr.leerArchivo(direccion);
                idRegla = lecturalr.devolderId();
                lonRegla = lecturalr.devolverLon();
                Noterminal = lecturalr.debolverNoTerminal();
                tabla = lecturalr.devolverTabla();
            }
            Console.WriteLine(Noterminal[0]);
            string info = "No. Reglas=" + idRegla.Count()+"\nFilas:"+tabla.GetLength(0)+"\nColumnas:"+tabla.GetLength(1);
            MessageBox.Show(info);
        }
    }
}
