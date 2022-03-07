

namespace FirstCSharp.Domain.Model.ConsoleWrapper
{
    using System;
    public class ConsoleWrapper:IConsoleWrapper
    {
        public void Clear()
        {
            Console.Clear();
        }
             
        public int Read()
        {
            return Console.Read();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// console write
        /// </summary>
        /// <param name="str"></param>
        public void Write(string str)
        {
            Console.Write(str);
        }

        /// <summary>
        /// console writeLine
        /// </summary>
        /// <param name="str"></param>
        public void WriteLine(string str)
        {
            Console.WriteLine(str);
        }
    }
}
