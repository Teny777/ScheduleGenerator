using RestrictionAnalyzer.Tools;
using System;

namespace RestrictionAnalyzer.Modules
{
    internal class Parser
    {
        private readonly Lexer _lexer;

        public Parser(Lexer lexer)
        {
            _lexer = lexer;
            _lexer.GetNextToken();
        }

        internal void Parse()
        {
            Требования();
            Accept(OperatorType.opImply);
            Условия();
        }

        private void Accept(OperatorType op)
        {
            if (_lexer.CurrentToken is OperatorToken token && token.OperatorType == op)
            {
                Console.WriteLine($"accept opera: {token}");
                _lexer.GetNextToken();
            }
            else
            {
                Console.WriteLine("error");
                //throw new ParserException(op, _lexer.CurrentToken);
            }
        }

        private void Accept(TokenType type)
        {
            if ( _lexer.CurrentToken.TokenType == type)
            {
                Console.WriteLine($"accept {type} : {_lexer.CurrentToken}");
                _lexer.GetNextToken();
            }
            else
            {
                Console.WriteLine("error");
            }
        }

        private bool IsStarts(OperatorType op)
        {
            return _lexer.CurrentToken is OperatorToken token && token.OperatorType == op;
        }

        private bool IsStarts(TokenType type)
        {
            return _lexer.CurrentToken.TokenType == type;
        }

        private bool IsStarts(Tools.ValueType type)
        {
            return _lexer.CurrentToken is ValueToken token && token.ValueType == type;
        }

        private void Требования()
        {
            ОпределениеПредикатов();
            if (!IsStarts(OperatorType.opImply))
            {
                ДополнительныеУсловия();
            }
        }

        //<условия>∷=<определение предикатов>И<доп.условия>| <доп.условия>
        private void Условия()
        {
            if (IsStarts(OperatorType.kwR))
            {
                ОпределениеПредикатов();
                ДополнительныеУсловия();
            }
            else
            {
                ДополнительныеУсловия();
            }
        }

        private void ОпределениеПредикатов()
        {
            Предикат();
            while (IsStarts(OperatorType.kwAnd))
            {
                Accept(OperatorType.kwAnd);
                if (IsStarts(OperatorType.kwR))
                {
                    Предикат();
                }
                else
                {
                    break;
                }
            }
        }

        private void Предикат()
        {
            Accept(OperatorType.kwR);
            Accept(OperatorType.opLBracket);
            Переменная();
            for (int i = 0; i < 5; i++)
            {
                Accept(OperatorType.opComma);
                Переменная();
            }
            Accept(OperatorType.opRBracket);
        }

        private void Переменная()
        {
            Accept(TokenType.Identifier);
        }

        private void ДополнительныеУсловия()
        {
            Условие();
            while (IsStarts(OperatorType.kwAnd))
            {
                Accept(OperatorType.kwAnd);
                Условие();
            }
        }

        private void Условие()
        {
            if (IsStarts(OperatorType.opLBracket))
            {
                Accept(OperatorType.opLBracket);
                СложноеУсловие();
                Accept(OperatorType.opRBracket);
            }
            else
            {
                ПростоеУсловие();
            }
        }

        private void СложноеУсловие()
        {
            Условие();
            while (IsStarts(OperatorType.kwOr))
            {
                Accept(OperatorType.kwOr);
                Условие();
            }
        }

        private void ПростоеУсловие()
        {
            Выражение();
            if (IsStarts(OperatorType.kwIn))
            {
                Accept(OperatorType.kwIn);
                Перечисление();
            }
            else if (IsStarts(OperatorType.kwNotIn))
            {
                Accept(OperatorType.kwNotIn);
                Перечисление();
            }
            else
            {
                ОперацияСравнения();
                Выражение();
            }
        }


        private readonly OperatorType[] _comparisons = new OperatorType[]
        {
            OperatorType.opSmaller,
            OperatorType.opSmallerEq,
            OperatorType.opLarger,
            OperatorType.opLargerEq,
            OperatorType.opEqual,
            OperatorType.opNotEqual
        };

        private void ОперацияСравнения()
        {
            foreach (var op in _comparisons)
            {
                if (IsStarts(op))
                {
                    Accept(op);
                    return;
                }
            }

            // error
        }

        private void Перечисление()
        {
            Accept(OperatorType.opSquareLBracket);
            Значение();
            while (IsStarts(OperatorType.opComma))
            {
                Accept(OperatorType.opComma);
                Значение();
            }
            Accept(OperatorType.opSquareRBracket);
        }

        private void Значение()
        {
            if (IsStarts(TokenType.Identifier))
            {
                Accept(TokenType.Identifier);
            }
            else if (IsStarts(TokenType.Value))
            {
                Accept(TokenType.Value);
            }
        }


        //<выражение> ::= <знак> <значение> { <знак> <значение> } | <значение> { <знак> <значение> } | <строковая константа>
        private void Выражение()
        {
            if (IsStarts(Tools.ValueType.String))
            {
                Accept(TokenType.Value);
            }
            else
            {
                if (IsStarts(OperatorType.opSubt))
                {
                    Accept(OperatorType.opSubt);
                }

                Значение();

                while (IsStarts(OperatorType.opAdd) || IsStarts(OperatorType.opSubt))
                { 
                    if (IsStarts(OperatorType.opAdd))
                    {
                        Accept(OperatorType.opAdd);
                    }
                    else
                    {
                        Accept(OperatorType.opSubt);
                    }
                    Значение();
                }
            }
        }
    }
}
