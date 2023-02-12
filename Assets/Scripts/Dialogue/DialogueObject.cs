using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField][TextArea] public string[] dialogue;
    [SerializeField] public Response[] responses;
    public string[] Dialogue => dialogue;
    public Response[] Responses => responses;

    public bool HasResponse => responses != null && responses.Length > 0;
}
