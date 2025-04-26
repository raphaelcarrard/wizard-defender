using UnityEngine;
using System.Collections.Generic;

public class Platform : MonoBehaviour
{

    public List<Transform> wayPoints;
    public float toleranceDistanceToChange;
    private Vector3 targetPlatform;
    private int currentWayPoint;
    public float dumbMovement;
    
    private void Start()
    {
        foreach(Transform wayPoint in wayPoints){
            wayPoint.SetParent(null);
        }
        ChangeWayPoint();
    }

    
    private void Update()
    {
        float num = Vector3.Distance(targetPlatform, base.transform.position);
        if(num > toleranceDistanceToChange){
            base.transform.position = Vector3.Lerp(base.transform.position, targetPlatform, dumbMovement * Time.deltaTime);
        }
        else
        {
            ChangeWayPoint();
        }
    }

    private void ChangeWayPoint(){
        if(currentWayPoint < wayPoints.Count - 1){
            currentWayPoint++;
        }
        else
        {
            currentWayPoint = 0;
        }
        targetPlatform = wayPoints[currentWayPoint].position;
    }
}
