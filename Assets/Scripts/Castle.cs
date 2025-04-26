using UnityEngine;

public class Castle : LifeBase
{
    
    public bool isPlayerCastle;
    private GameController gameController;

    private new void Start()
    {
        base.Start();
        gameController = Object.FindObjectOfType(typeof(GameController)) as GameController;
    }

    
    protected override void OnDamage()
    {
        GetComponent<Animator>().SetTrigger("Hit");
    }

    protected override void OnDestroyIt(){
        if(isPlayerCastle){
            GameController.instance.ChangeState(GAME_STATE.LOSE);
            return;
        }
        gameController.castleEnemy.Remove(this);
        Object.Destroy(base.gameObject, 1f);
    }
}
