public class LifePlayer : LifeBase
{
    
    private PlayerScript player;

    private new void Start()
    {
        base.Start();
        player = GetComponent<PlayerScript>();
    }

    protected override void OnDamage(bool shouldStun = true){
        base.OnDamage();
        if(IsAlive()){
            player.playerAnimator.SetTrigger("hit");
            player.ApplyStun();
        }
    }

    protected override void OnDestroyIt(){
        player.playerAnimator.SetBool("dead", true);
        GameController.instance.ChangeState(GAME_STATE.LOSE);
    }

    public void SetNewLife(float newLife){
        totalLife = newLife;
        currentLife = newLife;
    }

    public void AddLife(float life){
        currentLife += life;
        if(currentLife > totalLife){
            currentLife = totalLife;
        }
        UpdateUI();
    }
}
