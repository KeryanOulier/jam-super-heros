using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] public DialogueObject dialogueObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerInteract player))
        {
            player.Interactable.Add(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerInteract player))
        {
            player.Interactable.Remove(this);
        }
    }

    public void Interact(PlayerInteract player)
    {
        if (TryGetComponent(out DialogueReponseEvents responseEvents))
        {
            player.DialogueUI.AddResponseEvents(responseEvents.Events);
        }
        player.DialogueUI.ShowDialogue(dialogueObject, GetComponent<NPC>().NPCName);
    }
}
