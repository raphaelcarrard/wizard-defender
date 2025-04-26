using System;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    
    [Serializable]
    public class ItemParallax{
        public Transform item;
        public float maxLimit;
        public float minLimit;
        public float moveFactor;
    }
    public Transform target;
    public bool FollowVertical;
    public float maxLimitVertical;
    public float minLimitVertical;
    public float maxLimit;
    public float minLimit;
    public List<ItemParallax> itensParallax;
    private Vector3 newPosition = new Vector3(0f, 0f, -10f);
    private Vector3 lastPosition;

    private void LateUpdate()
    {
        newPosition.x = target.position.x;
        newPosition.y = target.position.y;
        if(!(newPosition != lastPosition)){
            return;
        }
        newPosition.x = Mathf.Clamp(newPosition.x, minLimit, maxLimit);
        newPosition.y = Mathf.Clamp(newPosition.y, minLimitVertical, maxLimitVertical);
        base.transform.position = newPosition;
        foreach(ItemParallax item in itensParallax){
            Vector3 localPosition = item.item.localPosition;
            localPosition.x = Mathf.Clamp(localPosition.x, item.minLimit, item.maxLimit);
            item.item.localPosition = localPosition;
            if(lastPosition.x < newPosition.x){
                item.item.Translate(Vector3.left * Time.deltaTime * item.moveFactor);
            }
            else if(lastPosition.x > newPosition.x){
                item.item.Translate(Vector3.right * Time.deltaTime * item.moveFactor);
            }
        }
        lastPosition = newPosition;
    }
}
