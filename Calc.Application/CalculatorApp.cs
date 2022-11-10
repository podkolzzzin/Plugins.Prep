using System.Composition;
using Calc.Interfaces;

namespace Calc.Application;

internal class CalculatorApp : IApplication
{
  private readonly ICalcAction[] _actions;
  private readonly IInputService _inputService;
  public CalculatorApp(ICalcAction[] actions, IInputService inputService)
  {
    _actions = actions;
    _inputService = inputService;
  }

  public async Task Run()
  {
    var needContinue = true;
    do
    {
      for (int i = 0; i < _actions.Length; i++)
      {
        Console.WriteLine($"{i + 1}. {_actions[i].Description}");
      }

      
      var @operator = int.Parse(Console.ReadLine()!);

      var input = await _inputService.GetActionOperands(_actions[@operator]);
      

      if (@operator > _actions.Length || @operator <= 0)
      {
        throw new ApplicationException("The operation is not supported.");
      }
      var result = _actions[@operator - 1].Execute(input);

      Console.WriteLine(result);
      Console.WriteLine("Calc smth else?");
      needContinue = Console.ReadLine()?.ToLowerInvariant() is "yes" or "y";
    } while (needContinue);
  }
}