using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewRangeOfFactory : MonoBehaviour
{
    [SerializeField] private Transform rangeViewer;
    [SerializeField]float currentRange;

    public void EnableRangePreview()
    {
        rangeViewer.localScale = Vector3.one * currentRange * 2;
    }

    public void DisableRangePreview()
    {
        rangeViewer.localScale = Vector3.zero;
    }

    public void SetRangeViewer(float factoryRange)
    {
        currentRange = factoryRange;
    }
}
