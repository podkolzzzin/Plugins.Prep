using Autofac;

class V3
{
  public void Do()
  {
    var loader = new Loader();
    var container = loader.BuildContainer();
    var app = container.Resolve<CalculatorApp>();
    app.Run();
  }
  
  class Loader
  {
    public IContainer BuildContainer()
    {
      var containerBuilder = new ContainerBuilder();
      containerBuilder.RegisterType<Plus>().As<ICalcAction>();
      containerBuilder.RegisterType<Minus>().As<ICalcAction>();
      containerBuilder.RegisterType<Multiply>().As<ICalcAction>();
      containerBuilder.RegisterType<Divide>().As<ICalcAction>();

      containerBuilder.RegisterType<CalculatorApp>()
        .AsSelf();

      return containerBuilder.Build();
    }
  }

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
  
  public interface ICalcAction
  {
    public string Description { get; }
    
    public float Execute(float left, float right);
  }

  class CalculatorApp
  {
    private readonly ICalcAction[] _actions;
    public CalculatorApp(params ICalcAction[] actions)
    {
      _actions = actions;
    }
    
    public void Run()
    {
      var needContinue = true;
      do
      {
        for (int i = 0; i < _actions.Length; i++)
        {
          Console.WriteLine($"{i + 1}. {_actions[i].Description}");
        }
        
        var @operator = int.Parse(Console.ReadLine()!);
        var left = float.Parse(Console.ReadLine()!);
        var right = float.Parse(Console.ReadLine()!);
        
        if (@operator > _actions.Length || @operator <= 0)
        {
          throw new ApplicationException("The operation is not supported.");
        }
        var result = _actions[@operator - 1].Execute(left, right);
        
        Console.WriteLine(result);
        Console.WriteLine("Calc smth else?");
        needContinue = Console.ReadLine()?.ToLowerInvariant() is "yes" or "y";
      } while (needContinue);
    }
  }
}