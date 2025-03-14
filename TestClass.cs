﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSharp_7
{
    internal class TestClass
    {
        public int I { get; set; }
        public string? S { get; set; }
        public decimal D { get; set; }
        public char[]? C { get; set; }

        [SaveField("CustomFieldName")]
        public string someName = "This is field with string";
        public TestClass()
        { }
        private TestClass(int i)
        {
            this.I = i;
        }
        public TestClass(int i, string s, decimal d, char[] c) : this(i)
        {
            this.S = s;
            this.D = d;
            this.C = c;
        }
    }
}
