public enum Symbol
{
    Plus,
    Minus,
    Multiply,
    Divide
}

public static class SymbolExtensions
{
    public static string ToSymbolString(this Symbol symbol)
    {
        switch (symbol)
        {
            case Symbol.Plus:
                return "+";
            case Symbol.Minus:
                return "-";
            case Symbol.Multiply:
                return "*";
            case Symbol.Divide:
                return "/";
            default:
                return symbol.ToString();
        }
    }
}