using System.Collections.Immutable;
using System.ComponentModel;

namespace Calc.Interfaces;

public record OperandInfo(Type Type);

public interface ICalcAction
{
  ImmutableArray<OperandInfo> OperandInfo { get; }
  
  string Description { get; }

  float Execute(ImmutableArray<OperandValue> operands);
}

public interface IApplication
{
  Task Run();
}

public interface IPropertyProvider
{
  bool TryGetProperty<T>(string name, out T val)
  {
    val = default;
    return false;
  }

  bool TryGetObjectProperty<TObject, TProperty>(string name, TObject target, out TProperty property);
}

public interface IPropertyProvider<T>
{
  T GetProperty(string key);
  bool HasProperty(string key);
}

public interface IPropertyProvider<TObject, TProperty>
{
  TProperty GetProperty(string key, TObject target);
  bool HasProperty(string key, TObject target);
}