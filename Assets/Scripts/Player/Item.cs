using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    // item
    [Header("Item")]
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite icon;

    // object
    [Header("Object")]
    [SerializeField] private Sprite sprite;

    // getters
    public string Name => _name;
    public string Description => _description;
    public Sprite Icon => icon;
    public Sprite Sprite => sprite;
}
