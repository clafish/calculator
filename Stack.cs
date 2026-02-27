namespace calculator_Andrii_Korzh;

public class Stack
{
    private ArrayList _stack;
    private int _pointer;

    public void Push(string value)
    {
        _stack.Add(value);
        _pointer++;
    }

    public string Pull()
    {
        if (_stack.Lenght() == 0)
            return null;
        _stack.Remove(_stack.GetValue(_pointer));
        _pointer--;
        return _stack.GetValue(_pointer);
    }
}    