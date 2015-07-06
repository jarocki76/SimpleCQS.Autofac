using System;
using System.Diagnostics.CodeAnalysis;
using Autofac;

namespace SimpleCQS.Autofac
{
  [ExcludeFromCodeCoverage]
  public static class ContainerBuilderExtensions
  {
    public static void RegisterComponentContextResolveAsFunc(this ContainerBuilder builder)
    {
      builder.Register(ctx =>
      {
        var c = ctx.Resolve<IComponentContext>();
        Func<Type, object> resolver = c.Resolve;
        return resolver;
      }).As<Func<Type, object>>().SingleInstance();
    }
  }
}
