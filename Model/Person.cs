public class Person
{
    public string? Name { get; set; }
    public int Age { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is Person other)
        {
            return Name == other.Name && Age == other.Age;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Age);
    }
}