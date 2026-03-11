namespace calculator_Andrii_Korzh;

public class Node
{
    public string value;       
    public Node LeftNeighbour;      
    public Node RightNeighbour;
    
    public Node(string value1)
    {
        value = value1;
        LeftNeighbour = null;
        RightNeighbour = null;
    }
}