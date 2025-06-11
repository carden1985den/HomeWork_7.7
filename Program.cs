abstract class Weapon
{
    abstract public void Fire();
}

class Gun:Weapon
{
    public override void Fire()
    {
        Console.WriteLine("Gso");
    }
}

class Player
{
    public void Fire(Weapon weapon)
    {
        weapon.Fire();
    }
}

class MyApp
{
    static void Main()
    {
        Gun gun = new Gun();        
        Player player = new Player();
        player.Fire(gun);
    }
}