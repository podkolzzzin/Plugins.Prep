namespace Calc.Interfaces;

public interface ICalcAction
{
  string Description { get; }

  float Execute(float left, float right);
}

public interface IApplication
{
  void Run();
}