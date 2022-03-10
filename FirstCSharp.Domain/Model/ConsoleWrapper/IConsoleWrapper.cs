using System.Collections.Generic;

namespace FirstCSharp.Domain.Model.ConsoleWrapper
{
    public interface IConsoleWrapper
    {

        void Clear();

        int Read();

        string ReadLine();


        void Write(string str);


        void WriteLine(string str);
    }
}
