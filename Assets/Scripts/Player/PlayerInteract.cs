using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;

    public IInteractable Interactable { get; set; }

    public void Update()
    {
        if (dialogueUI.IsOpen) {
            GetComponent<PlayerControler>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        } else
        {
            GetComponent<PlayerControler>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && !DialogueUI.IsOpen)
        {
            Interactable?.Interact(this);
        }
    }
}
