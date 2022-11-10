using Calc.Interfaces;
using Autofac;

namespace Calc.Application;

public class PropertyProvider : IPropertyProvider
{
  private readonly IComponentContext _container;
  public PropertyProvider(IComponentContext container)
  {
    _container = container;
  }

  public bool TryGetProperty<T>(string name, out T val)
  {
    var providers = _container.ResolveOptional<IEnumerable<IPropertyProvider<T>>>() 
                 ?? Enumerable.Empty<IPropertyProvider<T>>();
    
    foreach (var propertyProvider in providers)
    {
      if (propertyProvider.HasProperty(name))
      {
        val = propertyProvider.GetProperty(name);
        return true;
      }
    }
    val = default!;
    return false;
  }
  
  public bool TryGetObjectProperty<TObject, TProperty>(string name, TObject target, out TProperty property)
  {
    var providers = _container.ResolveOptional<IEnumerable<IPropertyProvider<TObject, TProperty>>>() 
                 ?? Enumerable.Empty<IPropertyProvider<TObject, TProperty>>();
    
    foreach (var propertyProvider in providers)
    {
      if (propertyProvider.HasProperty(name, target))
      {
        property = propertyProvider.GetProperty(name, target);
        return true;
      }
    }
    property = default!;
    return false;
  }
}