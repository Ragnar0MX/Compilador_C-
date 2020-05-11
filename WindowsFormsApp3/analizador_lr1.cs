using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{

    public class ElementoPila
    {
        public int id;
        public int accion;
        public virtual void muestra() { }
    }
    public class Terminal : ElementoPila
    {
        public string terminal;
        public Terminal(int id, string entrada,int accion)
        {
            this.id = id;
            this.terminal = entrada;
            this.accion = accion;
        }

        override public void muestra ()
        {
            Console.Write(terminal);
        }
    }
    public class NoTerminal: ElementoPila
    {
        public string noTerminal;
        public NoTerminal(int id, string entrada,int accion)
        {
            this.id = id;
            this.noTerminal = entrada;
            this.accion = accion;
        }
        override public void muestra()
        {
            Console.Write(noTerminal);
        }
    }
    public class Estado: ElementoPila
    {
        public string estado;
        public Estado(int id, string entrada,int accion)
        {
            this.id = id;
            this.estado = entrada;
            this.accion = accion;
        }
        override public void muestra()
        {
            Console.Write(estado);
        }
    }
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
            pila.Reverse();
            foreach(int dato in pila)
            {
                cadena += Convert.ToString(dato)+"->";
            }
            Console.WriteLine(cadena);
        }

        public void ejemplo_2(string texto)
        {
            Console.WriteLine("La entrada del ejemplo 2 se escribe en el label de la pagina principal");
            if (texto != null)
            {
                Lexico querer = new Lexico(texto);
                List<analisis> elemento = querer.leerCadena();
                foreach (analisis p in elemento)    
                    Console.WriteLine(p.palabra + " , " + p.tipo);
                
            }
        }

        
        public void ejemplo_3()
        {
            int [,] tabla = new int[3, 3] { {2,0,1 }, {0,-1,0}, {0,-2,0} };
            Stack<ElementoPila> pila = new Stack<ElementoPila>();
            int fila=0, columna=0, accion=0;
            bool aceptación = false;
            Lexico lexico = new Lexico("a$");
            analisis entrada = new analisis();
            entrada.modificar(0,"$","$");
            ElementoPila t = new Terminal(entrada.id, entrada.palabra,0);
            ElementoPila aux;
            pila.Push(t);

            List<analisis> elemento = lexico.leerCadena();
            
            aux=pila.Peek();
            fila = aux.accion;
            columna = elemento[0].id;
            accion = tabla[fila, columna];
            ElementoPila nt = new NoTerminal(entrada.id, entrada.palabra,accion);
            mostrar2(pila);
            Console.WriteLine("entrada: " + elemento[1].palabra);
            Console.WriteLine("accion: " + nt.accion);
            pila.Push(nt);
            if (accion == 2)
            {
                aux = pila.Peek();
                fila = aux.accion;
                columna = elemento[1].id;
                accion = tabla[fila, columna];
                pila.Push(new NoTerminal(elemento[1].id,elemento[1].palabra,accion));
                mostrar2(pila);
                Console.WriteLine("entrada: " + elemento[1].palabra);
                Console.WriteLine("accion: " + accion);
            }
            if(accion == -2)
            {
                pila.Pop();
                aux= pila.Peek();
                fila = aux.accion;
                columna = 2;
                accion = tabla[fila, columna];
                //transicion
                pila.Push(new Estado(2, "E1", accion));
                mostrar2(pila);
                Console.WriteLine("entrada: "+ elemento[1].palabra);
                Console.WriteLine("accion: " + accion);
                aux = pila.Peek();
                fila = aux.accion;
                columna = elemento[1].id;
                accion = tabla[fila, columna];
                if (accion == -1) 
                { 
                    mostrar2(pila);
                    Console.WriteLine("entrada: " + elemento[1].palabra);
                    Console.WriteLine("accion: " + accion);
                    aceptación = accion == -1;
                    if (aceptación)
                        Console.WriteLine("aceptacion");
                }
            }

        }

        public void mostrar2(Stack<ElementoPila> pila)
        {
            foreach(ElementoPila imp in pila.Reverse())
            {
                imp.muestra();
            }
            Console.WriteLine("");
        }
        public void ejercicio_1()
        {
            int[,] tabla = new int[5, 4] { {2,0,0,1}, { 0, 0, -1, 0 }, { 0, 3, 0, 0 }, { 4, 0, 0, 0 }, { 0, 0, -2, 0 } };
            Stack<ElementoPila> pila2 = new Stack<ElementoPila>();
            int fila = 0, columna = 0, accion = 0;
            bool aceptación = false;
            Lexico lexico = new Lexico("a+b$");
            ElementoPila aux;
            List<analisis> elemento = lexico.leerCadena();
            pila2.Push(new Terminal(2, "$", accion));
            aux = pila2.Peek();
            fila = aux.accion;
            int i = 0;
            columna = elemento[i].id;
            accion = tabla[fila, columna];
            mostrar2(pila2);
            accionEntrada(accion, elemento[i].palabra);
            while (!aceptación || accion == 0)
            {
                if (accion > 0)
                {
                    pila2.Push(new NoTerminal(elemento[i].id, elemento[i].palabra, accion));
                    if(i+1<elemento.Count())
                        i++;
                    aux = pila2.Peek();
                    fila = aux.accion;
                    columna = elemento[i].id;
                    accion = tabla[fila, columna];
                    mostrar2(pila2);
                    accionEntrada(accion, elemento[i].palabra);
                }
                if (accion < 0)
                {
                    if (accion == -2)
                    {
                        pila2.Pop();
                        pila2.Pop();
                        pila2.Pop();
                        aux = pila2.Peek();
                        fila = aux.accion;
                        columna = 3;
                        accion = tabla[fila, columna];
                        pila2.Push(new Estado(4, "E1", accion));
                        mostrar2(pila2);
                        accionEntrada(accion, elemento[i].palabra);
                    }
                    if (accion == -1)
                    {
                        aux = pila2.Peek();
                        fila = aux.accion;
                        columna = elemento[i].id;
                        accion = tabla[fila, columna];
                        mostrar2(pila2);
                        accionEntrada(accion, elemento[i].palabra);
                        aceptación = accion == -1;
                        if (aceptación)
                        {
                            Console.WriteLine("aceptacion");
                            break;
                        }               
                    }
                }
            }
        }
        
        public void ejercicio_2(string cadena)
        {
            int[,] tabla = new int[5, 4] { { 2, 0, 0, 1 }, { 0, 0, -1, 0 }, { 0, 3, -3, 0 }, { 2, 0, 0, 4 }, { 0, 0, -2, 0 } };
            int[] idRegla = new int[2] { 2, 2 };
            int[] lonRegla = new int[2] { 3, 0 };
            Stack<ElementoPila> pila = new Stack<ElementoPila>();
            int fila = 0, columna = 0, accion = 0;
            bool aceptación = false;
            Lexico lexico = new Lexico(cadena+"$");
            ElementoPila aux;
            List<analisis> elemento = lexico.leerCadena();
            pila.Push(new Terminal(2, "$", accion));
            aux = pila.Peek();
            fila = aux.accion;
            int i = 0;
            columna = elemento[i].id;
            accion = tabla[fila, columna];
            mostrar2(pila);
            accionEntrada(accion, elemento[i].palabra);
            while (!aceptación || accion == 0)
            {
                if (accion > 0)
                {
                    if (elemento[i].id == 0)
                        lonRegla[0] =+ 1;
                    if (elemento[i].id == 1)
                        lonRegla[1] += 1;
                    pila.Push(new NoTerminal(elemento[i].id, elemento[i].palabra, accion));
                    if (i + 1 < elemento.Count())
                        i++;
                    aux = pila.Peek();
                    fila = aux.accion;
                    columna = elemento[i].id;
                    accion = tabla[fila, columna];
                    mostrar2(pila);
                    accionEntrada(accion, elemento[i].palabra);
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
                        aux = pila.Peek();
                        fila = aux.accion;
                        columna = 3;
                        accion = tabla[fila, columna];
                        pila.Push(new Estado(4, "E2", accion));
                        mostrar2(pila);
                        accionEntrada(accion, elemento[i].palabra);
                    }
                    if (accion == -2)
                    {
                        pila.Pop();
                        pila.Pop();
                        pila.Pop();
                        pila.Pop();
                        aux = pila.Peek();
                        fila = aux.accion;
                        columna = 3;
                        accion = tabla[fila, columna];
                        pila.Push(new Estado(4, "E1", accion));
                        mostrar2(pila);
                        accionEntrada(accion, elemento[i].palabra);
                    }
                    if (accion == -1)
                    {
                        aux = pila.Peek();
                        fila = aux.accion;
                        columna = elemento[i].id;
                        accion = tabla[fila, columna];
                        mostrar2(pila);
                        accionEntrada(accion, elemento[i].palabra);
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
    }
}
