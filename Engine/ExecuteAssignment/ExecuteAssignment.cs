using System.Reflection;
using Xunit.Sdk;

namespace C_ASSIGNMENT_BUILDER.Engine.AssignmentBuilder;

public class Execution(TypeInfo typeInfo, MethodInfo methodInfo)
{
    public TypeInfo TypeInfo { get; } = typeInfo;
    public MethodInfo MethodInfo { get; } = methodInfo;

    public string Name { get { return $"{TypeInfo.Name} {MethodInfo.Name}"; } }
    public AssignmentBase? Assignment { get; private set; }
    public AssignmentBase GetAssignment() => Assignment ??= Activator.CreateInstance(TypeInfo.AsType()) as AssignmentBase ?? throw new NullReferenceException();
    public AssignmentResult Run()
    {
        var assignment = GetAssignment();
        var result = new[]{
            Try(()=>assignment.Setup()),
            Try(()=>MethodInfo.Invoke(assignment, Array.Empty<object>())),
            Try(()=>assignment.Close())
        };
        return result.Any(r => r is FailedResult) ?
        result.First(r => r is FailedResult) : result.FirstOrDefault()!;
    }
    public AssignmentResult Try(Action throwable)
    {
        try
        {
            throwable();
        }
        catch (TargetInvocationException e)
        {
            if (e.InnerException is XunitException)
            {
                return new AssertionFailedResult
                {
                    Execution = this,
                    Exception = e.InnerException
                };
            }
            return new FailedResult { Execution = this, Exception = e.InnerException ?? e };
        }
        catch (Exception e)
        {
            return new FailedResult { Execution = this, Exception = e };
        }
        return new SuccessResult { Execution = this };
    }


}
public abstract class AssignmentResult
{
    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;
    public required Execution Execution { get; set; }
    public Exception? Exception { get; set; }
    public virtual void Report()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Not yet completed assignment: {Execution.Name}");
        Console.ForegroundColor = ConsoleColor.Gray;
    }

}
public class SuccessResult : AssignmentResult
{
    public SuccessResult()
    {
        IsSuccess = true;
    }
    public override void Report()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Assignment {Execution.Name} is complete");
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}
public class FailedResult : AssignmentResult
{ };
public class AssertionFailedResult : FailedResult
{ };
public class AssignmentAttribute(int position) : Attribute
{
    public int Position { get; set; } = position;
}