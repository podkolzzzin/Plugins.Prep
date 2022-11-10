using Autofac;
using Calc.Interfaces;
using MediatR.Extensions.Autofac.DependencyInjection;

[assembly: CalcPlugin]

namespace Calc.Application;

public class ApplicationModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<CalculatorApp>()
      .As<IApplication>();
    builder.RegisterType<ConsoleInputService>()
      .As<IInputService>();

    builder.RegisterType<ConsoleFloatInputStrategy>()
      .AsImplementedInterfaces();

    builder.RegisterType<PropertyProvider>()
      .AsImplementedInterfaces();
    
    base.Load(builder);
  }
}