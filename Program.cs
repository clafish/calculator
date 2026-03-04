using calculator_Andrii_Korzh;
using Queue = calculator_Andrii_Korzh.Queue;
using Stack = calculator_Andrii_Korzh.Stack;

Console.WriteLine("Enter your expression: ");
string expression = Console.ReadLine();

var operators = new ArrayList();
operators.Add("+");
operators.Add("-");
operators.Add("*");
operators.Add("/");
operators.Add("^");

// TOKENIZER

var result = new ArrayList();
var buffer = new ArrayList();

foreach (char symbol in expression)
{

    if (char.IsNumber(symbol))
    {
        buffer.Add(symbol.ToString());
    }

    else if (char.IsWhiteSpace(symbol))
    {
        if (buffer.Count() > 0)
        {
            result.Add(buffer.Join());
            buffer.Clear();
        }
    }

    else if (operators.Contains(symbol.ToString()))
    {
        if (buffer.Count() > 0)
        {
            result.Add(buffer.Join());
            buffer.Clear();
        }

        result.Add(Convert.ToString(symbol));
    }

    else if (symbol == '(' || symbol == ')')
    {
        if (buffer.Count() > 0)
        {
            result.Add(buffer.Join());
            buffer.Clear();
        }
        result.Add(Convert.ToString(symbol));
        buffer.Clear();
    }
}

result.Add(buffer.Join());

for (var i = 0; i < result.Count(); i++)
    Console.WriteLine(result.GetValue(i));

// THE TRANSLATOR TO POSTFIX NOTATION

var output = new Queue();
var stack = new Stack();

for (int i = 0; i < result.Count(); i++)
{
    string token = result.GetValue(i);

    if (int.TryParse(result.GetValue(i), out _))
    {
        output.Enqueue(token);   
    }
    
    else if (operators.Contains(token))
    {
        while (stack.Peek() != null && stack.Peek() != "(" && (GetPrecedense(token) <= GetPrecedense(stack.Peek())))
        {
            output.Enqueue(stack.Pull());
        }
        stack.Push(token);
    }
    
    else if (token == ",")
    {
        while (stack.Peek() != "(")
        {
            output.Enqueue(stack.Pull());
        }
    }
    
    else if (token == "(")
    {
        stack.Push(token);
    }
    
    else if (token == ")")
    {
        while (stack.Peek() != "(")
        {
            output.Enqueue(stack.Pull());
        }
        stack.Pull();
    }
}

while (stack.Lenght() > 0)
{
    output.Enqueue(stack.Pull());
}

int GetPrecedense(string operat)
{
    if (operat == "+" || operat == "-")
        return 0; 
    if (operat == "*" || operat == "/")
        return 1;
    if (operat == "^")
        return 2;
    return 0;
}

// while (output.Lenght() > 0) 
//     Console.WriteLine(output.Dequeue());

// CALCULATING

var last_result = new Stack();

while (output.Lenght() > 0)
{
    var token = output.Dequeue();
    if (int.TryParse(token, out _))
    {
        last_result.Push(token);
    }
    else if (operators.Contains(token))
    {
        float first = float.Parse(last_result.Pull());
        float second = float.Parse(last_result.Pull());
        
        if (token == "+")
            last_result.Push((second + first).ToString());
        else if (token == "-")
            last_result.Push((second - first).ToString());
        else if (token == "*")
            last_result.Push((second * first).ToString());
        else if (token == "/")
            last_result.Push((second / first).ToString());
        else if (token == "^")
            last_result.Push((Math.Pow(second, first)).ToString());
    }
}

Console.WriteLine($"The result is: {last_result.Pull()}");
