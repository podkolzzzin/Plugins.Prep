using System.Collections.Immutable;
using Autofac.Core;
using Autofac.Core.Registration;
using Calc.Interfaces;

namespace Calc.Arithmetics;

public class Plus : ICalcAction
{
  public ImmutableArray<OperandInfo> OperandInfo { get; } = new[] { new OperandInfo(typeof(float)), new OperandInfo(typeof(float)) }.ToImmutableArray();
  
  public string Description => "x + y";
  
  public float Execute(ImmutableArray<OperandValue> operands) => operands.Sum(x => (float)x.Value);
}

public class Minus : ICalcAction
{
  public ImmutableArray<OperandInfo> OperandInfo { get; } = new[] { new OperandInfo(typeof(float)), new OperandInfo(typeof(float)) }.ToImmutableArray();
  
  public string Description => "x - y";
  public float Execute(ImmutableArray<OperandValue> operands) => (float)operands.First().Value - (float)operands.Last().Value;
}

public class Multiply : ICalcAction
{
  public ImmutableArray<OperandInfo> OperandInfo { get; } = new[] { new OperandInfo(typeof(float)), new OperandInfo(typeof(float)) }.ToImmutableArray();
  
  public string Description => "x * y";
  public float Execute(ImmutableArray<OperandValue> operands) => (float)operands.First().Value * (float)operands.Last().Value;
}

public class Divide : ICalcAction
{
  public ImmutableArray<OperandInfo> OperandInfo { get; } = new[] { new OperandInfo(typeof(float)), new OperandInfo(typeof(float)) }.ToImmutableArray();
  
  public string Description => "x / y";
  public float Execute(ImmutableArray<OperandValue> operands) => (float)operands.First().Value / (float)operands.Last().Value;
}