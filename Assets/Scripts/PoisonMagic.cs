public class PoisonMagic : BaseMagic
{
    
    public float poisonDamage;

    private new void Start()
    {
        base.Start();
    }

    
    private new void Update()
    {
        base.Update();
    }

    protected override void OnApplyDamage(LifeBase enemy){
        enemy.ApplyDamage(damage);
        enemy.ApplyPoison(poisonDamage);
    }
}
