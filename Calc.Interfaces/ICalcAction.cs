using System.Collections.Immutable;

namespace Calc.Interfaces;

public record OperandInfo(Type Type);

public interface ICalcAction
{
  ImmutableArray<OperandInfo> OperandInfo { get; }
  
  string Description { get; }

  float Execute(ImmutableArray<OperandValue> operands);
}

public interface IApplication
{
  Task Run();
}