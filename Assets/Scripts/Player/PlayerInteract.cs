using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;

    public List<IInteractable> Interactable { get; set; } = new List<IInteractable>();

    public void Update()
    {
        foreach (var interactable in Interactable)
        {
            if (dialogueUI.IsOpen) {
                GetComponent<PlayerControler>().enabled = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            } else
            {
                GetComponent<PlayerControler>().enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (interactable is DialogueActivator)
                {
                    if (!DialogueUI.IsOpen)
                        interactable?.Interact(this);
                } else
                {
                    interactable?.Interact(this);
                }
            }
        }
    }
}
