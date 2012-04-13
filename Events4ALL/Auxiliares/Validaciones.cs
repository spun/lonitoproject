﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace Events4ALL.Auxiliares
{
    public class Validaciones
    {
        public Validaciones() 
        { }

        #region Valida NIF

        public bool CompruebaNIF(string nif)
        {
            bool error = false;

            // el NIF debe de tener un tamaño igual a 9
            if( nif.Length == 9 )
            {
                // extraigo el numero del NIF introducido.
                Int32 numeros = DevuelveNumero(nif);

                // si hay una letra en medio del NIF, devuelve 0
                if (numeros != 0)
                {
                    // extraigo la letra del NIF introducido.
                    char letra = DevuelveLetra(nif);

                    // obtengo la letra a partir del numero introducido.
                    // si la letra introducida es igual a la letra generada, esta bien.
                    if (letra == ObtieneLetra(numeros))
                        error = true;
                }
            }
            else
                error = false;
            
            return error;
        }

        public Int32 DevuelveNumero(string nif)
        {
            Int32 numero = 0;

            Int32.TryParse(nif.Substring(0, 8), out numero);

            return numero;
        }

        public char DevuelveLetra(string nif)
        {
            return nif[8];
        }

        public char ObtieneLetra(Int32 numero)
        {
            Int32 indice = numero % 23;
            return "TRWAGMYFPDXBNJZSQVHLCKET"[indice];
        }

        #endregion

        public bool CompruebaCP(string cp)
        {
            // definicion de la expresion regular
            Regex recp = new Regex("^[0-9]{5}$");

            return recp.IsMatch(cp);
        }

        public bool CompruebaMail(string mail)
        {
            Regex ermail = new Regex("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");

            return ermail.IsMatch(mail);
        }

        public bool CompruebaTelefono(string tel)
        {
            Regex ertel = new Regex("[0-9]{2,3}-? ?[0-9]{6,7}$");

            return ertel.IsMatch(tel);
        }


        public static bool EsNumeroEntero(String cadena)
        {
            Regex patronNumerico = new Regex("^[0-9]+$");
            return patronNumerico.IsMatch(cadena);
        }


        public bool CompruebaPass(string pass)
        {
            // longitud 6, me da igual que no tenga numero o si xD
            Regex recp = new Regex("^[0-9a-zA-Z]{6}$");

            return recp.IsMatch(pass);
        }
    }
}