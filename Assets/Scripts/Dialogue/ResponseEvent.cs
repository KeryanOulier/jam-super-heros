using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ResponseEvent
{
    [HideInInspector] public string name;
    [SerializeField] private UnityEvent onPickedResponse = new UnityEvent();

    public UnityEvent OnPickedResponse => onPickedResponse;
}
