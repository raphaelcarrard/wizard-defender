using UnityEngine;

public class SpawnController : MonoBehaviour
{
    
    private Level level;
    public LifeBase prefabEnemy;
    public float timeBeforeStart;
    private float currentTimeBeforeStart;
    private bool canSpawn = false;
    public float timeToSpawnEnemy;
    private float currentTimeToSpawnEnemy;
    public float offsetHeight;
    public Castle castleLinked;

    private void Start()
    {
        level = Object.FindObjectOfType(typeof(Level)) as Level;
    }

    
    private void Update()
    {
        if(!(castleLinked == null)){
            if(currentTimeBeforeStart < timeBeforeStart || !level.CanSpawn() || currentTimeToSpawnEnemy < timeToSpawnEnemy)
            {
                canSpawn = false;
            }
            else
            {
                canSpawn = true;
            }
            if(canSpawn){
                level.IncreaseEnemy();
                currentTimeToSpawnEnemy = 0f;
                Vector3 position = base.transform.position;
                position.y += Random.Range(0f - offsetHeight, offsetHeight);
                Object.Instantiate(prefabEnemy.gameObject, position, prefabEnemy.transform.rotation);
            }
            else
            {
                currentTimeToSpawnEnemy += Time.deltaTime;
                currentTimeBeforeStart += Time.deltaTime;
            }
        }
    }
}
