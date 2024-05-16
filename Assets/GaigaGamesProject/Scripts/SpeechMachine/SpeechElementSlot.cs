using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static Utils;

public class SpeechElementSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private SpeechElements speechElementSlotID;

    public SpeechMachineGameEvent elementDetected;
    public CanvasGroup canvasGroup;

    private void Start()
    {
        elementDetected.AddListener(GameObject.FindGameObjectWithTag("SpeechMachineController").GetComponent<SpeechMachineGameManager>().ElementInSlotDetected);
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public SpeechElements GetSpeechElementSlotID()
    {
        return speechElementSlotID;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");

        // Catch object being dragged by the mouse
        if(eventData.pointerDrag != null)
        {
            var speechElementDragged = eventData.pointerDrag.gameObject.GetComponent<DragDrop>();
            var speechElementID = speechElementDragged.GetSpeechElementID();

            // Position are reset here
            if (speechElementSlotID == speechElementID)
            {
                Debug.Log("Speech Element is equal!");
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                DisableRaycast();
            }
            else
            {
                Debug.Log("Speech Element is different. Resetting!");
                speechElementDragged.ResetPosition();
            }

            // Analyse of logic is made in manager
            Debug.Log("Speech Element ID detected, sending event!");
            elementDetected.Invoke(speechElementSlotID, speechElementID);
        }
    }

    private void DisableRaycast()
    {
        if (canvasGroup == null)
        {
            Debug.Log("[" + speechElementSlotID + "] Doesn't have a raycast");
            return;
        }
        canvasGroup.blocksRaycasts = false;
        Debug.Log("[" + speechElementSlotID + "] raycast disabled");
    }
}
