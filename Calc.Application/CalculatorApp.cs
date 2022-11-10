using System.Composition;
using Calc.Interfaces;
using MediatR;

namespace Calc.Application;

internal class CalculatorApp : IApplication
{
  private readonly IMediator _mediator;
  private readonly ICalcAction[] _actions;
  private readonly IInputService _inputService;
  public CalculatorApp(IMediator mediator, ICalcAction[] actions, IInputService inputService)
  {
    _mediator = mediator;
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

      var action = _actions[@operator - 1];
      var input = await _inputService.GetActionOperands(action);
      

      if (@operator > _actions.Length || @operator <= 0)
      {
        throw new ApplicationException("The operation is not supported.");
      }
      var result = _actions[@operator - 1].Execute(input);

      Console.WriteLine(result);
      Console.WriteLine("Calc smth else?");
      needContinue = Console.ReadLine()?.ToLowerInvariant() is "yes" or "y";
      
      await _mediator.Publish(new CalculatedEvent(action, input));
      
    } while (needContinue);
  }
}