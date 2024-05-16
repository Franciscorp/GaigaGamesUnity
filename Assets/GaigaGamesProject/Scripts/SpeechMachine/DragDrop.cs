using UnityEngine;
using UnityEngine.EventSystems;
using static Utils;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    // Canvas controls the movement of the delta, so if the canvas i scaled, so will be the movement
    [SerializeField] private Canvas canvas;
    [SerializeField] private SpeechElements speechElementID;


    private RectTransform rectTransform;
    private Vector2 initialPositionTransform;
    private CanvasGroup canvasGroup;

    private bool isElementInSlot = false;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPositionTransform = rectTransform.anchoredPosition;
    }

    public SpeechElements GetSpeechElementID()
    {
        return speechElementID;
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = initialPositionTransform;
    }

    public void ElementInSlot(SpeechElements speechElementInSlotID)
    {
        // If this ID corresponds to the one in slot, then it passes to true
        if (speechElementID == speechElementInSlotID)
        {
            isElementInSlot = true;
            Debug.Log("Element = " + speechElementID + " is in Slot");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // if element is already in slot, does nothing
        if (isElementInSlot)
            return;

        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

    }
    
    // Called on every frame of dragging
    public void OnDrag(PointerEventData eventData)
    {
        // if element is already in slot, does nothing
        if (isElementInSlot)
            return;

        // the movement done by the mouse, since the last updated frame
        if (canvas != null)
        {
            rectTransform.anchoredPosition += (eventData.delta / canvas.scaleFactor);
        }
        else
        {
            Debug.LogWarning("Canvas was not linked to script: DragAndDrop");
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        
        if (!isElementInSlot)
            ResetPosition();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
}
