using Autofac.Core;
using Autofac.Core.Registration;
using Calc.Interfaces;

namespace Calc.Arithmetics;

public class Plus : ICalcAction
{
  public string Description => "x + y";
  public float Execute(float left, float right) => left + right;
}

public class Minus : ICalcAction
{
  public string Description => "x - y";
  public float Execute(float left, float right) => left + right;
}

public class Multiply : ICalcAction
{
  public string Description => "x * y";
  public float Execute(float left, float right) => left * right;
}

public class Divide : ICalcAction
{
  public string Description => "x / y";
  public float Execute(float left, float right) => left / right;
}