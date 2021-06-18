using RestrictionAnalyzer.Semant;
using RestrictionAnalyzer.Tools;
using System.Collections.Generic;
using System.Linq;

namespace RestrictionAnalyzer.Modules
{
    internal class Parser
    {
        private readonly List<Token> _expressionBuilder;
        private readonly Lexer _lexer;
        private Scope _scope;
        public Parser(Lexer lexer)
        {
            _expressionBuilder = new List<Token>();
            _lexer = lexer;
            _lexer.GetNextToken();
        }

        internal void Parse()
        {
            _scope = new Scope();
            Требования();
            try
            {
                Accept(OperatorType.opImply);
            }
            catch (ParserException ex)
            {
                _lexer.SetError(ex.Code, ex.Token);
            }
            var scope = new Scope
            {
                PreviousScope = _scope
            };
            _scope = scope;
            Условия();
        }

        internal string GetExpression()
        {
            var result = string.Join("", _expressionBuilder.Select(x =>
            {
                if (x is OperatorToken ot) return _operators[ot.OperatorType];
                return x.ToString();
            }));
            return result;
        }

        private void Accept(OperatorType op)
        {
            if (_lexer.CurrentToken is OperatorToken token && token.OperatorType == op)
            {
                _expressionBuilder.Add(token);
                _lexer.Log($"Accept Operator: {token}");
                _lexer.GetNextToken();
            }
            else
            {
                throw new ParserException(op, _lexer.CurrentToken);
            }
        }

