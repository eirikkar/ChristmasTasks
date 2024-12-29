using System.Xml.Serialization;
using C_ASSIGNMENT_BUILDER.Engine.AssignmentBuilder;
using Microsoft.VisualBasic;
using Xunit;

public class TestDatastructures : AssignmentBase
{
    Datastructures datastructures = new Datastructures()
    {
        Strings = [],
        Integers = [],
        Doubles = [],
        Strings2d = [],
        Doubles2d = [],
    };

    [Assignment(1)]
    public void ShouldBeStringArray()
    {
        datastructures.Strings = new string[5];
        Assert.Equal(datastructures.Strings, new string[5]);
    }

    [Assignment(2)]
    public void ShouldBeIntArray()
    {
        datastructures.Integers = new int[5];
        Assert.Equal(datastructures.Integers, new int[5]);
    }

    [Assignment(3)]
    public void ShouldBeDoubleArray()
    {
        datastructures.Doubles = new double[5];
        Assert.Equal(datastructures.Doubles, new double[5]);
    }

    [Assignment(4)]
    public void ShouldBeBooleanArray()
    {
        datastructures.Booleans = new bool[2];
        Assert.Equal(datastructures.Booleans, new bool[2]);
    }

    [Assignment(5)]
    public void ShouldBe2DStringArray()
    {
        Assert.Equal(datastructures.Strings2d, new string[1][]);
    }

    [Assignment(6)]
    public void ShouldBe2DIntArray()
    {
        Assert.Equal(datastructures.Integers2d, new int[1][]);
    }

    [Assignment(7)]
    public void ShouldBe2DArrayOfDoubles()
    {
        Assert.Equal(datastructures.Doubles2d, new double[1][]);
    }

    [Assignment(8)]
    public void ShouldBeListOfStrings()
    {
        Assert.Equal(new List<string>(), datastructures.StringsList);
    }

    [Assignment(9)]
    public void ShouldBeDictionaryOfIntAndStrings()
    {
        Assert.Equal(new Dictionary<int, string>(), datastructures.KeyValuePairs);
    }
}
