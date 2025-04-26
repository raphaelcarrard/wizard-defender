using UnityEngine;

public class BeamMagic : BaseMagic
{
    
    private PlayerScript player;
    private bool shouldGiveHit;
    public float currentTimeForHit;

    private new void Start()
    {
        base.Start();
    }

    
    private new void Update()
    {
        base.Update();
        PlayerScript player = Object.FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
        currentTimeForHit += Time.deltaTime;
        if(currentTimeForHit > timeToRefresh / 10f){
            shouldGiveHit = true;
            currentTimeForHit = 0f;
        }
        else
        {
            shouldGiveHit = false;
        }
        if(player != null){
            base.transform.position = player.transform.position;
        }
        GetComponent<BoxCollider2D>().enabled = shouldGiveHit;
    }

    protected override void OnApplyDamage(LifeBase enemy){
        enemy.ApplyDamage(damage);
    }
}
