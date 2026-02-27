using System.Collections;

Console.WriteLine("Enter your expression: ");
string expression = Console.ReadLine();

var operators = new List<char> { '+', '-', '*', '/'};

var result = new List<string>();
var buffer = new List<char>();

int counter = 1;
foreach (char symbol in expression)
{
    
    if (char.IsNumber(symbol))
    {
        buffer.Add(symbol);
    }

    else if (char.IsWhiteSpace(symbol))
    {
        if (buffer.Count > 0)
        {
            string token = string.Join("", buffer);
            result.Add(token);
            buffer.Clear();
        }
    }
    
    else if (operators.Contains(symbol))
    {
        if (buffer.Count > 0)
        {
            string token = string.Join("", buffer);
            result.Add(token);
            buffer.Clear();
        }
        result.Add(Convert.ToString(symbol));
    }

    counter++;
}

string last_token = string.Join("", buffer);
result.Add(last_token);

foreach (var i in result)
{
    Console.WriteLine(i);
}

