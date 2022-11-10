using System.Collections.Immutable;
using Autofac;
using Calc.Interfaces;

[assembly: CalcPlugin]

namespace Calc.WinFormsInput;

public class WinFormsInputModule : Autofac.Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<WinFormsInputService>()
      .AsImplementedInterfaces();
  }
}

public class WinFormsInputService : IInputService
{

  public Task<ImmutableArray<OperandValue>> GetActionOperands(ICalcAction action)
  {
    //ApplicationConfiguration.Initialize();
    var form = new InputForm(action.OperandInfo);
    form.Show();
    var tsc = new TaskCompletionSource<ImmutableArray<OperandValue>>();
    form.Closed += (_, __) =>
    {
      tsc.SetResult(form.GetResult());
    };
    return tsc.Task;
  }
}