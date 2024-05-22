using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropManager : MonoBehaviour, IDropHandler
{
    public SpeechElementSlot[] slots;
    public CanvasGroup[] slotsCanvasGroup;

    // Note: Somewhat a bad solution, but drop manager sends the signal to the controller
    public SpeechMachineGameEvent elementDetected;


    void Start()
    {
        // Assuming slots are assigned in the inspector or dynamically
        slots = GetComponentsInChildren<SpeechElementSlot>();
        elementDetected.AddListener(GameObject.FindGameObjectWithTag("SpeechMachineController").GetComponent<SpeechMachineGameManager>().ElementInSlotDetected);
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

        // TODO doesn't detect the right slot and can't suggest a wrong message correctly
        elementDetected.Invoke(UtilsSpeechMachine.SpeechElements.None, speechElementID);
        Debug.Log("[DropManager] No compatible slots, sending signal");
    }


}
