using System.Diagnostics.CodeAnalysis;
using Autofac;
using SimpleCQS.Command;
using SimpleCQS.Command.Validation;
using SimpleCQS.Query;

namespace SimpleCQS.Autofac
{
  [ExcludeFromCodeCoverage]
  public static class ContainerBuilderSimpleCQSExtensions
  {
    public static void RegisterSimpleCQS(this ContainerBuilder builder)
    {
      builder.RegisterComponentContextResolveAsFunc();

      builder.RegisterType<ValidationProcessor>().As<IValidationProcessor>().SingleInstance();

      const string name = "Dispatcher";
      builder.RegisterType<CommandDispatcher>().Named<ICommandDispatcher>(name).SingleInstance();
      builder.RegisterDecorator<ICommandDispatcher>(
        (c, inner) => new ValidatedCommandDispatcher(inner, c.Resolve<IValidationProcessor>()), name).SingleInstance();

      builder.RegisterType<CommandExecutor>().As<ICommandExecutor>().SingleInstance();
      builder.RegisterType<QueryExecutor>().As<IQueryExecutor>().SingleInstance();
    } 
  }
}