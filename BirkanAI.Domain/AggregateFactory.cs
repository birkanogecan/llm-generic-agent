using BirkanAI.Domain.Aggregate;
using BirkanAI.Domain.AIModel;
using BirkanAI.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BirkanAI.Domain
{
    public static class AggregateFactory
    {
        public static T CreateCommand<T>(string commandName) where T : class
        {
            Type commandType = null;

            var assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
            {
                foreach (var method in type.GetMethods())
                {
                    var attribute = method.GetCustomAttribute<FunctionAttribute>();
                    if (attribute != null && attribute.Name == commandName)
                    {
                        commandType = type;
                    }
                }
            }

            if (commandType == null)
                throw new ArgumentException($"Command '{commandName}' not found.");

            return Activator.CreateInstance(commandType) as T ?? throw new InvalidOperationException("Instance creation failed.");
        }

        public static string ExecuteCommand(string commandName, Response response)
        {
            var command = CreateCommand<IAggregate>(commandName);
            var method = command.GetType().GetMethods()
                .FirstOrDefault(m => m.GetCustomAttribute<FunctionAttribute>()?.Name == commandName);

            if (method == null)
                throw new ArgumentException($"Method with FunctionAttribute '{commandName}' not found in command '{commandName}'.");

            return method.Invoke(command, new object[] { response }) as string ?? throw new InvalidOperationException("Method invocation failed.");
        }
    }
}
