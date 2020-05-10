using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class analizador_lr1
    {
        public void ejemplo_1()
        {
            Stack<int> pila = new Stack<int>();

            pila.Push(2);
            pila.Push(3);
            pila.Push(4);
            pila.Push(5);
            mostrar(pila);
            Console.WriteLine("Pila.top=" + pila.Peek());
            Console.WriteLine("Pila.top=" + pila.Peek());
            Console.WriteLine("pila pop:" + pila.Pop());
            Console.WriteLine("pila pop:" + pila.Pop());

        }

        private void mostrar(Stack<int> pila)
        {
            string cadena="pila el tope se encuentra más a la izquierda: ";
            foreach(int dato in pila)
            {
                cadena += Convert.ToString(dato)+"<-";
            }
            Console.WriteLine(cadena);
        }

        public void ejemplo_2(string texto)
        {
            Console.WriteLine("La entrada del ejemplo 2 se escribe en el label de la pagina principal");
            if (texto != null)
            {
                Lexico querer = new Lexico(texto);
                List<string> elemento = querer.leerCadena();
                for (int i = 0; i < elemento.Count(); i++)
                {
                    string[] palabra = elemento[i].Split('#');
                    Console.WriteLine(palabra[0] + " , " + palabra[2]);
                }
            }
        }

        
        public void ejemplo_3()
        {
            int [,] tabla = new int[3, 3] { {2,0,1 }, {0,-1,0}, {0,-2,0} };
            Stack<string> pila = new Stack<string>();
            int fila=0, columna=0, accion=0;
            bool aceptación = false;
            Lexico lexico = new Lexico("a$");
            pila.Push("1");
            pila.Push("0");

            List<string> elemento = lexico.leerCadena();
            string[] palabra = elemento[0].Split('#');
                try
                {
                    fila = Convert.ToInt32(pila.Peek());
                    columna = Convert.ToInt32(palabra[1]);
                }
                catch { };

                accion = tabla[fila, columna];

                mostrar2(pila);
                Console.WriteLine("entrada: " + palabra[0]);
                Console.WriteLine("accion: " + accion);
                pila.Push(palabra[1]);
                pila.Push(Convert.ToString(accion));
            if (accion == 2)
            {
                palabra = elemento[1].Split('#');
                try
                {
                    fila = Convert.ToInt32(pila.Peek());
                    columna = Convert.ToInt32(palabra[1]);
                }
                catch { };

                accion = tabla[fila, columna];
                mostrar2(pila);
                Console.WriteLine("entrada: " + palabra[0]);
                Console.WriteLine("accion: " + accion);

            }
            if(accion == -2)
            {
                pila.Pop();
                pila.Pop();
                try
                {
                    fila = Convert.ToInt32(pila.Peek());
                    columna = 2;
                }
                catch { };
                accion = tabla[fila, columna];
                //transicion
                pila.Push(Convert.ToString(2));
                pila.Push(Convert.ToString(accion));
                mostrar2(pila);

                Console.WriteLine("entrada: "+ palabra[0]);
                Console.WriteLine("accion: " + accion);

                    try
                    {
                        fila = Convert.ToInt32(pila.Peek());
                        columna =Convert.ToInt32(palabra[1]);
                    }
                    catch { };
                    accion = tabla[fila, columna];
                if (accion == -1) 
                { 
                    mostrar2(pila);

                    Console.WriteLine("entrada: " + palabra[0]);
                    Console.WriteLine("accion: " + accion);
                    aceptación = accion == -1;
                    if (aceptación)
                        Console.WriteLine("aceptacion");
                }
            }

        }

        public void ejercicio_1()
        {
            int[,] tabla = new int[5, 4] { {2,0,0,1}, { 0, 0, -1, 0 }, { 0, 3, 0, 0 }, { 4, 0, 0, 0 }, { 0, 0, -2, 0 } };
            Stack<string> pila = new Stack<string>();
            int pos = 0;//indica la posicion actual del analizador lexico
            int fila = 0, columna = 0, accion = 0;
            bool aceptación = false;
            Lexico lexico = new Lexico("a+b$");
            pila.Push("2");
            pila.Push("0");
            List<string> elemento = lexico.leerCadena();
            string[] palabra = elemento[0].Split('#');
            fila = Convert.ToInt32(pila.Peek());
            columna = Convert.ToInt32(palabra[1]);
            accion = tabla[fila, columna];

            mostrar2(pila);
            accionEntrada(accion, palabra[0]);
            while (!aceptación || accion == 0)
            {
                if (accion > 0)
                {
                    pila.Push(palabra[1]);
                    pila.Push(Convert.ToString(accion));
                    if(pos+1<elemento.Count())
                        pos++;
                    palabra = elemento[pos].Split('#');
                    fila = Convert.ToInt32(pila.Peek());
                    columna = Convert.ToInt32(palabra[1]);
                    accion = tabla[fila, columna];
                    mostrar2(pila);
                    accionEntrada(accion, palabra[0]);
                }
                if (accion < 0)
                {
                    if (accion == -2)
                    {
                        pila.Pop();
                        pila.Pop();
                        pila.Pop();
                        pila.Pop();
                        pila.Pop();
                        pila.Pop();
                        fila = Convert.ToInt32(pila.Peek());
                        columna = 3;
                        accion = tabla[fila, columna];
                        pila.Push("4");
                        pila.Push(Convert.ToString(accion));
                        mostrar2(pila);
                        accionEntrada(accion, palabra[0]);
                    }
                    if (accion == -1)
                    {
                        fila = Convert.ToInt32(pila.Peek());
                        columna = Convert.ToInt32(palabra[1]);
                        accion = tabla[fila, columna];
                        mostrar2(pila);
                        accionEntrada(accion, palabra[0]);
                        aceptación = accion == -1;
                        if (aceptación)
                            Console.WriteLine("aceptacion");
                    }
                }
            }

        }
        
        public void ejercicio_2(string cadena)
        {
            int[,] tabla = new int[5, 4] { { 2, 0, 0, 1 }, { 0, 0, -1, 0 }, { 0, 3, -3, 0 }, { 2, 0, 0, 4 }, { 0, 0, -2, 0 } };
            int[] idRegla = new int[2] { 2, 2 };
            int[] lonRegla = new int[2] { 3, 0 };
            Stack<string> pila = new Stack<string>();
            int pos = 0;//indica la posicion actual del analizador lexico
            int fila = 0, columna = 0, accion = 0;
            bool aceptación = false;
            Lexico lexico = new Lexico(cadena+"$");
            pila.Push("2");
            pila.Push("0");
            List<string> elemento = lexico.leerCadena();
            string[] palabra = elemento[0].Split('#');
            fila = Convert.ToInt32(pila.Peek());
            columna = Convert.ToInt32(palabra[1]);
            accion = tabla[fila, columna];

            mostrar2(pila);
            accionEntrada(accion, palabra[0]);
            while (!aceptación || accion == 0)
            {
                if (accion > 0)
                {
                    if (palabra[1] == "0")
                        lonRegla[0] =+ 1;
                    if (palabra[1] == "1")
                        lonRegla[1] += 1;
                    pila.Push(palabra[1]);
                    pila.Push(Convert.ToString(accion));
                    if (pos + 1 < elemento.Count())
                        pos++;
                    palabra = elemento[pos].Split('#');
                    fila = Convert.ToInt32(pila.Peek());
                    columna = Convert.ToInt32(palabra[1]);
                    accion = tabla[fila, columna];
                    mostrar2(pila);
                    accionEntrada(accion, palabra[0]);
                }
                if(accion == 0)
                {
                    Console.WriteLine("error");
                    break;
                }
                if (accion < 0)
                {
                    if (accion == -3)
                    {
                        pila.Pop();
                        pila.Pop();
                        fila = Convert.ToInt32(pila.Peek());
                        columna = 3;
                        accion = tabla[fila, columna];
                        pila.Push("4");
                        pila.Push(Convert.ToString(accion));
                        mostrar2(pila);
                        accionEntrada(accion, palabra[0]);

                    }
                    if (accion == -2)
                    {
                        pila.Pop();
                        pila.Pop();
                        pila.Pop();
                        pila.Pop();
                        pila.Pop();
                        pila.Pop();
                        pila.Pop();
                        pila.Pop();
                        fila = Convert.ToInt32(pila.Peek());
                        columna = 3;
                        accion = tabla[fila, columna];
                        pila.Push("4");
                        pila.Push(Convert.ToString(accion));
                        mostrar2(pila);
                        accionEntrada(accion, palabra[0]);
                    }
                    if (accion == -1)
                    {
                        fila = Convert.ToInt32(pila.Peek());
                        columna = Convert.ToInt32(palabra[1]);
                        accion = tabla[fila, columna];
                        mostrar2(pila);
                        accionEntrada(accion, palabra[0]);
                        aceptación = accion == -1;
                        if (aceptación)
                            Console.WriteLine("aceptacion");
                    }
                }
            }
        }
        private void accionEntrada(int accion, string entrada)
        {
            Console.WriteLine("Accion :" + accion);
            Console.WriteLine("entrada:" + entrada);
        }
        private void mostrar2(Stack<string> pila)
        {
            string cadena = "";
            foreach (string dato in pila)
            {
                cadena += dato + "<-";
            }
            Console.WriteLine(cadena);
        }
    }
}
