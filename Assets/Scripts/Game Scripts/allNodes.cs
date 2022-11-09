using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allNodes : MonoBehaviour
{

    public void DisableAllNodes()
    {
        Node[] nodes = GetComponentsInChildren<Node>();
        
        foreach(Node node in nodes)
        {
            node.DisableCurrentSelectedNode();
        }
    }

    public void DisableAllPreviews()
    {
        PreviewRangeOfFactory[] prevTemps = GetComponentsInChildren<PreviewRangeOfFactory>();
        foreach(PreviewRangeOfFactory prevTemp in prevTemps)
        {
            prevTemp.DisableRangePreview();
        }
    }
}
