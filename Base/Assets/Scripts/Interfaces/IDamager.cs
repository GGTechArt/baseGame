public interface IDamager
{
    public float damage { get; set; }

    public void SetDamage(IDamageable damageable);
}
