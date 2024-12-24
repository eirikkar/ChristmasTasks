using System.Collections;
using System.Reflection;
namespace C_ASSIGNMENT_BUILDER.Engine.AssignmentBuilder;

public abstract class AssignmentListBase : IEnumerable<Type>
{

    internal Type[]? Types;
    public int Walkthrough(Guide guide)
    {
        try
        {
            ForEachExecution(exec => guide.Observe(exec.Run()));
        }
        catch (GuideException)
        { }
        guide.Instruct(this);
        return guide.Failed() ? -1 : 0;
    }
    public void ForEachAssignment(Action<TypeInfo> action)
    {
        foreach (var type in this)
        {
            action(type.GetTypeInfo());
        }
    }
    public void ForEachExecution(Action<Execution> action)
    {
        ForEachAssignment(typeInfo =>
        {
            var methods = typeInfo.GetMethods()
            .Where(m => m.GetCustomAttributes(typeof(AssignmentAttribute), false).Any())
            .OrderBy(m => m.GetCustomAttributes(typeof(AssignmentAttribute), false)
                                        .Cast<AssignmentAttribute>().Single().Position)
            .ToArray();
            foreach (var method in methods)
            {
                action(new Execution(typeInfo, method));
            }
        });
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<Type>)Types!).GetEnumerator();
    }

    IEnumerator<Type> IEnumerable<Type>.GetEnumerator()
    {
        return ((IEnumerable<Type>)Types!).GetEnumerator();
    }
}