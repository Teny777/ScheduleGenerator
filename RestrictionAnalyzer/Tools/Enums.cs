namespace RestrictionAnalyzer.Tools
{
    public enum TokenType
    {
        Value,
        Identifier,
        Operator,
        End,
    }

    public enum ValueType
    {
        Integer,
        String,
    }

    public enum OperatorType
    {
        opAdd,              // +
        opSubt,             // -
        opLBracket,         // (
        opRBracket,         // )
        opSquareLBracket,   // [
        opSquareRBracket,   // ]
        opComma,            // ,
        opLarger,           // >
        opLargerEq,         // >=
        opSmaller,          // <
        opSmallerEq,        // <=
        opEqual,            // =
        opNotEqual,         // !=
        opImply,            // ->
        kwAnd,              // И
        kwOr,               // ИЛИ
        kwR,                // R
        kwIn,               // in
        kwNotIn,            // not in
    }
}
