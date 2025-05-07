namespace MyGame.MyGame.Core
{
    public interface IHealth
    {
        int Health { get; set; }
        void TakeDamage(int damage);
        void Die();
    }
}