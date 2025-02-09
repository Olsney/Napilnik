namespace medium8_1_4;

class Program
{
    static void Main(string[] args)
    {
    }
}

class Player
{
    private readonly Movement _movement;
    private readonly Weapon _weapon;

    public Player(string name, int age, Movement movement, Weapon weapon)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"\"{nameof(name)}\" cannot be empty or contain only space", nameof(name));
        }

        if (age <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(age));
        }

        if (movement is null)
        {
            throw new ArgumentNullException(nameof(movement));
        }

        if (weapon is null)
        {
            throw new ArgumentNullException(nameof(weapon));
        }

        Name = name;
        Age = age;
        _movement = movement;
        _weapon = weapon;
    }

    public string Name { get; private set; }
    public int Age { get; private set; }
}

class Movement
{
    public Movement(float directionY, float directionX, float speed)
    {
        DirectionY = directionY;
        DirectionX = directionX;
        Speed = speed;
    }

    public float DirectionX { get; private set; }
    public float DirectionY { get; private set; }
    public float Speed { get; private set; }

    public void Move()
    {
        //Do move
    }
}

class Weapon
{
    public Weapon(float cooldown, int damage)
    {
        if(cooldown <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cooldown));
        }

        if (damage <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cooldown));
        }

        Cooldown = cooldown;
        Damage = damage;
    }

    public float Cooldown { get; }
    public int Damage { get; }

    public void Attack()
    {
        //attack
    }

    public bool IsReloading()
    {
        throw new NotImplementedException();
    }
}