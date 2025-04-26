using UnityEngine;

public abstract class BaseMagic : MonoBehaviour
{

    public float speed;
    public float timeToLife;
    public float timeToRefresh;
    public float damage;
    public bool canUseThisMagic;
    public string targetTag;
    public float manaRequest;
    public AudioClip hitSound;
    public AudioClip launchSound;
    public Animator magicAnimator;
    public GameObject glow;
    private float currentTimeToLive;
    private bool goToRight = true;
    public ItemShop itemShop;

    protected void Start()
    {
        if(targetTag == ""){
            targetTag = "Enemy";
        }
        damage = itemShop.GetNewValueByLevel(damage);
        canUseThisMagic = itemShop.IsEquiped();
        manaRequest = ManaRequest();
    }

    public float ManaRequest(){
        return itemShop.GetNewValueByLevelReverse(manaRequest);
    }

    protected void Update()
    {
        currentTimeToLive += Time.deltaTime;
        if(currentTimeToLive > timeToLife){
            Object.Destroy(base.gameObject);
        }
        Vector3 vector = ((!goToRight) ? Vector3.left : Vector3.right);
        base.transform.Translate(vector * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == targetTag){
            ApplyDamage(other.gameObject);
            GetComponent<Collider2D>().enabled = false;
        }
    }

    protected void ApplyDamage(GameObject enemy){
        OnApplyDamage(enemy.GetComponent<LifeBase>());
        magicAnimator.SetTrigger("hit");
        glow.SetActive(false);
        speed = 0f;
        GetComponent<AudioSource>().clip = hitSound;
        GetComponent<AudioSource>().PlayOneShot(hitSound);
    }

    protected abstract void OnApplyDamage(LifeBase enemyToDamage);

    public void SetDirection(bool right){
        goToRight = right;
        if(!right){
            Vector3 localScale = base.transform.localScale;
            localScale.x *= -1f;
            base.transform.localScale = localScale;
        }
    }
}
