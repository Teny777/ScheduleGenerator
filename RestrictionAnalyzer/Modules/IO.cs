using RestrictionAnalyzer.Tools;
using System.Collections.Generic;
using System.Linq;

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

        public List<string> GetErrors()
        {
            var result = _errors.Select(x => x.ToString()).ToList();
            return result;
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
    }
}