using System.Collections.Immutable;

namespace Calc.Interfaces;

public record OperandValue(object Value, OperandInfo Info);

public interface IInputService
{
  Task<ImmutableArray<OperandValue>> GetActionOperands(ICalcAction action);
}

public interface IInputStrategy
{
  Type Type { get; }
  
  Task<OperandValue> GetInputAsync(OperandInfo info);
}