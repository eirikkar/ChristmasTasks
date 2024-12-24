using System.Drawing;
using System.Text;

namespace C_ASSIGNMENT_BUILDER.Engine.AssignmentBuilder;

public class Guide
{
    private ICollection<AssignmentResult> Assignments;
    public Guide()
    {
        Assignments = new List<AssignmentResult>();
    }
    public bool Failed()
    {
        return Assignments.Any(a => a is FailedResult);
    }
    public void Observe(AssignmentResult result)
    {
        Assignments.Add(result);
    }
    public void Instruct(AssignmentListBase list)
    {
        if (Failed())
        {
            foreach (var result in Assignments)
            {
                result.Report();
            }
            ShowProgress(list);
        }
        else
        {
            EndAssignment();
        }
    }
    public void ShowProgress(AssignmentListBase list)
    {
        int stepCount = 0;
        list.ForEachExecution(s => stepCount++);
        int stepsNotCompleted = stepCount - Assignments.Count(a => a.IsSuccess);
        var ratio = GenerateRatio(stepCount - stepsNotCompleted, stepCount);
        var graph = GenerateGraph(ratio);
        Console.ForegroundColor = ColorByRatio(ratio);
        Console.WriteLine(graph);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    private string GenerateGraph(int ratio, int scale = 100)
    {
        var sb = new StringBuilder();
        sb.Append(" [");
        sb.Append('#', ratio);
        sb.Append('_', scale - ratio);
        sb.Append(']');
        return sb.ToString();
    }
    private void EndAssignment()
    {
        Console.WriteLine(string.Empty);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("All assignments complete");
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    private ConsoleColor ColorByRatio(int ratio)
    {
        ConsoleColor color = ratio switch
        {
            int i when i < 25 => ConsoleColor.Red,
            int i when i < 50 => ConsoleColor.Yellow,
            int i when i < 75 => ConsoleColor.Blue,
            _ => ConsoleColor.Green

        };
        return color;
    }
    private int GenerateRatio(int completed, int all, int scale = 100)
    {
        int ratio = completed == 0 ? 1 : (int)Math.Round((double)completed / all * scale);
        return ratio;
    }
}
public class GuideException : Exception;