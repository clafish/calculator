namespace calculator_Andrii_Korzh;

public class NodeStack
{
    private Node[] _array = new Node[50];
    public int _pointer;

    public void Push(Node node)
    {
        if (_pointer == _array.Length)
        {
            Node[] newArray = new Node[_array.Length * 2];
            for (int i = 0; i < _array.Length; i++)
            {
                newArray[i] = _array[i];
            }

            _array = newArray;
        }
        
        _array[_pointer] = node;
        _pointer++;
    }
    
    public Node Pull()
    {
        if (_pointer == 0) 
            return null;

        _pointer--;
        Node node = _array[_pointer];
        _array[_pointer] = null; // Clean up memory
        
        return node;
    }

    public int Count()
    {
        return _pointer;
    }
}