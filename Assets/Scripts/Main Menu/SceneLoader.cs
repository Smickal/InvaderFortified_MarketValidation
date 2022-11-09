using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject background;


    private void Awake()
    {
        background.SetActive(false);
    }
    void Start()
    {
        StartCoroutine(EnableBackground());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EnableBackground()
    {
        yield return new WaitForSeconds(0.1f);
        background.SetActive(true);
    }
}
