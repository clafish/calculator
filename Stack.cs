namespace calculator_Andrii_Korzh;

public class Stack
{
    private ArrayList _stack = new ArrayList();
    private int _pointer;

    public void Push(string value)
    {
        _stack.Add(value);
        _pointer++;
    }

    public string Pull()
    {
        if (_pointer == 0)
            return null;
        _pointer--;
        string variable = _stack.GetValue(_pointer);
        _stack.Remove(_stack.GetValue(_pointer));
        return variable;
    }

    public string Peek()
    {
        if (_pointer == 0)
            return null;
        return _stack.GetValue(_pointer - 1);
    }

    public int Lenght()
    {
        return _stack.Count();
    }
}    