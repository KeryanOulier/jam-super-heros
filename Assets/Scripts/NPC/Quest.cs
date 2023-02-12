using UnityEngine;
using System;

public class Quest
{
    public Item item;
    public Action<GameObject> Setup;
    public Action<GameObject> Finish;
}