        private string Accept(TokenType type)
        {
            if (_lexer.CurrentToken.TokenType == type)
            {
                _expressionBuilder.Add(_lexer.CurrentToken);
                _lexer.Log($"Accept {type}: {_lexer.CurrentToken}");
                var token = _lexer.CurrentToken;
                _lexer.GetNextToken();
                return token.ToString();
            }
            else
            {
                throw new ParserException(type, _lexer.CurrentToken);
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

        private void SkipTo(params OperatorType[] skipto)
        {
            while (true)
            {
                if (_lexer.CurrentToken is EndToken) break;
                foreach (var skip in skipto)
                {
                    if (IsStarts(skip))
                    {
                        return;
                    }
                }
                _lexer.GetNextToken();
            }
        }

        private void Требования()
        {
            ОпределениеПредикатов(new[] { OperatorType.opImply, OperatorType.kwAnd });
            if (!IsStarts(OperatorType.opImply))
            {
                ДополнительныеУсловия(new[] { OperatorType.opImply });
            }
        }

        //<условия>∷=<определение предикатов>И<доп.условия>| <доп.условия>
        private void Условия()
        {
            if (IsStarts(OperatorType.kwR))
            {
                ОпределениеПредикатов(new[] { OperatorType.kwAnd });
            }
            ДополнительныеУсловия(new OperatorType[0]);
        }

        private void ОпределениеПредикатов(OperatorType[] skipTo)
        {
            var predskip = new[] { OperatorType.kwAnd, OperatorType.kwR }.Concat(skipTo).ToArray();
            Предикат(predskip);
            while (IsStarts(OperatorType.kwAnd))
            {
                Accept(OperatorType.kwAnd);
                if (IsStarts(OperatorType.kwR))
                {
                    Предикат(predskip);
                }
                else
                {
                    break;
                }
            }
        }

        private void Предикат(OperatorType[] skipTo)
        {
            var lbskip = new[] { OperatorType.opLBracket }.Concat(skipTo).ToArray();
            var varskip = new[] { OperatorType.opComma, OperatorType.opRBracket }.Concat(skipTo).ToArray();

            try
            {
                Accept(OperatorType.kwR);
            }
            catch (ParserException ex)
            {
                _lexer.SetError(ex.Code, ex.Token);
                SkipTo(lbskip);
            }

            try
            {
                Accept(OperatorType.opLBracket);
            }
            catch (ParserException ex)
            {
                _lexer.SetError(ex.Code, ex.Token);
                SkipTo(varskip);
            }

            var token = _lexer.CurrentToken;
            var name = Переменная(varskip);

            AddIdentToScopeFromNumber(1, name, token);

            for (int i = 2; i <= 6; i++)
            {
                try
                {
                    Accept(OperatorType.opComma);
                }
                catch (ParserException ex)
                {
                    _lexer.SetError(ex.Code, ex.Token);
                }
                token = _lexer.CurrentToken;
                name = Переменная(varskip);
                AddIdentToScopeFromNumber(i, name, token);
            }
            try
            {
                Accept(OperatorType.opRBracket);
            }
            catch (ParserException ex)
            {
                _lexer.SetError(ex.Code, ex.Token);
                SkipTo(skipTo);
            }
        }

        private void AddIdentToScopeFromNumber(int number, string name, Token token)
        {
            var identifier = _scope.Search(name);
            if (identifier == null)
            {
                var type = number switch
                {
                    1 => Type.String,
                    2 => Type.String,
                    3 => Type.String,
                    4 => Type.String,
                    5 => Type.Integer,
                    6 => Type.Integer,
                    _ => throw new System.Exception("переменных в предикате всего 6"),
                };

                _scope.Add(new Identifier(name, type));
            }
            else
            {
                _lexer.SetError(201, token);
            }
        }

        private string Переменная(OperatorType[] skipTo)
        {
            try
            {
                return Accept(TokenType.Identifier);
            }
            catch (ParserException ex)
            {
                _lexer.SetError(ex.Code, ex.Token);
                SkipTo(skipTo);
                return null;
            }
        }

        private void ДополнительныеУсловия(OperatorType[] skipTo)
        {
            var skip = new[] { OperatorType.kwAnd }.Concat(skipTo).ToArray();

            Условие(skip);
            while (IsStarts(OperatorType.kwAnd))
            {
                Accept(OperatorType.kwAnd);
                Условие(skip);
            }
        }

        private void Условие(OperatorType[] skipTo)
        {
            if (IsStarts(OperatorType.opLBracket))
            {
                Accept(OperatorType.opLBracket);
                СложноеУсловие(new[] { OperatorType.opRBracket }.Concat(skipTo).ToArray());

                try
                {
                    Accept(OperatorType.opRBracket);
                }
                catch (ParserException ex)
                {
                    _lexer.SetError(ex.Code, ex.Token);
                    SkipTo(skipTo);
                }

            }
            else
            {
                try
                {
                    ПростоеУсловие(skipTo);
                }
                catch (ParserException ex)
                {
                    _lexer.SetError(ex.Code, ex.Token);
                    SkipTo(skipTo);
                }
            }
        }

        private void СложноеУсловие(OperatorType[] skipTo)
        {
            Условие(new[] { OperatorType.kwOr }.Concat(skipTo).ToArray());

            while (IsStarts(OperatorType.kwOr))
            {
                Accept(OperatorType.kwOr);
                Условие(new[] { OperatorType.kwOr }.Concat(skipTo).ToArray());
            }
        }

        private void ПростоеУсловие(OperatorType[] skipTo)
        {
            Type mainType = null;
            try
            {
                mainType = Выражение(new[] { OperatorType.kwIn, OperatorType.kwNotIn }.Concat(_comparisons).Concat(skipTo).ToArray());
            }
            catch (ParserException ex)
            {
                _lexer.SetError(ex.Code, ex.Token);
            }

            if (IsStarts(OperatorType.kwIn))
            {
                Accept(OperatorType.kwIn);
                var token = _lexer.CurrentToken;
                var type = Перечисление(skipTo);
                if (!IsComparable(type, ref mainType))
                {
                    _lexer.SetError(205, token);
                }
            }
            else if (IsStarts(OperatorType.kwNotIn))
            {
                Accept(OperatorType.kwNotIn);
                var token = _lexer.CurrentToken;
                var type = Перечисление(skipTo);
                if (!IsComparable(type, ref mainType))
                {
                    _lexer.SetError(205, token);
                }
            }
            else if (_comparisons.Any(IsStarts))
            {
                var token = _lexer.CurrentToken;
                var operatorType = ОперацияСравнения();
                Type type = null;
                try
                {
                    type = Выражение(skipTo);
                }
                catch (ParserException ex)
                {
                    _lexer.SetError(ex.Code, ex.Token);
                }

                if (!IsComparable(type, ref mainType))
                {
                    _lexer.SetError(207, token);
                }

                if (type == Type.String)
                {
                    if (operatorType != OperatorType.opEqual && operatorType != OperatorType.opNotEqual)
                    {
                        _lexer.SetError(206, token);
                    }
                }
            }
            else
            {
                throw new ParserException(125, _lexer.CurrentToken); // Ожидалось сравнение или in not in
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

        private OperatorType? ОперацияСравнения()
        {
            foreach (var op in _comparisons)
            {
                if (IsStarts(op))
                {
                    Accept(op);
                    return op;
                }
            }
            return null;
        }

        private Type Перечисление(OperatorType[] skipTo)
        {
            Accept(OperatorType.opSquareLBracket);
            Type mainType = null;
            try
            {
                mainType = Значение();
            }
            catch (ParserException ex)
            {
                _lexer.SetError(ex.Code, ex.Token);
                SkipTo(OperatorType.opComma, OperatorType.opSquareRBracket);
            }

            while (IsStarts(OperatorType.opComma))
            {
                Accept(OperatorType.opComma);
                try
                {
                    var token = _lexer.CurrentToken;
                    var type = Значение();
                    if (!IsComparable(type, ref mainType))
                    {
                        _lexer.SetError(204, token);
                    }
                }
                catch (ParserException ex)
                {
                    _lexer.SetError(ex.Code, ex.Token);
                    SkipTo(OperatorType.opComma, OperatorType.opSquareRBracket);
                }
            }

            try
            {
                Accept(OperatorType.opSquareRBracket);
            }
            catch (ParserException ex)
            {
                _lexer.SetError(ex.Code, ex.Token);
                SkipTo(skipTo);
            }

            return mainType;
        }

        private bool IsComparable(Type type, ref Type mainType)
        {
            if (mainType == null) mainType = type;
            if (type == null || mainType == null) return true;
            return type == mainType;
        }

        private Type Значение()
        {
            if (IsStarts(TokenType.Identifier))
            {
                var token = _lexer.CurrentToken;
                var name = Accept(TokenType.Identifier);
                var identifier = _scope.Search(name);
                if (identifier is null)
                {
                    _lexer.SetError(202, token);
                    _scope.Add(new Identifier(name, null));
                    return null;
                }
                else
                {
                    return identifier.Type;
                }
            }
            else if (IsStarts(TokenType.Value))
            {
                var token = (ValueToken)_lexer.CurrentToken;
                Accept(TokenType.Value);

                return token.ValueType switch
                {
                    ValueType.Integer => Type.Integer,
                    ValueType.String => Type.String,
                    _ => throw new System.NotImplementedException(),
                };
            }
            else
            {
                throw new ParserException(124, _lexer.CurrentToken); // ожидалось значение или идентификатор
            }
        }


        //<выражение> ::= <знак> <значение> { <знак> <значение> } | <значение> { <знак> <значение> } | <строковая константа>
        private Type Выражение(OperatorType[] skipTo)
        {
            Type resultType = null;
            if (IsStarts(ValueType.String))
            {
                Accept(TokenType.Value);
                return Type.String;
            }
            else
            {
                bool good = false;
                bool firstSign = false;
                if (IsStarts(OperatorType.opSubt))
                {
                    Accept(OperatorType.opSubt);
                    firstSign = true;
                }

                var isString = false;
                try
                {
                    var token = _lexer.CurrentToken;
                    resultType = Значение();
                    isString = resultType == Type.String;
                    if (isString && firstSign)
                    {
                        _lexer.SetError(203, token);
                    }
                    good = true;
                }
                catch (ParserException ex)
                {
                    _lexer.SetError(ex.Code, ex.Token);
                    SkipTo(new[] { OperatorType.opAdd, OperatorType.opSubt }.Concat(skipTo).ToArray());
                }

                while (IsStarts(OperatorType.opAdd) || IsStarts(OperatorType.opSubt))
                {
                    if (isString)
                    {
                        _lexer.SetError(203, _lexer.CurrentToken);
                    }

                    if (IsStarts(OperatorType.opAdd))
                    {
                        Accept(OperatorType.opAdd);
                    }
                    else
                    {
                        Accept(OperatorType.opSubt);
                    }

                    try
                    {
                        var token = _lexer.CurrentToken;
                        var type = Значение();
                        if (type == Type.String)
                        {
                            _lexer.SetError(203, token);
                        }
                    }
                    catch (ParserException ex)
                    {
                        _lexer.SetError(ex.Code, ex.Token);
                        SkipTo(new[] { OperatorType.opAdd, OperatorType.opSubt }.Concat(skipTo).ToArray());
                    }
                }

                if (!good)
                {
                    throw new ParserException(123, _lexer.CurrentToken); // ожидалось выражение;
                }

                return resultType;
            }
        }

        private static readonly Dictionary<OperatorType, string> _operators = new Dictionary<OperatorType, string>
        {
            [OperatorType.opAdd] = " + ",
            [OperatorType.opSubt] = " - ",
            [OperatorType.opEqual] = " == ",
            [OperatorType.opLBracket] = "(",
            [OperatorType.opRBracket] = ")",
            [OperatorType.opSquareLBracket] = "[",
            [OperatorType.opSquareRBracket] = "]",
            [OperatorType.opComma] = ", ",
            [OperatorType.opLarger] = " > ",
            [OperatorType.opLargerEq] = " >= ",
            [OperatorType.opSmaller] = " < ",
            [OperatorType.opSmallerEq] = " <= ",
            [OperatorType.opNotEqual] = " != ",
            [OperatorType.opImply] = " -> ",
            [OperatorType.kwNotIn] = " not in ",
            [OperatorType.kwIn] = " in ",
            [OperatorType.kwR] = "R",
            [OperatorType.kwAnd] = " && ",
            [OperatorType.kwOr] = " || ",
        };
    }
}
