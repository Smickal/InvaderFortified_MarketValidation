using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    CancelButton cancelButton;
    public Transform rangeViewer;

    float turretRange;
    private void Awake() {
        int numOfDragAndDrop = FindObjectsOfType<DragAndDrop>().Length;
        if(numOfDragAndDrop > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        cancelButton = FindObjectOfType<CancelButton>();
    }

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Deactivate();
        DisableRangeViewer();
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }


    public void Activate(Sprite sprite)
    {
        this.spriteRenderer.sprite = sprite;
        cancelButton.ActivateCancelButton();
        EnableRangeViewer();
    }

    public void Deactivate()
    {
        this.spriteRenderer.sprite = null;
        cancelButton.DeactivateCancelButton();
        DisableRangeViewer();
    }

    public void EnableRangeViewer()
    {
        rangeViewer.localScale = Vector3.one * turretRange;
    }

    public void DisableRangeViewer()
    {
        rangeViewer.localScale = Vector3.zero;
    }

    public void SetRangeViewer(float factoryRange)
    {
        turretRange = factoryRange;
    }
}
