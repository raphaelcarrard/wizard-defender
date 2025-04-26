using UnityEngine;

public class FlyerEnemy : SimpleEnemy
{
    public BaseMagic magicAttack;
    public Transform positionSpawnAttack;
    
    private new void Start()
    {
        base.Start();
        currentAttackRate = attackRate;
    }

    
    protected override void Update()
    {
        base.Update();
        switch(currentState){
            case ENEMY_STATES.ATTACK:
                if(distanceToCastle >= distanceToAttackCastle){
                    base.transform.Translate(Vector3.left * speed * Time.deltaTime);
                }
                break;
            case ENEMY_STATES.DEAD:
                GetComponent<Rigidbody2D>().gravityScale = 1f;
                break;
        }
    }

    public override void Attack(){
        GameObject gameObject = Object.Instantiate(magicAttack.gameObject, positionSpawnAttack.position, base.transform.rotation);
        BaseMagic component = gameObject.GetComponent<BaseMagic>();
        component.SetDirection(false);
        if(distanceToCastle >= distanceToAttackCastle){
            component.targetTag = "Player";
        }
        else
        {
            component.targetTag = "Castle";
        }
        currentAttackRate = 0f;
    }
}
