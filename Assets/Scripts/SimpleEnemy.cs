using UnityEngine;
using System.Collections;

public class SimpleEnemy : LifeBase
{	
	protected float speed;
    private float initSpeed;
    public float maxSpeed;
    public float minSpeed;
    public float attackDamage;
    public float timeToRecoverStun;
	public Animator enemyAnimator;
	public float distanceToAttackCastle;
	public float distanceToAttackPlayer;
	private float currentTimeToRecoverStun;
    private bool inStun;
    protected ENEMY_STATES currentState;
    protected PlayerScript player;
    public float timeToRemoveEnemy;
    private float currentTimeToRemoveEnemy;
	protected Castle castlePlayer;
	public float attackRate;
    protected float currentAttackRate;
	protected float distanceToCastle;
	protected float currentDistanceToPlayer;
	public float dropRateItem;
    public GameObject[] itensToDrop;
    private bool isFrozed;
    private float timeToBeFrozed;
    private float currentTimeToBeFrozed;


	new protected void Start()
	{
		base.Start();
        ChangeState(ENEMY_STATES.WALK);
        player = FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
        speed = Random.Range(minSpeed, maxSpeed);
        initSpeed = speed;
		castlePlayer = GameController.instance.castlePlayer;
	}

	protected override void Update()
	{
		if(GameController.instance.currentState != GAME_STATE.INGAME){
            return;
        }
		distanceToCastle = Vector3.Distance(transform.position, castlePlayer.transform.position);
		currentDistanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
		if(isFrozed){
            speed = initSpeed / 3f;
            currentTimeToBeFrozed += Time.deltaTime;
            enemyAnimator.SetBool("isFrozed", true);
            if(currentTimeToBeFrozed >= timeToBeFrozed){
                isFrozed = false;
                currentTimeToBeFrozed = 0f;
                speed = initSpeed;
                enemyAnimator.SetBool("isFrozed", false);
            }
        }
        enemyAnimator.SetBool("isPoisoned", isPoisoned);
		switch (currentState)
		{
			case ENEMY_STATES.WALK:
				transform.Translate(Vector3.left * speed * Time.deltaTime);
				if (currentDistanceToPlayer < distanceToAttackPlayer || distanceToCastle < distanceToAttackCastle)
				{
					ChangeState(ENEMY_STATES.ATTACK);
					enemyAnimator.SetBool("inAttack", true);
				}
				break;
			case ENEMY_STATES.ATTACK:
				currentAttackRate += Time.deltaTime;
				if (currentDistanceToPlayer > distanceToAttackPlayer && distanceToCastle > distanceToAttackCastle)
				{
					ChangeState(ENEMY_STATES.WALK);
					enemyAnimator.SetBool("inAttack", false);
				}
				else if (currentAttackRate > attackRate)
				{
					currentAttackRate = 0f;
					Attack();
				}
				break;
			case ENEMY_STATES.STUN:
                enemyAnimator.SetBool("inAttack", false);
                currentTimeToRecoverStun += Time.deltaTime;
                if(currentTimeToRecoverStun > timeToRecoverStun){
                    ChangeState(ENEMY_STATES.WALK);
                    currentTimeToRecoverStun = 0f;
                }
                break;
            case ENEMY_STATES.DEAD:
                currentTimeToRemoveEnemy += Time.deltaTime;
                if(currentTimeToRemoveEnemy > timeToRemoveEnemy){
                    Destroy(gameObject);
                }
                break;
		}
	}

	protected void ChangeState(ENEMY_STATES newState){
        currentState = newState;
    }

	public virtual void Attack()
	{
		if (distanceToCastle < distanceToAttackCastle)
		{
			castlePlayer.ApplyDamage(attackDamage);
		}
		else
		{
			player.GetComponent<LifePlayer>().ApplyDamage(attackDamage);
		}
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.GetComponent<Castle>() != null && col.gameObject.tag != "Enemy")
		{
			ChangeState(ENEMY_STATES.ATTACK);
			enemyAnimator.SetBool("inAttack", true);
		}
	}

	protected override void OnDamage(){
            ChangeState(ENEMY_STATES.STUN);
            enemyAnimator.SetTrigger("hit");
			currentTimeToRecoverStun = 0f;
    }

    protected override void OnDestroyIt(){
        ChangeState(ENEMY_STATES.DEAD);
        enemyAnimator.SetTrigger("die");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        DropItem();
    }

    public void DropItem(){
        float num = Random.Range(0, 100);
        if(num <= dropRateItem){
            num = Random.Range(0, 100);
            GameObject original = ((!(num > 70f)) ? itensToDrop[0] : itensToDrop[Random.Range(0, itensToDrop.Length)]);
            Object.Instantiate(original, base.transform.position, base.transform.rotation);
        }
    }

    public void ApplyColdStatus(float timeToBeFrozed){
        this.timeToBeFrozed = timeToBeFrozed;
        isFrozed = true;
    }
}
