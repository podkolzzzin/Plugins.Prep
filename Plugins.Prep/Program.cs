// See https://aka.ms/new-console-template for more information

using System.Reflection;
using Autofac;
using Calc.Application;
using Calc.Arithmetics;
using Calc.Interfaces;
using MediatR.Extensions.Autofac.DependencyInjection;

await new V4().Do();

class V4
{
  public async Task Do()
  {
    var loader = new Loader();
    var container = loader.BuildContainer();
    var app = container.Resolve<IApplication>();
    await app.Run();
  }

  class Loader
  {
    public IContainer BuildContainer()
    {
      var containerBuilder = new ContainerBuilder();
      //containerBuilder.RegisterModule<ApplicationModule>();  // (!!!) Important
      //containerBuilder.RegisterModule<ArithmeticsModule>();

      LoadPlugins(containerBuilder);
      
      return containerBuilder.Build();
    }
    private void LoadPlugins(ContainerBuilder builder)
    {
      var dir = new FileInfo(GetType().Assembly.Location).Directory;
      var dlls = dir.GetFiles("*.dll");
      var asms = AppDomain.CurrentDomain.GetAssemblies();
      foreach (var dll in dlls.Where(x => IsSuitable(x.FullName)))
      {
        var defaultContext = System.Runtime.Loader.AssemblyLoadContext.Default; // (!!!) Important
        var loaded = asms.FirstOrDefault(x => x.Location.ToLowerInvariant() == dll.FullName.ToLowerInvariant());
        if (loaded == null)
        {
          loaded = defaultContext.LoadFromAssemblyPath(dll.FullName);
        }
        builder.RegisterAssemblyModules(loaded);
        builder.RegisterMediatR(loaded);
      }
    }
    private bool IsSuitable(string path)
    {
      try
      {
        var type = typeof(CalcPlugin);
        var asm = Mono.Cecil.AssemblyDefinition.ReadAssembly(path); // (!!!) Important
        return asm
          .CustomAttributes
          .Any(attribute => attribute.AttributeType.Name == type.Name && attribute.AttributeType.Namespace == type.Namespace);
      }
      catch
      {
        return false;
      }
    }
  }
}