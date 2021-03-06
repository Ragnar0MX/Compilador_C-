﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class analisis
    {
        public int id;
        public string palabra;
        public string tipo;


        public void modificar(int id, string palabra,string tipo)
        {
            this.id = id;
            this.palabra = palabra;
            this.tipo = tipo;
        }
        public analisis()
        {
            this.id = 0;
            this.palabra = "";
            this.tipo = "";
        }
}
    class Lexico
    {
        public char[] terminales = {' ', '+', '-', '<', '>', '*', '/', '|', '&', ';', ',', '(', ')', '{', '}', '=', '!','$','\n','\r'};
        public string[] comTerminal = { "<=", ">=", "||", "&&", "==", "!=" };
        public string[] pReservada = { "if", "while", "return", "else" };
        public string[] esp = { "", " ", "\n", "\r" };
        private string cadena;
        private int posicion;
        List<string> elemento = new List<string>();
        List<analisis> analisis = new List<analisis>();
        public Lexico(string cadena)
        {
            this.cadena = cadena;
            posicion = 0;
        }

        public List<analisis> leerCadena()
        {
            char c = ' ';
            string cad = "";
            while (posicion < cadena.Length )
            {
                c = cadena[posicion];
                if (!terminales.Contains(c))
                {
                    cad = cad + c;
                }
                else
                {
                    if (!esp.Contains(cad)){
                        elemento.Add(cad);
                    }
                    analizarTerminal(c);
                    cad = "";
                }
                posicion++;
            }
            analizarLista();
            return analisis;
        }
        public void analizarTerminal(char c){
            string cad = "";
            cad = cad + c;
            char p = ' ';
            if (!esp.Contains(cad))
            {
                if (posicion + 1 < cadena.Length)
                {
                    p = cadena[posicion + 1];
                    if (terminales.Contains(p))
                    {
                        cad = cad + p;
                        if (comTerminal.Contains(cad))
                        {
                            elemento.Add(cad);
                            posicion++;
                        }
                        else
                        {
                            cad = "";
                            cad = cad + c;
                            elemento.Add(cad);
                        }
                    }
                    else
                    {
                        cad = "";
                        cad = cad + c;
                        elemento.Add(cad);
                    }

                }
                else
                    elemento.Add(cad);
            }
        }

        public string[] tipo = { "int", "float", "void" };//4
        public string[] opsum = { "+", "-" };//5
        public string[] opmul = { "*", "/" };//6
        public string[] oprelac = { "<", "<=",">",">=" };//7
        public string[] opigualdad = { "==", "!=" };//11
        private void analizarLista() {
            int i;
            string p;
            analisis anl = new analisis();
            for (i = 0; i < elemento.Count(); i++)
            {
                p = elemento[i];
                bool id = true;
                foreach(char l in p)
                {
                    if (!Char.IsLetterOrDigit(l))
                    {
                        id = false;
                        break;
                    }
                }
                if (id)
                {
                    p = p + "#0#identificador";
                    elemento[i] = p;
                }
                else if (p == "if")
                {
                    p = p + "#19#if";
                    elemento[i] = p;
                }
                else if(p == "else")
                {
                    p = p + "#22#else";
                    elemento[i] = p;
                }
                else if (p == "while")
                {
                    p = p + "#20#while";
                    elemento[i] = p;
                }
                else if (p == "return")
                {
                    p = p + "#21#return";
                    elemento[i] = p;
                }
                else if (p == "$")
                {
                    p = p + "#2#$";
                    elemento[i] = p;
                }
                else if (p == "=")
                {
                    p = p + "#18#=";
                    elemento[i] = p;
                }
                else if (p == "}")
                {
                    p = p + "#17#}";
                    elemento[i] = p;
                }
                else if (p == "{")
                {
                    p = p + "#16#{";
                    elemento[i] = p;
                }
                else if (p == ")")
                {
                    p = p + "#15#)";
                    elemento[i] = p;
                }
                else if (p == "(")
                {
                    p = p + "#14#(";
                    elemento[i] = p;
                }
                else if (p == ",")
                {
                    p = p + "#13#,";
                    elemento[i] = p;
                }
                else if (p == ";")
                {
                    p = p + "#12#if";
                    elemento[i] = p;
                }
                else if (opigualdad.Contains(p))
                {
                    p = p + "#11#opIgualdad";
                    elemento[i] = p;
                }
                else if (p == "!")
                {
                    p = p + "#10#opNot";
                    elemento[i] = p;
                }
                else if (p == "&&")
                {
                    p = p + "#9#opAnd";
                    elemento[i] = p;
                }
                else if (p == "||")
                {
                    p = p + "#8#opOr";
                    elemento[i] = p;
                }
                else if (oprelac.Contains(p))
                {
                    p = p + "#7#opRelac";
                    elemento[i] = p;
                }
                else if (opmul.Contains(p))
                {
                    p = p + "#6#opMul";
                    elemento[i] = p;
                }
                else if (opsum.Contains(p))
                {
                    p = p + "#1#opSum";
                    elemento[i] = p;
                }
                else if (tipo.Contains(p))
                {
                    p = p + "#4#tipo";
                    elemento[i] = p;
                }
                else
                {
                    p = p + "#-1#error";
                    elemento[i] = p;
                }
                int numero;
                float numero2;
                bool result = false;
                bool result2 = false;
                try
                {
                    result = int.TryParse(p, out numero);
                    result2 = float.TryParse(p, out numero2);
                }
                catch { };
                if (result2)
                {
                    elemento[i] = p + "#2#real";
                }
                if (result)
                {
                    elemento[i] = p + "#19#entero";
                }
            }
            analitic();
        }


        private void analitic()
        {
            string[] p;
            for(int i = 0; i < elemento.Count(); i++)
            {
                analisis anl = new analisis();
                p = elemento[i].Split('#');
                anl.modificar(Convert.ToInt32(p[1]), p[0], p[2]);
                analisis.Add(anl);
            }
        }
    }
}

