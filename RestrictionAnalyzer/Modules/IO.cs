using RestrictionAnalyzer.Tools;
using System;
using System.Collections.Generic;

namespace RestrictionAnalyzer.Modules
{
    internal class IO
    {
        private readonly string _restriction;
        private readonly List<Error> _errors;
        private int _position;
        public int CharNumber { get; private set; }

        public IO(string restriction)
        {
            _restriction = restriction;
            _errors = new List<Error>();
            _position = -1;
            CharNumber = 1;
            GetNextChar();
        }

        public char? CurrentChar
        {
            get
            {
                if (_position < _restriction.Length)
                {
                    return _restriction[_position];
                }
                return null;
            }
        }

        public void SetError(Error error)
        {
            _errors.Add(error);
        }

        public char? GetNextChar()
        {
            _position++;
            if (_position == _restriction.Length)
            {
                return null;
            }

            var ch = _restriction[_position];
            switch (ch)
            {
                case '\t':
                    CharNumber += 4;
                    break;
                default:
                    CharNumber++;
                    break;
            }
            return ch;
        }

        public char? ShowNextNthChar(int number = 1)
        {
            var position = _position + number;
            if (position < _restriction.Length)
            {
                return _restriction[position];
            }

            return null;
        }

        public void PrintListing()
        {
            foreach (var error in _errors)
            {
                Console.WriteLine(error);
            }
            //Console.WriteLine("----------------------------------------------------------------------------");
            //Console.WriteLine($"Количество ошибок: {_errors.Count}");
            //Console.WriteLine();

            //var lines = _path.Split('\n');
            //int i = 1;

            //var reader = new StreamReader(_path);
            //string line;
            //while ((line = reader.ReadLine()) != null)
            //{
            //    Console.WriteLine($"{i,4} {line.Replace("\t", "    ")}");
            //    var errors = _errors.Where(x => x.LineNumber == i).ToList();
            //    foreach (var error in errors)
            //    {
            //        _errors.Remove(error);
            //        PrintError(error);
            //    }
            //    i++;
            //}
            //Console.WriteLine("----------------------------------------------------------------------------");
        }

        private void PrintError(Error error)
        {
            //var offset = new string(' ', error.CharNumber - 1);
            //Console.WriteLine($"****{offset}^{Error.Errors[error.Code]}");
        }
    }
}
