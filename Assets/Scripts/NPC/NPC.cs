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
    public bool hasQuest = false;
    [SerializeField] private GameObject questIndicator;
    [SerializeField] private Item[] itemList;
    [SerializeField] private DialogueObject hello;
    void Start()
    {
        if (NPCName == "") NPCName = gameObject.name;
        // generateQuest();
    }

    public void generateQuest()
    {
        if (hasQuest)
            return;

        DialogueObject testQuest = ScriptableObject.CreateInstance<DialogueObject>();

        // generate QuestType
        int questType = UnityEngine.Random.Range(0, templates.Length);
        Quest quest = new Quest();

        NPC npc = npcList[UnityEngine.Random.Range(0, npcList.Length)];
        // while (npc.NPCName == NPCName)
        while (npc == this)
            npc = npcList[UnityEngine.Random.Range(0, npcList.Length)];
        string area = areaList[UnityEngine.Random.Range(0, areaList.Length)];
        Item item = itemList[UnityEngine.Random.Range(0, itemList.Length)];

        quest.item = item;
        switch(questType)
        {
            case 0: // DElIVER
                quest.Setup = (GameObject player) =>
                {
                    print("deliver start");
                    // setup receiver
                    var receiver = npc.gameObject.AddComponent<ItemReceiver>();
                    receiver.enabled = true;
                    receiver.quest = quest;
                    receiver.given = false;

                    // setup player
                    player.GetComponent<PlayerInventory>().addItem(quest.item);

                    // setup questGiver
                    var questGiver = gameObject;
                    questGiver.GetComponent<DialogueActivator>().dialogueObject = hello;
                };

                quest.Finish = (GameObject player) =>
                {
                    print("deliver finished");
                    // reset questGiver
                    hasQuest = false;
                    // player
                    player.GetComponent<PlayerQuest>().addScore(10);
                    // rm from list if list
                };
                break;
            case 1: // FETCH
                quest.Setup = (GameObject player) =>
                {
                    print("fetch start");
                    // setup giver
                    var giver = npc.gameObject.AddComponent<ItemGiver>();
                    giver.enabled = true;
                    giver.item = quest.item;
                    giver.given = false;

                    // setup questGiver
                    var questGiver = gameObject;
                    questGiver.GetComponent<DialogueActivator>().dialogueObject = hello;
                    var receiver = questGiver.AddComponent<ItemReceiver>();
                    receiver.enabled = true;
                    receiver.quest = quest;
                    receiver.given = false;
                };

                quest.Finish = (GameObject player) =>
                {
                    print("deliver finished");
                    // reset questGiver
                    hasQuest = false;
                    // player
                    player.GetComponent<PlayerQuest>().addScore(20);
                    // rm from list if list
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

        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        dialogueReponseEvents.Events[0].OnPickedResponse.AddListener(() =>
        {
            quest.Setup(player);
        });
        hasQuest = true;
    }

    private string fillTemplate(string format, string npc, string area)
    {
        if (npc != null)
            format = format.Replace("${npc}", "<color=red>" + npc + "</color>");
        if (area != null)
            format = format.Replace("${area}", "<color=red>" + area + "</color>");
        return format;
    }

    public void Update()
    {
        questIndicator.SetActive(hasQuest);
    }
}
