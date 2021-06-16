using System;

namespace RestrictionAnalyzer.Tools
{
    public abstract class Token
    {
        public Token(TokenType tokenType, int charNumber)
        {
            TokenType = tokenType;
            CharNumber = charNumber;
        }

        public int CharNumber { get; private set; }
        public TokenType TokenType { get; private set; }
    }

    public class OperatorToken : Token
    {
        public OperatorToken(OperatorType type, int charNumber)
            : base(TokenType.Operator, charNumber)
        {
            OperatorType = type;
        }

        public OperatorType OperatorType { get; private set; }

        public override string ToString()
        {
            return OperatorType.ToString();
        }
    }

    public class IdentifierToken : Token
    {
        public IdentifierToken(string token, int charNumber)
            : base(TokenType.Identifier, charNumber)
        {
            Identifier = token;
        }

        public string Identifier { get; private set; }

        public override string ToString()
        {
            return Identifier;
        }
    }

    public class ValueToken : Token
    {
        public ValueToken(object value, int charNumber)
            : base(TokenType.Value, charNumber)
        {
            Value = value;

            ValueType = Value switch
            {
                int _ => ValueType.Integer,
                string _ => ValueType.String,
                _ => throw new NotImplementedException(),
            };
        }

        public object Value { get; private set; }
        public ValueType ValueType { get; private set; }

        public override string ToString()
        {
            return ValueType switch
            {
                ValueType.Integer => ((int)Value).ToString(),
                ValueType.String => (string)Value,
                _ => throw new NotImplementedException(),
            };
        }
    }

    public class EndToken : Token
    {
        public EndToken(TokenType tokenType, int charNumber) : base(tokenType, charNumber)
        {
        }
    }
}
