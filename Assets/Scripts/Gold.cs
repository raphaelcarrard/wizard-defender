using UnityEngine;

public class Gold : MonoBehaviour
{
    
    private GameObject targetToGo;
    public bool isCollected;
    private bool wasCollected;

    private void Start()
    {
        targetToGo = GameObject.Find("goldTarget");
    }


    private void Update()
    {
        if(isCollected && !wasCollected){
            GetComponent<AudioSource>().Play();
            wasCollected = true;
        }
        if(targetToGo != null && isCollected){
            Vector3 position = Vector3.Lerp(base.transform.position, targetToGo.transform.position, 0.05f);
            base.transform.position = position;
        }
    }
}
