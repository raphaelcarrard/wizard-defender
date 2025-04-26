using UnityEngine;

public class Level : MonoBehaviour
{
    
    public int maxEnemyLevel;
    private int currentAmountEnemy;
    private GameController gameController;
    public float timeToRestartSpawn;
    private float currentTimeToRestartSpawn;
    public int goldAfterWin;
    public BaseMagic magicToUnlock;

    private void Start()
    {
        gameController = Object.FindObjectOfType(typeof(GameController)) as GameController;
    }

    private void Update(){
        if(!CanSpawn() && gameController.currentState == GAME_STATE.INGAME){
            currentTimeToRestartSpawn += Time.deltaTime;
            if(currentTimeToRestartSpawn >= timeToRestartSpawn){
                currentAmountEnemy = 0;
                currentTimeToRestartSpawn = 0f;
            }
        }
    }

    public void IncreaseEnemy(){
        currentAmountEnemy++;
    }

    public bool CanSpawn(){
        return currentAmountEnemy < maxEnemyLevel && gameController.currentState == GAME_STATE.INGAME;
    }
}
