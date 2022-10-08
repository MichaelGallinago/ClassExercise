using System.Collections.Generic;
using System.IO;

namespace Bruh
{
    class Program
    {
        private static List<string> outLines;

        public static void Main()
        {
            string[] lines = File.ReadAllLines("File1.txt");

            string className = "";
            foreach (var line in lines)
            {
                if (line.StartsWith("Класс: "))
                {
                    className = line.Remove(0, 7).Replace("наследует", ":");
                    outLines = new List<string>() { "namespace SillyProgram", "{", "\tpublic class " + className, "\t{" };
                    className = className.Split(" : ")[0];
                }
                else if (line.StartsWith("Поля: "))
                {
                    string[] fields = GetFields(line);

                    AddLineToFileCS(fields, true);
                    AddLineToFileCS(fields, false);
                    outLines.Add("\t}");
                    outLines.Add("}");

                    File.WriteAllLines($"{className}.cs", outLines);
                }
            }
        }

        public static string[] GetFields(string line)
        {
            string fieldsString = line.Remove(0, 6);
            string[] replaceIt   = new string[] { "скрытый", "скрытая", "скрытое", "число", "строка", "символ", "флаг" };
            string[] replaceWith = new string[] { "private", "private", "private", "int",   "string", "char",   "bool" };
            for (var i = 0; i < replaceIt.Length; i++)
            {
                fieldsString = fieldsString.Replace(replaceIt[i], replaceWith[i]);
            }
            return fieldsString.Split(", ");
        }

        public static void AddLineToFileCS(string[] fields, bool isPrivate)
        {
            foreach (var field in fields)
            {
                string[] fieldData = ((field.StartsWith("private ") ? "" : "public ") + field).Split(" ");
                if (fieldData[0] == (isPrivate ? "private" : "public"))
                {
                    if (fieldData[1] == "список")
                    {
                        outLines.Add($"\t\t{fieldData[0]} List<{fieldData[2]}> {GetFullFieldName(fieldData[3], isPrivate)}"); 
                    }
                    else
                    {
                        outLines.Add($"\t\t{fieldData[0]} {fieldData[1]} {GetFullFieldName(fieldData[2], isPrivate)}");
                    }
                }
            }
        }

        public static string GetFullFieldName(string fieldName, bool isPrivate)
        {
            if (isPrivate) return fieldName.ToLower() + ";";
            return fieldName[0].ToString().ToUpper() + fieldName.Remove(0, 1) + " { get; set; }";
        }
    }
}