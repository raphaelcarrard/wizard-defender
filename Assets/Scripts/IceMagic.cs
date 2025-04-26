public class IceMagic : BaseMagic
{
    
    public float timeToFrozeEnemy;

    private new void Start()
    {
        base.Start();
        timeToFrozeEnemy = itemShop.GetNewValueByLevel(timeToFrozeEnemy);
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
    }

    protected override void OnApplyDamage(LifeBase enemy){
        enemy.ApplyDamage(damage);
        if(enemy.GetComponent<SimpleEnemy>() != null){
            enemy.GetComponent<SimpleEnemy>().ApplyColdStatus(timeToFrozeEnemy);
        }
    }
}
