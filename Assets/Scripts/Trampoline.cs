using UnityEngine;

public class Trampoline : MonoBehaviour
{

    public float jumpMultiplyerTramp;

    public void PlayJumpAnimation(){
        GetComponent<Animator>().SetTrigger("Jump");
    }

}
