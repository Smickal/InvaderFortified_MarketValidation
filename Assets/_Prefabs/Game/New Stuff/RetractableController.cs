using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetractableController : MonoBehaviour
{
    // Start is called before the first frame update

    Animator retracableAnimator;


    [SerializeField] bool isOpened = true;
    private void Awake()
    {
        retracableAnimator = GetComponent<Animator>();
    }

    public void TriggerRetracable()
    {
        
        if(isOpened)
        {
            retracableAnimator.SetTrigger("SlideOut");
            isOpened = false;
        }
        else
        {
            retracableAnimator.SetTrigger("SlideIn");
            isOpened = true;
        }
    }
}
