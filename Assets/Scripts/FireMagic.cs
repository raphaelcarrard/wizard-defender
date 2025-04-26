public class FireMagic : BaseMagic
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
    }

    protected override void OnApplyDamage(LifeBase enemy){
        enemy.ApplyDamage(damage);
    }
}
