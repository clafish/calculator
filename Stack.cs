namespace calculator_Andrii_Korzh;

public class Stack
{
    private ArrayList _stack = new ArrayList();

    public void Push(string value)
    {
        _stack.Add(value);
    }

    public string Pull()
    {
        if (_stack._pointer == 0)
            return null;
        _stack._pointer--;
        string variable = _stack.GetValue(_stack._pointer);
        return variable;
    }

    public string Peek()
    {
        if (_stack._pointer == 0)
            return null;
        return _stack.GetValue(_stack._pointer - 1);
    }

    public int Lenght()
    {
        return _stack.Count();
    }
}    