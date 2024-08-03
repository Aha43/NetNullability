using System.Dynamic;

namespace ExampleLibNoNullability;

public abstract class Interval
{
    protected static (int min, int max) Normalize(int start, int end)
    {
        return start > end ? (end, start) : (start, end);
    }

    protected static (string min, string max) Normalize(string start, string end)
    {
        return string.Compare(start, end) > 0 ? (end, start) : (start, end);
    }
}

public class IntInterval : Interval
{
    public int Start { get; internal set; } = 0;
    public int End { get; internal set; } = 10;

    private (int min, int max)? _normalized = null;
    private (int min, int max) Normalized
    {
        get
        {
            _normalized ??= Normalize(Start, End);
            return _normalized.Value;
        }
    }

    public bool Contains(int value)
    {
        var (min, max) = Normalized;
        return value >= min && value <= max;
    }
}

public class StringInterval : Interval
{
    public string Start { get; internal set; } = "a";
    public string End { get; internal set; } = "z";

    private (string min, string max)? _normalized = null;
    private (string min, string max) Normalized
    {
        get
        {
            _normalized ??= Normalize(Start, End);
            return _normalized.Value;
        }
    }

    public bool Contains(string value)
    {
        var (min, max) = Normalized;
        return string.Compare(value, min) >= 0 && string.Compare(value, max) <= 0;
    }
}
