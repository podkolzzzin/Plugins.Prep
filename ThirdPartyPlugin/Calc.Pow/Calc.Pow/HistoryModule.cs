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
    builder.RegisterType<BasicActionsExtendedView>()
      .AsImplementedInterfaces();
  }
}

public class BasicActionsExtendedView : IPropertyProvider<ICalcAction, string?>
{
  public string? GetProperty(string key, ICalcAction target)
  {
    if (target.Description.Contains("+"))
      return "{0}+{1}";
    if (target.Description.Contains("-"))
      return "{0}-{1}";
    if (target.Description.Contains("*"))
      return "{0}*{1}";
    if (target.Description.Contains("/"))
      return "{0}/{1}";
    return null;
  }
  
  public bool HasProperty(string key, ICalcAction target) => key == "ExtendedView" && GetProperty(key, target) != null;
}

public class CalculatedHandler : NotificationHandler<CalculatedEvent>
{
  private readonly IPropertyProvider _propertyProvider;

  public CalculatedHandler(IPropertyProvider propertyProvider)
  {
    _propertyProvider = propertyProvider;

  }

  protected override void Handle(CalculatedEvent notification)
  {
    string view;
    if (_propertyProvider.TryGetObjectProperty("ExtendedView", notification.Action, out string template))
    {
      view = string.Format(template, notification.Operands.Select(x => x.Value).ToArray());
    }
    else
    {
      view = notification.Action.Description + " " + string.Join(" ", notification.Operands.Select(x => $"({x.Info.Type.Name}){x.Value}"));
    }
    File.AppendAllLines("history", new [] {
      view
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