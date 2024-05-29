/* MIT License

Copyright (c) 2024 Dexrn ZacAttack

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Xml.Serialization;

namespace HexpatCombiner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("https://github.com/DexrnZacAttack/HexpatCombiner");
            if (args[0] == null && args[1] == null) { Console.WriteLine("HexpatCombiner.exe {string filePath} {bool addCombinationComment}"); }
            string[] FileContent = File.ReadAllLines(args[0]);

            Combine(FileContent, args[1].ToLower() == "true" ? true : false );
        }

        private const string ImportKeyword = "combine";
        private const string NamePragma = "#pragma combinerName";

        private static void Combine(string[] content, bool doNotAddCombinationComment)
        {
            List<string> result = new List<string>();

            foreach (string line in content)
            {
                if (line.StartsWith(ImportKeyword))
                {
                    //import file
                    string path = line
                        .Split(ImportKeyword, 2)[1]
                        .Replace("\"", "")
                        .Trim()[..^1];

                    Console.WriteLine($"Processing combine import {path}");

                    List<string> importContent = new List<string>(File.ReadAllLines(path));
                    string name = "";
                    for (int i = importContent.Count - 1; i >= 0; i--)
                    {
                        string chkLn = importContent[i];
                        if (chkLn.StartsWith(NamePragma))
                        {
                            name = new string($"\"{chkLn.Split(NamePragma, 2)[1].Trim()}\"");
                            importContent.RemoveAt(i); // remove the line lol
                            break;
                        }
                    }

                    string comment = $" * COMBINED HEXPAT {name}";
                    // this is really repetitive lol
                    string endComment = $" * END COMBINED HEXPAT {name}";
                    string endEquals = new String('=', endComment.Length);
                    string equals = new String('=', comment.Length);
                    if (doNotAddCombinationComment != true)
                    result.AddRange(new List<String>
                    {
                        "",
                        "/*",
                        equals,
                        comment,
                        equals,
                        "*/",
                        ""
                    });

                    result.AddRange(importContent); // this adds the actual file.

                    if (doNotAddCombinationComment != true)
                    result.AddRange(new List<String>
                    {
                        "",
                        "/*",
                        endEquals,
                        endComment,
                        endEquals,
                        "*/",
                        ""
                    });
                }
                else
                {
                    result.Add(line);
                }
            }

            File.WriteAllLines("combined.hexpat", result);
        }

    }
}
