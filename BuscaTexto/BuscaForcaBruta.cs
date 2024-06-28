﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaTexto {
    class BuscaForcaBruta {
        public static int forcaBruta(String p, String t, int index) {
            int i, j, aux;
            int m = p.Length;
            int n = t.Length;
            for (i = index; i < n; i++) {
                aux = i;
                for (j = 0; j < m && aux < n; j++) {
                    if (p[j] != '?' && t[aux] != p[j])
                        break;
                    aux++;
                }
                if (j == m)
                    return i;
            }
            return -1;
        }
    }
}