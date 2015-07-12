using UnityEngine;
using System.Collections;

public class CameraAnimHandler : MonoBehaviour
{
    // Messages
    public void SetCamDirection(bool isRight)
    {
        GetComponent<Animator>().SetBool("IsRight", isRight);
    }
}
