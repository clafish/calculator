using calculator_Andrii_Korzh;
using Queue = calculator_Andrii_Korzh.Queue;
using Stack = calculator_Andrii_Korzh.Stack;

Console.WriteLine("Enter your expression: ");
string expression = Console.ReadLine();
expression = expression.Replace(" ", "");

var operators = new ArrayList();
operators.Add("+");
operators.Add("-");
operators.Add("*");
operators.Add("/");
operators.Add("^");

var letters = new ArrayList();
letters.Add("s");
letters.Add("i");
letters.Add("n");
letters.Add("c");
letters.Add("o");
letters.Add("t");
letters.Add("g");
letters.Add("m");
letters.Add("a");
letters.Add("x");

var possibleFunctions = new ArrayList();
possibleFunctions.Add("sin");
possibleFunctions.Add("cos");
possibleFunctions.Add("tg");
possibleFunctions.Add("ctg");
possibleFunctions.Add("max");

// TOKENIZER

var result = new ArrayList();
var buffer = new ArrayList();
var functionsBuffer = new ArrayList();

foreach (char symbol in expression)
{
    if (char.IsNumber(symbol))
    {
        buffer.Add(symbol.ToString());
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

        if (functionsBuffer.Count() > 0)
        {
            result.Add(functionsBuffer.Join());
            functionsBuffer.Clear();
        }
        
        result.Add(Convert.ToString(symbol));
        buffer.Clear();
        functionsBuffer.Clear();
    }
    
    else if (symbol == '.' || symbol == ',')
    {
        buffer.Add(symbol.ToString());
    }
    
    else if (symbol == ';')
    {
        if (buffer.Count() > 0)
        {
            result.Add(buffer.Join());
            buffer.Clear();
        }
        result.Add(symbol.ToString());
    }
    
    else if (letters.Contains(symbol.ToString()))
    {
        functionsBuffer.Add(symbol.ToString());
    }
}

result.Add(buffer.Join());

for (var i = 0; i < result.Count(); i++)
{
    for (int j = 0; j < letters.Count(); j++)
    {
        if (result.GetValue(i).Contains(letters.GetValue(j)))
        {
            if (!possibleFunctions.Contains(result.GetValue(i)))
                throw new Exception("Wrong function");
            // if (possibleFunctions.Contains(result.GetValue(i)) && (!result.Contains(")" ) || !result.Contains("(")))
            //     throw new Exception("Invalid syntax");
        }
    }
    if (result.GetValue(i).Contains("."))
    {
        var value = result.GetValue(i).Replace(".", ",");
        result.Insert(i, value);
    }
}

// for (var i = 0; i < result.Count(); i++)
//     Console.WriteLine(result.GetValue(i));

// THE TRANSLATOR TO POSTFIX NOTATION

var output = new Queue();
var stack = new Stack();
var astQueue = new Queue();

for (int i = 0; i < result.Count(); i++)
{
    string token = result.GetValue(i);

    if (double.TryParse(token, out _))
    {
        output.Enqueue(token);   
        astQueue.Enqueue(token); 
    }

    else if (possibleFunctions.Contains(token))
    {
        stack.Push(token);
    }
    
    else if (operators.Contains(token))
    {
        while (stack.Peek() != null && stack.Peek() != "(" && GetPrecedense(stack.Peek()) >= GetPrecedense(token))
        {
            var varuable = stack.Pull();
            output.Enqueue(varuable);
            astQueue.Enqueue(varuable); 
        }
        stack.Push(token);
    }
    
    else if (token == "(")
    {
        stack.Push(token);
    }
    
    else if (token == ")")
    {
        while (stack.Peek() != "(")
        {
            var varuable = stack.Pull();
            output.Enqueue(varuable);
            astQueue.Enqueue(varuable); 
        }
        
        if (stack.Peek() == "(")
        {
            stack.Pull();
        }
        
        if (possibleFunctions.Contains(stack.Peek()))
        {
            var varuable = stack.Pull();
            output.Enqueue(varuable);
            astQueue.Enqueue(varuable); 
        }
    }
    
    else if (token == ";")
    {
        while (stack.Peek() != null && stack.Peek() != "(")
        {
            var varuable = stack.Pull();
            output.Enqueue(varuable);
            astQueue.Enqueue(varuable);
        }
    }
}

while (stack.Lenght() > 0)
{
    if (stack.Peek() == null)
        break;
    var varuable = stack.Pull();
    output.Enqueue(varuable);
    astQueue.Enqueue(varuable);
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

var lastResult = new Stack();

while (output.Lenght() > 0)
{
    var token = output.Dequeue();
    if (token.Length > 0 && char.IsDigit(token[0])) 
    {
        lastResult.Push(token);
    }
    
    else if (operators.Contains(token))
    {
        
        float first = float.Parse(lastResult.Pull().Replace(".", ","));
        float second = float.Parse(lastResult.Pull().Replace(".", ","));
        
        if (token == "+")
            lastResult.Push((second + first).ToString());
        else if (token == "-")
            lastResult.Push((second - first).ToString());
        else if (token == "*")
            lastResult.Push((second * first).ToString());
        else if (token == "/")
            lastResult.Push((second / first).ToString());
        else if (token == "^")
            lastResult.Push((Math.Pow(second, first)).ToString());
    }
    
    else if (possibleFunctions.Contains(token))
    {
        
        if (token == "max")
        {
            float first = float.Parse(lastResult.Pull().Replace(".", ","));
            float second = float.Parse(lastResult.Pull().Replace(".", ","));
            lastResult.Push(Math.Max(first, second).ToString());
        }
        
        else
        {
            float degrees = float.Parse(lastResult.Pull().Replace(".", ","));
            double radians = degrees * Math.PI / 180;
        
            if (token == "sin")
                lastResult.Push(Math.Round(Math.Sin(radians), 4).ToString());
        
            else if (token == "cos")
                lastResult.Push(Math.Round(Math.Cos(radians), 4).ToString());
        
            else if (token == "tg")
            {
                if (Math.Abs(degrees % 180) == 90)
                    throw new Exception("Tangents is undefined");
                lastResult.Push(Math.Round(Math.Tan(radians), 4).ToString());
            }
        
            else if (token == "ctg")
            {
                if (degrees % 180 == 0)
                    throw new Exception("Cotangents is undefined");
                lastResult.Push(Math.Round((1 / Math.Tan(radians)), 4).ToString());
            }
        }
    }
}

Console.WriteLine($"The result is: {lastResult.Pull()}");

var ast = new AST();
ast._queue = astQueue;
ast.Algorithm();
ast.Draw(ast._nodes.Pull(), "", false, true);
