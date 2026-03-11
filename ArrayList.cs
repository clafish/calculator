namespace calculator_Andrii_Korzh;

public class ArrayList
{
    private string[] _array = new string[50];
    public int _pointer;
    
    public void Add(string value)
    {
        if (_pointer == _array.Length)
        {
            string[] newArray = new string[_array.Length * 2];
            for (int i = 0; i < _array.Length; i++)
            
                newArray[i] = _array[i];
            
            _array = newArray;
            // _array[_pointer] = value;
            // _pointer++;
        }
        
        _array[_pointer] = value;
        _pointer++;
    }

    public void Insert(int index, string value)
    {
        _array.SetValue(value, index);
    }
    
    public void Remove(string value)
    {
        for (int i = 0; i < _pointer; i++)
        {
            if (_array[i] == value)
            {
                for (int j = i; j < _pointer - 1; j++)
                {
                    _array[j] = _array[j + 1];
                }

                _pointer -= 1;
                return; 
            }
        }
    }

    public string GetValue(int index)
    {
        return _array[index];
    }

    public int IndexOf(string element)
    {
        for (var i = 0; i < _array.Length; i++)
        {
            if (_array[i] == element)
            {
                return i;
            }
        }
        return -1;
    }
    
    public bool Contains(string element)
    {
        return IndexOf(element) != -1;
    }
    
    public int Count()
    {
        return _pointer;
    }

    public void Clear()
    {
        _pointer = 0;
    }

    public string Join()
    {
        string result = "";
        for (int i = 0; i < _pointer; i++)
        {
            result += _array[i];
        }

        return result;
    }
}