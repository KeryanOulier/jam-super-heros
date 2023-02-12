using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
public class NPC : MonoBehaviour
{
    [Header("NPC")]
    public string NPCName;

    [Header("Quests")]
    [SerializeField] private DialogueObject[] templates;
    [SerializeField] private NPC[] npcList;
    [SerializeField] private string[] areaList;
    [SerializeField] private Quest[] questList;
    public Item testItem;
    void Start()
    {
        // npcNames = Array.ConvertAll<NPC, string>(npcList, npc => npc.NPCName);
        generateQuest();
    }

    public void generateQuest()
    {
        DialogueObject testQuest = ScriptableObject.CreateInstance<DialogueObject>();

        // generate QuestType
        int questType = UnityEngine.Random.Range(0, templates.Length);
        Quest quest = new Quest();

        NPC npc = npcList[UnityEngine.Random.Range(0, npcList.Length)];
        while (npc.NPCName == NPCName)
            npc = npcList[UnityEngine.Random.Range(0, npcList.Length)];
        string area = areaList[UnityEngine.Random.Range(0, areaList.Length)];

        switch(questType)
        {
            case 0: // DElIVER
                quest.item = testItem;
                quest.Setup = (GameObject player) =>
                {
                    var receiver = gameObject.GetComponent<ItemReceiver>();
                    receiver.enabled = true;
                    receiver.quest = quest;
                    receiver.given = false;
                    player.GetComponent<PlayerInventory>().addItem(quest.item);
                };
                break;
            case 1: // FETCH
                quest.Setup = (GameObject player) =>
                {
                    print("fetch start");
                };
                break;
            case 2: // FIND
                quest.Setup = (GameObject player) =>
                {
                    print("find start");
                };
                break;
        }
        // quest.Setup(null);

        // generate Dialogue
        var baseDialogue = templates[questType];
        // testQuest.dialogue = baseDialogue.Dialogue;
        testQuest.dialogue = new string[baseDialogue.Dialogue.Length];
        testQuest.responses = baseDialogue.Responses;

        for (int i = 0; i < testQuest.Dialogue.Length; ++i)
        {
            testQuest.dialogue[i] = fillTemplate(baseDialogue.Dialogue[i], npc.NPCName, area);
        }

        DialogueActivator dialogueActivator = GetComponent<DialogueActivator>();
        dialogueActivator.enabled = true;
        dialogueActivator.dialogueObject = testQuest;

        DialogueReponseEvents dialogueReponseEvents = GetComponent<DialogueReponseEvents>();
        dialogueReponseEvents.dialogueObject = testQuest;
        dialogueReponseEvents.OnValidate();
        dialogueReponseEvents.Events[0].OnPickedResponse.AddListener(() =>
        {
            // quest.Setup();
        });
    }

    private string fillTemplate(string format, string npc, string area)
    {
        if (npc != null)
            format = format.Replace("${npc}", "<color=red>" + npc + "</color>");
        if (area != null)
            format = format.Replace("${area}", "<color=red>" + area + "</color>");
        return format;
    }
}
