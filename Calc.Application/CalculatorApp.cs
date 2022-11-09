using Calc.Interfaces;

namespace Calc.Application;

internal class CalculatorApp : IApplication
{
  private readonly ICalcAction[] _actions;
  public CalculatorApp(params ICalcAction[] actions)
  {
    _actions = actions;
  }

  public void Run()
  {
    var needContinue = true;
    do
    {
      for (int i = 0; i < _actions.Length; i++)
      {
        Console.WriteLine($"{i + 1}. {_actions[i].Description}");
      }

      var @operator = int.Parse(Console.ReadLine()!);
      var left = float.Parse(Console.ReadLine()!);
      var right = float.Parse(Console.ReadLine()!);

      if (@operator > _actions.Length || @operator <= 0)
      {
        throw new ApplicationException("The operation is not supported.");
      }
      var result = _actions[@operator - 1].Execute(left, right);

      Console.WriteLine(result);
      Console.WriteLine("Calc smth else?");
      needContinue = Console.ReadLine()?.ToLowerInvariant() is "yes" or "y";
    } while (needContinue);
  }
}