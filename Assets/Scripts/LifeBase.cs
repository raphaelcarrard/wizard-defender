using UnityEngine;

public abstract class LifeBase : MonoBehaviour
{
    public float totalLife;
    protected float currentLife;
    public Transform lifeBar;
    private Vector3 startSizeLifeBar;
    private Vector3 currentSizeLifeBar;
    public float poisonTime;
    protected bool isPoisoned;
    private float currentPoisonTime;
    private float poisonDamage;
    public int poisonCicle = 10;
    private int currentPoisonCicle;
    protected bool isAlive = true;

    protected void Start()
    {
        currentLife = totalLife;
        startSizeLifeBar = lifeBar.localScale;
        currentSizeLifeBar = lifeBar.localScale;
    }

    protected virtual void Update()
    {
        if(!isPoisoned){
            return;
        }
        currentPoisonTime += Time.deltaTime;
        if(currentPoisonTime > poisonTime){
            ApplyDamage(poisonDamage, false);
            currentPoisonCicle++;
            if(currentPoisonCicle > poisonCicle){
                isPoisoned = false;
                currentPoisonCicle = 0;
            }
            currentPoisonTime = 0f;
        }
    }

    public bool IsAlive(){
        return currentLife > 0f;
    }

    public void ApplyPoison(float damage){
        poisonDamage = damage;
        isPoisoned = true;
        currentPoisonTime = 0f;
    }

    public void ApplyDamage(float damage, bool shouldStun = true){
        currentLife -= damage;
        OnDamage(shouldStun);
        if(!IsAlive()){
            OnDestroyIt();
            isAlive = false;
        }
        UpdateUI();
    }

    protected void UpdateUI(){
        currentSizeLifeBar.x = currentLife * startSizeLifeBar.x / totalLife;
        if(currentSizeLifeBar.x < 0f){
            currentSizeLifeBar.x = 0f;
        }
        lifeBar.localScale = currentSizeLifeBar;
    }

    protected virtual void OnDamage(bool shouldStun = true){

    }

    protected virtual void OnDamage(){

    }

    protected abstract void OnDestroyIt();
}
