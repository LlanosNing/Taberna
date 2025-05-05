using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class World3Manager : MonoBehaviour
{
    public int rootsCollected;
    bool world3Cleared;
    public TextMeshProUGUI uiText;

    public ParticleSystem playerQuestVFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rootsCollected >= 3 && !world3Cleared)
        {
            Level3Completed();
        }
    }

    public void AddRoot()
    {
        rootsCollected++;
        uiText.text = rootsCollected.ToString();
    }

    void Level3Completed()
    {
        GetComponent<GameManager>().NextQuest();

        world3Cleared = true;

        GameObject.FindWithTag("QuestCompleted").GetComponent<Animator>().SetTrigger("QuestCompleted");

        playerQuestVFX.Play();
    }
}
