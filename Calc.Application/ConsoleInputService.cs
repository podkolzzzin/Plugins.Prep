using System.Collections.Immutable;
using Calc.Interfaces;

namespace Calc.Application;

public class ConsoleInputService : IInputService
{
  private readonly IInputStrategy[] _strategies;
  
  public ConsoleInputService(IInputStrategy[] strategies)
  {
    _strategies = strategies;
  }
  

  public async Task<ImmutableArray<OperandValue>> GetActionOperands(ICalcAction action)
  {
    var info = action.OperandInfo;
    var builder = ImmutableArray.CreateBuilder<OperandValue>();
    foreach (var item in info)
    {
      builder.Add(await GetInputStrategy(item.Type).GetInputAsync(item));
    }

    return builder.ToImmutable();
  }
  
  private IInputStrategy GetInputStrategy(Type itemType)
  {
    return _strategies.Single(x => x.Type == itemType);// Can be IsAssignableFrom
  }
}

public class ConsoleFloatInputStrategy : IInputStrategy
{
  public Type Type { get; } = typeof(float);
  public Task<OperandValue> GetInputAsync(OperandInfo info)
  {
    var line = Console.ReadLine();
    var val = float.Parse(line!);
    return Task.FromResult(new OperandValue(val, info));
  }
}