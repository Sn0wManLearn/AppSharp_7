using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSharp_7
{
    internal class SaveFieldAttribute: Attribute
    {
        public int FieldI {  get; }
        public string? FieldS { get; }
        public decimal FieldD { get; }
        public char[]? FieldC { get; }
        public SaveFieldAttribute()
        {
            
        }
        public SaveFieldAttribute(int i, string s, decimal d, char[] c)
        {
            FieldI = i;
            FieldS = s;
            FieldD = d;
            FieldC = c;
        }
    }
}
