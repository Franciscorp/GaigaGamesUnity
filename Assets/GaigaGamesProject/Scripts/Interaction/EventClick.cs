using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // TODO finish good interaction
    // https://www.youtube.com/watch?v=mRkFj8J7y_I
    // https://www.youtube.com/watch?v=kkkmX3_fvfQ
    // https://stackoverflow.com/questions/36971880/unity-c-sharp-raycast-mouse-click
    private Interactable objectInteractable;

    private void Awake()
    {
        objectInteractable = GetComponent<Interactable>();
        Debug.Log("objectInteractable done");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pointer interacted with collider");
        objectInteractable.InteractWithObject();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
