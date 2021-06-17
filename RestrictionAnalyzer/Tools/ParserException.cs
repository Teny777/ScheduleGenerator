using System;

namespace RestrictionAnalyzer.Tools
{
    public class ParserException : Exception
    {
        private readonly TokenType _tokenType;
        private readonly OperatorType _operator;
        private readonly ValueType _val;
        private readonly int _code;

        public Token Token { get; }

        public ParserException(TokenType type, Token token)
        {
            _tokenType = type;
            Token = token;
        }

        public ParserException(ValueType val, Token token)
        {
            _val = val;
            _tokenType = TokenType.Value;
            Token = token;
        }

        public ParserException(OperatorType @operator, Token token)
        {
            _operator = @operator;
            _tokenType = TokenType.Operator;
            Token = token;
        }

        public ParserException(int v, Token token)
        {
            _code = v;
            Token = token;
        }

        public int Code
        {
            get
            {
                if (_code != 0) return _code;

                return _tokenType switch
                {
                    TokenType.Identifier => 101,
                    TokenType.End => throw new Exception("End is disabled"),
                    TokenType.Value => _val switch
                    {
                        ValueType.Integer => 102,
                        ValueType.String => 103,
                        _ => throw new Exception("No variants"),
                    },
                    TokenType.Operator => _operator switch
                    {
                        OperatorType.opAdd => 104,
                        OperatorType.opSubt => 105,
                        OperatorType.opLBracket => 106,
                        OperatorType.opRBracket => 107,
                        OperatorType.opComma => 108,
                        OperatorType.opLarger => 109,
                        OperatorType.opLargerEq => 110,
                        OperatorType.opSmaller => 111,
                        OperatorType.opSmallerEq => 112,
                        OperatorType.opEqual => 113,
                        OperatorType.opNotEqual => 114,
                        OperatorType.opSquareLBracket => 115,
                        OperatorType.opSquareRBracket => 116,
                        OperatorType.kwAnd => 117,
                        OperatorType.opImply => 118,
                        OperatorType.kwOr => 119,
                        OperatorType.kwR => 120,
                        OperatorType.kwIn => 121,
                        OperatorType.kwNotIn => 122,
                        _ => -1,
                    },
                    _ => throw new Exception("No variants"),
                };
            }
        }
    }
}
