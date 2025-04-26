using UnityEngine;

public class Potion : MonoBehaviour
{
    
    public int amount;
    public POTION_TYPE typePotion;
    public int timeToDestroy = 10;
    private float currentTimeLiving;
    private float currentTimeBlink;

    private void Start()
    {
        Object.Destroy(base.gameObject, timeToDestroy);
    }

    
    private void Update()
    {
        currentTimeLiving += Time.deltaTime;
        if(currentTimeLiving > (float)(timeToDestroy / 2)){
            currentTimeBlink += Time.deltaTime;
            if(currentTimeBlink > 0.2f){
                currentTimeBlink = 0f;
                GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            }
        }
    }
}
