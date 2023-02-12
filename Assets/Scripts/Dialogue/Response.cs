using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] public string responseText;
    [SerializeField] public DialogueObject dialogueObject;

    public string ResponseText => responseText;
    public DialogueObject DialogueObject => dialogueObject;
}
