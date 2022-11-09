using Autofac;
using Calc.Interfaces;

[assembly: CalcPlugin]

namespace Calc.Application;

public class ApplicationModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<CalculatorApp>()
      .As<IApplication>();
    base.Load(builder);
  }
}