using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public NPC[] npcList;
    private float cd = 0;
    // Update is called once per frame
    void Update()
    {
        if (cd > 0)
        {
            cd -= Time.deltaTime;
        }
        else
        {
            cd = Random.Range(10f, 20f);
            int nbTime = Random.Range(1, 3);
            for (int i = 0; i < nbTime; i++) {
                int npcIndex = Random.Range(0, npcList.Length);
                bool allQuest = true;
                foreach (NPC npc in npcList)
                {
                    if (!npc.hasQuest)
                    {
                        allQuest = false;
                        break;
                    }
                }
                if (allQuest)
                {
                    print("All NPC have a quest");
                    break;
                }
                while (npcList[npcIndex].hasQuest)
                    npcIndex = Random.Range(0, npcList.Length);
                npcList[npcIndex].generateQuest();
                print(npcList[npcIndex].NPCName + " has a new quest");
            }
        }
    }
}
