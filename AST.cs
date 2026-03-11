namespace calculator_Andrii_Korzh;

public class AST
{
    public NodeStack _nodes = new NodeStack();
    public Queue _queue { get; set; }
    private ArrayList _binaryOperators = new ArrayList();
    private ArrayList _unaryOperators = new ArrayList();
    
    public void Algorithm()
    {
        _binaryOperators.Add("+");
        _binaryOperators.Add("-");
        _binaryOperators.Add("*");
        _binaryOperators.Add("/");
        _binaryOperators.Add("^");
        _binaryOperators.Add("max");
        
        _unaryOperators.Add("sin");
        _unaryOperators.Add("cos");
        _unaryOperators.Add("tg");
        _unaryOperators.Add("ctg");
        
        while (_queue.Lenght() > 0)
        {
            string token = _queue.Dequeue();

            if (double.TryParse(token, out _))
            {
                var node = new Node(token);
                _nodes.Push(node);
            }
    
            else if (_binaryOperators.Contains(token))
            {
                var node = new Node(token);
                node.RightNeighbour = _nodes.Pull();
                node.LeftNeighbour = _nodes.Pull();
                _nodes.Push(node);
            }
    
            else if (_unaryOperators.Contains(token))
            {
                var node = new Node(token);
                node.LeftNeighbour = _nodes.Pull();
                _nodes.Push(node);
            }
        }
    }

    // ├── │
    // └──

    public void Draw(Node node, string intend, bool isLast, bool isFirstNode)
    {
        if (node == null) return;

        if (isFirstNode)
            Console.WriteLine(node.value);
        
        else
        {
            Console.Write(intend);

            if (isLast)
            {
                Console.Write("└── ");
                intend += "    ";
            }
            else
            {
                Console.Write("├── ");
                intend += "│   ";
            }

            Console.WriteLine(node.value);
        }

        if (node.RightNeighbour != null && node.LeftNeighbour != null)
            
        {
            Draw(node.LeftNeighbour, intend, false, false);
            Draw(node.RightNeighbour, intend, true, false);
        }
        else if (node.LeftNeighbour != null)
        {
            Draw(node.LeftNeighbour, intend, true, false);
        }
        else if (node.RightNeighbour != null)
        {
            Draw(node.RightNeighbour, intend, true, false);
        }
    }
}