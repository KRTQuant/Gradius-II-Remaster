using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateLogo : MonoBehaviour
{
    public bool AnimateEnd = false;  

    public void OnAnimationEnd()
    {
        AnimateEnd = true;
    }
}
