﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


namespace AppSharp_7;
class MyClass
{
    public static TestClass MakeTestclass()
    {
        Type testclass = typeof(TestClass);
        return Activator.CreateInstance(testclass) as TestClass;
    }

    public static TestClass MakeTestclass(int i)
    {
        Type testclass = typeof(TestClass);
        return Activator.CreateInstance(testclass, new object[] { i }) as TestClass;
    }

    public static TestClass MakeTestclass(int i, string s, decimal d, char[] c)
    {
        Type testclass = typeof(TestClass);
        return Activator.CreateInstance(testclass, new object[] { i, s, d, c }) as TestClass;
    }

    static object StringToObject(string s)
    {
        string[] strArr1 = s.Split('|');

        object obj = Activator.CreateInstance(null, strArr1[0].Split(',')[0])?.Unwrap();

        if (strArr1.Length > 1 && obj != null)
        {
            var type = obj.GetType();
            for (int i = 1; i < strArr1.Length; i++)
            {
                string[] nameAndValue = strArr1[i].Split(":");
                var prop = type.GetProperty(nameAndValue[0]);
                if (prop == null) continue;
                if (prop.PropertyType == typeof(int))
                {
                    prop.SetValue(obj, int.Parse(nameAndValue[1]));
                }
                else if (prop.PropertyType == typeof(string))
                {
                    prop.SetValue(obj, nameAndValue[1]);
                }
                else if (prop.PropertyType == typeof(decimal))
                {
                    prop.SetValue(obj, decimal.Parse(nameAndValue[1]));
                }
                else if (prop.PropertyType == typeof(char[]))
                {
                    prop.SetValue(obj, nameAndValue[1].ToCharArray());
                }
            }
        }
        return obj;
    }

    static string ObjectToString(object o)
    {
        Type type = o.GetType();

        StringBuilder result = new StringBuilder();
        result.Append(type.AssemblyQualifiedName + ":");
        result.Append(type.Name + '|');
        var proper = type.GetProperties();
        foreach (var item in proper)
        {
            var tmp = item.GetValue(o);
            result.Append(item.Name + ':');
            if (item.PropertyType == typeof(char[]))
            {
                result.Append(new string(tmp as char[]) + '|');
            }
            else
            {
                result.Append(tmp);
                result.Append('|');
            }
        }

        var fields = type.GetFields();
        foreach (var field in fields)
        {
            var attribute = field.GetCustomAttribute<SaveFieldAttribute>();
            if (attribute != null)
            {
                result.Append(attribute.Name + ":");
                var fieldVal = field.GetValue(o);
                result.Append(fieldVal + "\n");
            }
        }
        return result.ToString();
    }
    static void Main()
    {
        char[] chars = { 'a', 'b', 'c' };
        var obj1 = MakeTestclass(1, "str", 1.5M, chars);

        string str = ObjectToString(obj1);

        Console.WriteLine(str);
        Console.WriteLine();

        var obj2 = StringToObject(str);

        Console.WriteLine(ObjectToString(obj2));
    }
}
