using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    // TODO optimize this and check correct properties
    public bool isInRange;
    public SpriteRenderer selectionShadow;
    public SpriteRenderer interactIcon;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    // Start is called before the first frame update
    void Awake()
    {
        if (selectionShadow == null)
            Debug.LogWarning("[Interactable] Shadow not linked");
        else
            selectionShadow.enabled = false;

        if (interactIcon == null)
            Debug.LogWarning("[Interactable] interactIcon not linked");
        else
            interactIcon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if it is in range invokes action to change game mode
        // Note associated in unity event system
        if (isInRange)
        {
            // TODO improve mouse down
            if (Input.GetKeyDown(interactKey) )
            {
                InteractWithObject();
            }
            // TODO improve to new input system
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 cubeRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D cubeHit = Physics2D.Raycast(cubeRay, Vector2.zero);

                if (cubeHit)
                {
                    Debug.Log("We hit " + cubeHit.collider.name);
                    InteractWithObject();
                }
            }
        }
    }

    public void InteractWithObject()
    {
        if (isInRange)
        {
            interactAction.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player is now in range");
            selectionShadow.enabled = true;
            interactIcon.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player is now out of range range");
            selectionShadow.enabled = false;
            interactIcon.enabled = false;
        }
    }
}
