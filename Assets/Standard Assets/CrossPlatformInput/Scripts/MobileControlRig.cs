using UnityEngine;
using UnityEngine.EventSystems;


namespace UnityStandardAssets.CrossPlatformInput
{
    [ExecuteInEditMode]
    public class MobileControlRig : MonoBehaviour
    {

	private void OnEnable()
	{
		CheckEnableControlRig();
	}


        private void Start()
        {
            EventSystem eventSystem = Object.FindObjectOfType<EventSystem>();
            if(eventSystem == null){
                GameObject gameObject = new GameObject("EventSystem");
                gameObject.AddComponent<EventSystem>();
                gameObject.AddComponent<StandaloneInputModule>();
            }
        }


        private void CheckEnableControlRig()
        {
		    #if MOBILE_INPUT
		    EnableControlRig(true);
            #else
            EnableControlRig(false);
            #endif
        }


        private void EnableControlRig(bool enabled)
        {
            foreach (Transform item in base.transform)
            {
                item.gameObject.SetActive(enabled);
            }
        }
    }
}