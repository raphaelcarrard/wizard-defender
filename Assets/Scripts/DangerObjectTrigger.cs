using UnityEngine;

public class DangerObjectTrigger : DangerObject
{
    
    public float distanceToTrigger;
    public float timeToDestroyAfterTriggered;
    private PlayerScript player;
    private float currentDistance;

    private void Start()
    {
        player = Object.FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
    }

    
    private void Update()
    {
        currentDistance = Vector3.Distance(base.transform.position, player.transform.position);
        if(currentDistance <= distanceToTrigger){
            GetComponent<Rigidbody2D>().isKinematic = false;
            Object.Destroy(base.gameObject, timeToDestroyAfterTriggered);
        }
    }
}
