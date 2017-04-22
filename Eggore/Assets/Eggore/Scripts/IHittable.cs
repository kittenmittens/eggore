namespace Eggore
{
    public enum HitType
    {
        None = 0,
        Bullet,
        Blast,
    }

    public interface IHittable
    {
        void OnHit(HitType hit, int damage);
    }

}
