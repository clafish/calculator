namespace calculator_Andrii_Korzh;

public class Queue 
{
    private ArrayList _queue = new ArrayList();
    private int _pointerFirst;
    private int _pointerLast;

    public string Dequeue()
    {
        string result = _queue.GetValue(0);
        _queue.Remove(result);
        return result;
    }

    public void Enqueue(string value)
    {
        _queue.Add(value);
        _pointerLast++;
    }
    
    public int Lenght()
    {
        return _queue.Count();
    }
}