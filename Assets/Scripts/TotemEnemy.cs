using UnityEngine;

public class TotemEnemy : SimpleEnemy
{

    public BaseMagic magicToSpawn;
    public Transform positionSpawnAttack;
    public bool spawnMagicLeft;

    public override void Attack(){
        GameObject gameObject = Object.Instantiate(magicToSpawn.gameObject, positionSpawnAttack.position, base.transform.rotation);
        BaseMagic component = gameObject.GetComponent<BaseMagic>();
        component.SetDirection(false);
        component.targetTag = "Player";
        currentAttackRate = 0f;
    }

}
