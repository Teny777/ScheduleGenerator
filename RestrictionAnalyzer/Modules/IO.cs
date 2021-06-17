using RestrictionAnalyzer.Tools;
using System;
using System.Collections.Generic;

namespace RestrictionAnalyzer.Modules
{
    internal class IO
    {
        private readonly string _restriction;
        private readonly List<Error> _errors;
        private readonly List<string> _log;
        private int _position;
        public int CharNumber { get; private set; }

        public IO(string restriction)
        {
            _restriction = restriction;
            _errors = new List<Error>();
            _log = new List<string>();
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
            var result = new List<string>(_errors.Count);
            foreach (var error in _errors)
            {
                var offset = new string(' ', error.CharNumber - 2);
                var position = $"^Позиция: {error.CharNumber}  ";
                var code = $"Код ошибки: {error.Code}  ";
                var description = $"Описание ошибки: {Error.Errors[error.Code]}";
                result.Add($"{offset}{position}{code}{description}");
            }
            return result;
        }

        public List<string> GetLog()
        {
            var result = new List<string>(_log);
            return result;
        }

        public void SetError(Error error)
        {
            _errors.Add(error);
        }

        public void Log(string log)
        {
            _log.Add(log);
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
