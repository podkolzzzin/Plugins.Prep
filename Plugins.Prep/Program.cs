// See https://aka.ms/new-console-template for more information

var app = new CalculatorAppV1();
app.Run();

class CalculatorAppV1
{
  public void Run()
  {
    var needContinue = true;
    do
    {
      Console.WriteLine("1. x + y");
      Console.WriteLine("2. x - y");
      Console.WriteLine("3. x * y");
      Console.WriteLine("4. x / y");
      var @operator = int.Parse(Console.ReadLine()!);
      var left = double.Parse(Console.ReadLine()!);
      var right = double.Parse(Console.ReadLine()!);
      var result = @operator switch {
        1 => left + right,
        2 => left - right,
        3 => left * right,
        4 => left / right,
        _ => throw new ApplicationException("The operation is not supported.")
      };
      
      Console.WriteLine(result);
      Console.WriteLine("Calc smth else?");
      needContinue = Console.ReadLine()?.ToLowerInvariant() is "yes" or "y";
    } while (needContinue);
  }
}

// Plugin Load
// Interact with Host App
//   - Provide some data
//   - Add Behavior
//   - Replace Behavior
//   - Modify Behavior
// Interact with other Plugins
//   - Hard dependency - plugin requires another plugin
//   - Soft dependence - plugin can use functions of other plugin but it is not required
//   - via Host Interfaces
//   - via Plugin Interfaces