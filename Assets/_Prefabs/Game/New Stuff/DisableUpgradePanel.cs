using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableUpgradePanel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] UpgradePanel panel;
    [SerializeField] Vector3 smallSize;
    [SerializeField] Vector3 smallPosition;

    Vector3 fullPosition;
    Vector3 fullSize;




    private void OnMouseDown()
    {
        panel.DisableUpgradePanel();
        FindObjectOfType<allNodes>().DisableAllPreviews();
    }

    private void Start()
    {
        fullSize = transform.localScale;
        fullPosition = transform.position;
    }

    private void Update()
    {
        if (panel.isUpgradePanelActivated)
        {
            transform.localScale = smallSize;
            transform.localPosition = smallPosition;
        }
        else
        {
            transform.localScale = fullSize;
            transform.localPosition = fullPosition;
        }
    }
}
