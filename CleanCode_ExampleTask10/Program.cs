namespace CleanCode_ExampleTask10;

class Program
{
    static void Main(string[] args)
    {
    }
    
    class Weapon
    {
        private int _bulletsAmount;
        private int _bulletsPerShot = 1;

        public bool CanShoot => _bulletsAmount > _bulletsPerShot;

        public void Shoot()
        {
            if (CanShoot == false)
                throw new InvalidOperationException("Not enough bullets.");

            _bulletsAmount -= _bulletsPerShot;
        }
    }
}