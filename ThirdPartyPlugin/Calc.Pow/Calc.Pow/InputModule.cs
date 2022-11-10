using System.Collections.Immutable;
using Autofac;
using Calc.Interfaces;

namespace Calc.Pow;

public class InputModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterDecorator<MoreComplexInput, IInputService>();
    base.Load(builder);
  }
}

internal class MoreComplexInput : IInputService
{
  private readonly IInputService _originalService;

  public MoreComplexInput(IInputService originalService)
  {
    _originalService = originalService;
  }

  public Task<ImmutableArray<OperandValue>> GetActionOperands(ICalcAction action)
  {
    var vals = ImmutableArray.CreateBuilder<OperandValue>();
    foreach (var operandInfo in action.OperandInfo)
    {
      Console.Write($"Input {operandInfo.Type.Name} operand?>");
      vals.Add(new OperandValue(float.Parse(Console.ReadLine()), operandInfo));
    }
    return Task.FromResult(vals.ToImmutable());
  }
}