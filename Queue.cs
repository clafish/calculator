namespace calculator_Andrii_Korzh;

public class Queue
{
    private ArrayList _queue;
    private int _pointerFirst;
    private int _pointerLast;

    public string Dequeue()
    {
        _queue.Remove(_queue.GetValue(0));
        _pointerFirst++;
        return _queue.GetValue(0);
    }

    public void Enqueue(string value)
    {
        _queue.Add(value);
        _pointerLast++;
    }
}