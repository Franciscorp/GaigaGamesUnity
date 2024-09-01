
using UnityEngine;

public class InteractionController : MonoBehaviour
{

    public GameObject interactionShadow;
    public GameObject interactionIcon;
    public Interactable interactable;

    private void Start()
    {
        if (interactionShadow == null)
            Debug.LogWarning("interactionShadow is null, please check Interactable objects");

        if (interactionIcon == null)
            Debug.LogWarning("interactionIcon is null, please check Interactable objects");

        if (interactable == null)
            Debug.LogWarning("interactable is null, please check Interactable objects");
    }

    public void SetInteractionActive(bool active)
    {
        if (interactionShadow == null || interactionIcon == null)
            return;

        interactionShadow.SetActive(active);
        interactionIcon.SetActive(active);

        if (active)
            interactable.FreeInteractionWithObject();
    }

    public void HideInteraction()
    {
        if (interactionShadow == null || interactionIcon == null)
            return;

        interactionShadow.SetActive(false);
        interactionIcon.SetActive(false);
    }

    public void ShowInteraction()
    {
        if (interactionShadow == null || interactionIcon == null)
            return;

        interactionShadow.SetActive(true);
        interactionIcon.SetActive(true);
    }

}

