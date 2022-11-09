class V1
{

  public void Do()
  {
    var app = new CalculatorApp();
    app.Run();
  }

  class CalculatorApp
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
}