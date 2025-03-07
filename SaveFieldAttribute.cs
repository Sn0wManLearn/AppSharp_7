using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSharp_7
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class SaveFieldAttribute: Attribute
    {
        public string Name { get; private set; }
        public SaveFieldAttribute()
        {
            
        }        
        public SaveFieldAttribute(string customFieldName)
        {
           Name = customFieldName;
        }
    }
}
