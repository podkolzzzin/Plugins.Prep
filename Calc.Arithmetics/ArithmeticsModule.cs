using Autofac;
using Calc.Interfaces;

[assembly: CalcPlugin]

namespace Calc.Arithmetics;

public class ArithmeticsModule : Module 
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<Plus>().As<ICalcAction>();
    builder.RegisterType<Minus>().As<ICalcAction>();
    builder.RegisterType<Multiply>().As<ICalcAction>();
    builder.RegisterType<Divide>().As<ICalcAction>();
  }
}