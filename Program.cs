using System;
using System.Collections.Generic;
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
        string[] strArr = s.Split('|');
        string[] strA = strArr[0].Split(':');
        object obg = Activator.CreateInstance(null, strA[1])?.Unwrap();
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
        return result.ToString();
    }
    static void Main()
    {
        char[] chars = { 'a', 'b', 'c' };
        var obj1 = MakeTestclass(1, "str", 1.5M, chars);
    }
}
