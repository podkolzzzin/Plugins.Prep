using Autofac;
using Calc.Interfaces;

// (!!!) Important Reference to CalcApp.Interfaces
// (!!!) Publish options, Run Config options
[assembly: CalcPlugin] // (!!!) Important

namespace Calc.Pow;

public class Pow : ICalcAction
{
  public float Execute(float left, float right) => (float)Math.Pow(left, right);
  public string Description => "x ^ y";
}

public class PowModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<Pow>()
      .As<ICalcAction>();
    base.Load(builder);
  }
}