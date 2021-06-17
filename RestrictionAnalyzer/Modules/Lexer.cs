using RestrictionAnalyzer.Tools;
using System.Collections.Generic;

namespace RestrictionAnalyzer.Modules
{
    internal class Lexer
    {
        private readonly IO _io;

        public Lexer(IO io)
        {
            _io = io;
        }


        // <символ> ::= <значение> | <идентификатор> | <оператор (в т.ч. ключевое слово)>
        public Token CurrentToken { get; private set; }

        public Token GetNextToken()
        {
            Token token;

            do
            {
                token = GetNextTokenWithNull();
            }
            while (token == null);

            CurrentToken = token;
            return token;
        }

        private Token GetNextTokenWithNull()
        {
            SkipSpaces();

            var charNumber = _io.CharNumber;

            var ch_ = _io.CurrentChar;
            if (ch_ is null) return new EndToken(TokenType.End, charNumber);
            var ch = (char)ch_;

            // идентификатор или ключевое слово
            if (char.IsLetter(ch) || ch == '_')
            {
                var token = ScanIdentOrKeyword();
                return token;
            }
            // число
            else if (char.IsDigit(ch))
            {
                var token = ScanNumber();
                return token;
            }
            // строка
            else if (ch == '"')
            {
                var token = ScanString();
                return token;
            }
            // оператор < или <=
            else if (ch == '<')
            {
                var nextCh = _io.GetNextChar();
                if (nextCh is null || nextCh != '=')
                {
                    var token = new OperatorToken(OperatorType.opSmaller, charNumber);
                    return token;
                }
                else
                {
                    var token = new OperatorToken(OperatorType.opSmallerEq, charNumber);
                    _io.GetNextChar();
                    return token;
                }
            }
            // оператор > или >=
            else if (ch == '>')
            {
                var nextCh = _io.GetNextChar();
                if (nextCh is null || nextCh != '=')
                {
                    var token = new OperatorToken(OperatorType.opLarger, charNumber);
                    return token;
                }
                else
                {
                    var token = new OperatorToken(OperatorType.opLargerEq, charNumber);
                    _io.GetNextChar();
                    return token;
                }
            }
            // оператор !=
            else if (ch == '!')
            {
                var nextCh = _io.ShowNextNthChar();
                if (nextCh != null && nextCh == '=')
                {
                    var token = new OperatorToken(OperatorType.opNotEqual, charNumber);
                    _io.GetNextChar();
                    _io.GetNextChar();
                    return token;
                }
                // else пойдет в конец метода к SetError
            }
            // оператор ->
            else if (ch == '-')
            {
                var nextCh = _io.GetNextChar();
                if (nextCh != null && nextCh == '>')
                {
                    var token = new OperatorToken(OperatorType.opImply, charNumber);
                    _io.GetNextChar();
                    return token;
                }
                else
                {
                    var token = new OperatorToken(OperatorType.opSubt, charNumber);
                    return token;
                }
            }
            // оставшиеся односимвольные операторы
            else if (_operators.ContainsKey(ch.ToString()))
            {
                var token = new OperatorToken(_operators[ch.ToString()], charNumber);
                _io.GetNextChar();
                return token;
            }

            _io.SetError(new Error(charNumber, 1)); // Error: запрещенный символ
            _io.GetNextChar();
            return null;
        }

        private void SkipSpaces()
        {
            char? ch = _io.CurrentChar;
            while (ch != null)
            {
                if (!char.IsWhiteSpace((char)ch))
                {
                    return;
                }
                ch = _io.GetNextChar();
            }
        }

        private Token ScanString()
        {
            var charNumber = _io.CharNumber;

            // todo: may be ограничивать длину идентификаторов

            // сразу дальше
            if (_io.GetNextChar() == null)
            {
                // всё закончилось так и не начавшись
                _io.SetError(new Error(charNumber, 2)); // Error: ошибка в константе: ожидался символ конца строки
                return null;
            }
            var str = _io.ReadWhile(ch => ch != '"');
            if (_io.CurrentChar is null)
            {
                _io.SetError(new Error(charNumber, 2)); // Error: ошибка в константе: ожидался символ конца строки
                return null;
            }

            // завершаем чтение (скип ")
            _io.GetNextChar();
            var @string = new ValueToken(str, charNumber);
            return @string;
        }

        private Token ScanNumber()
        {
            var charNumber = _io.CharNumber;
            var str = _io.ReadWhile(ch => char.IsDigit(ch));

            if (int.TryParse(str, out int value))
            {
                var integerValue = new ValueToken(value, charNumber);
                return integerValue;
            }
            else
            {
                //error слишком большая константа
                _io.SetError(new Error(charNumber, 3));
                return null;
            }
        }

        private Token ScanIdentOrKeyword()
        {
            var charNumber = _io.CharNumber;
            var str = _io.ReadWhile(ch => char.IsLetterOrDigit(ch));

            if (str == "not in")
            {
                var @operator = new OperatorToken(OperatorType.kwNotIn, charNumber);
                return @operator;
            }

            if (_operators.TryGetValue(str, out OperatorType type))
            {
                var @operator = new OperatorToken(type, charNumber);
                return @operator;
            }
            else
            {
                var identifier = new IdentifierToken(str, charNumber);
                return identifier;
            }
        }

        public void SetError(int code, Token token)
        {
            _io.SetError(new Error(token.CharNumber, code));
        }

        public void Log(string log)
        {
            _io.Log(log);
        }

        private readonly Dictionary<string, OperatorType> _operators = new Dictionary<string, OperatorType>
        {
            ["+"] = OperatorType.opAdd,
            ["-"] = OperatorType.opSubt,
            ["="] = OperatorType.opEqual,
            ["("] = OperatorType.opLBracket,
            [")"] = OperatorType.opRBracket,
            ["["] = OperatorType.opSquareLBracket,
            ["]"] = OperatorType.opSquareRBracket,
            [","] = OperatorType.opComma,
            [">"] = OperatorType.opLarger,
            [">="] = OperatorType.opLargerEq,
            ["<"] = OperatorType.opSmaller,
            ["<="] = OperatorType.opSmallerEq,
            ["!="] = OperatorType.opNotEqual,
            ["->"] = OperatorType.opImply,
            ["not in"] = OperatorType.kwNotIn,
            ["in"] = OperatorType.kwIn,
            ["R"] = OperatorType.kwR,
            ["И"] = OperatorType.kwAnd,
            ["ИЛИ"] = OperatorType.kwOr,
        };
    }
}
