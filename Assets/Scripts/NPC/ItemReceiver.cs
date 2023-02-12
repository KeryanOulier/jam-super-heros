using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReceiver : MonoBehaviour, IInteractable
{
    public bool given = false;
    public Quest quest;
    public void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled) return;

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
        if (given) return;
        // TODO make sound
        if (player.GetComponent<PlayerInventory>().removeItem(quest.item))
        {
            given = true;
            quest.Finish(player.gameObject);

            StartCoroutine(dest(player));
        }
    }

    IEnumerator dest(PlayerInteract player)
    {
        yield return null;
        player.Interactable.Remove(this);
        Destroy(this);
    }
}
