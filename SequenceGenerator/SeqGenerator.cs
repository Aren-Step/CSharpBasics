public interface ISequenceGenerator<T>
{
    public abstract T Previous { get; set; }
    public abstract T Current { get; set; }
    public abstract T Next { get; set; }
}

public abstract class SequenceGenerator<T> : ISequenceGenerator<T>
{
    public abstract T Previous { get; set; }
    public abstract T Current { get; set; }
    public abstract T Next { get; set; }
    public int Count { get; private set; }
    public SequenceGenerator(T first, T second)
    {
        Previous = first;
        Current = second;
        Next = default;
        Count++;
    }

    public abstract T GetNext();
}

public class FibonacciSequenceGenerator : SequenceGenerator<int>
{
    public FibonacciSequenceGenerator(int first, int second) : base(first, second)
    {
        Next = first + second;
    }
    public override int Previous { get; set; }
    public override int Current { get; set; }
    public override int Next { get; set; }
    public override int GetNext()
    {
        return Next;
    }
}