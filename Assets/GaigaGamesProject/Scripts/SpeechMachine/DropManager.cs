using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropManager : MonoBehaviour, IDropHandler
{
    public SpeechElementSlot[] slots;
    public CanvasGroup[] slotsCanvasGroup;

    void Start()
    {
        // Assuming slots are assigned in the inspector or dynamically
        slots = GetComponentsInChildren<SpeechElementSlot>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        var speechElementDragged = eventData.pointerDrag.gameObject.GetComponent<DragDrop>();
        var speechElementID = speechElementDragged.GetSpeechElementID();

        // Search throughout all the avaiable slots to find if one matches

        foreach (var slot in slots)
        {
            if (slot.GetSpeechElementSlotID() == speechElementID)
            {
                slot.OnDrop(eventData);
                Debug.Log("[DropManager] Slot is equal");
                return;
            }
        }

        Debug.Log("[DropManager] No compatible slots");
    }


}
