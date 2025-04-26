using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    
    public GameObject objToSpawn;
    public float timeToSpawn;
    private float currentTimeToSpawn;
    private GameObject currentObject;

    private void Start()
    {
        currentTimeToSpawn = timeToSpawn;
    }

    
    private void Update()
    {
        if(currentTimeToSpawn > timeToSpawn && currentObject == null){
            currentTimeToSpawn = 0f;
            currentObject = Object.Instantiate(objToSpawn, base.transform.position, objToSpawn.transform.rotation);
        }
        else
        {
            currentTimeToSpawn += Time.deltaTime;
        }
    }
}
