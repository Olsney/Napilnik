namespace CleanCode_ExampleTask15;

class Program
{
    static void Main(string[] args)
    {
    }
    
    class PlayerInfo { }
    class Turret { }
    class TargetFollower { }
    class Unit { }
    class UnitsContainer
    {
        public IReadOnlyCollection<Unit> Units { get; private set; }
    }
}