using System.Collections.Immutable;
using Autofac;
using Calc.Interfaces;
using MediatR;

namespace Calc.Pow;

public class HistoryModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<HistoryAction>()
      .AsImplementedInterfaces();
  }
}

public class CalculatedHandler : NotificationHandler<CalculatedEvent>
{

  protected override void Handle(CalculatedEvent notification)
  {
    File.AppendAllLines("history", new [] {
      notification.Action.Description + " " + string.Join(" ", notification.Operands.Select(x => $"({x.Info.Type.Name}){x.Value}"))
    });
  }
}

public class HistoryAction : ICalcAction
{
  private readonly IMediator _mediator;

  public HistoryAction(IMediator mediator)
  {
    _mediator = mediator;
  }

  public float Execute(ImmutableArray<OperandValue> operands)
  {
    Console.WriteLine(File.ReadAllText("history"));
    return 0f;
  }
  
  public ImmutableArray<OperandInfo> OperandInfo { get; } = ImmutableArray<OperandInfo>.Empty;
  public string Description { get; } = "Print History";
}