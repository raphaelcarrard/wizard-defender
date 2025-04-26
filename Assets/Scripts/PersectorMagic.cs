using UnityEngine;

public class PersectorMagic : FireMagic
{

    private SimpleEnemy enemy;
    
    private new void Start()
    {
        base.Start();
        SetDirection(true);
        SimpleEnemy[] array = Object.FindObjectsOfType(typeof(SimpleEnemy)) as SimpleEnemy[];
        float num = 9999f;
        SimpleEnemy[] array2 = array;
        foreach(SimpleEnemy simpleEnemy in array2){
            if(Vector3.Distance(base.transform.position, simpleEnemy.transform.position) < num && simpleEnemy.IsAlive()){
                enemy = simpleEnemy;
                num = Vector3.Distance(base.transform.position, simpleEnemy.transform.position);
            }
        }
    }

    
    private new void Update()
    {
        base.Update();
        if(enemy != null && enemy.IsAlive()){
            Vector3 vector = enemy.transform.position - base.transform.position;
            float angle = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
            base.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    protected override void OnApplyDamage(LifeBase enemy){
        enemy.ApplyDamage(damage);
    }
}
