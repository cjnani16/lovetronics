using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryController : MonoBehaviour
{
    public GameObject storyText, storyPicture, FadeIn;
    public string nextScene;
    private int currentStorySection = 0;
    private string[] storySections = new string[] {
        "PROFESSOR: Hello there! Welcome to SCRAP! In this world, you can build and battle robots!",
        "PROFESSOR: Oh, how rude of me. I haven’t even introduced myself. My name is --",
        "PROFESSOR: UWARGHH …!!!",
        "JAILBOT: THIS IS AN UNAUTHORIZED GATHERING. YOU ARE UNDER ARREST. YOU HAVE THE RIGHT TO REMAIN SILENT. ANYTHING YOU SAY CAN AND WILL BE USED AGAINST YOU IN THE COURT OF LAW.",
        "DO NOT RESIST.",
        "DO NOT RESIST.",
        "DO NOT RESIST.",
        "RESISTANCE WILL BE MET WITH LETHAL FORCE.",
        "..."
    };
    [SerializeField] public Sprite[] imgPerPanel;
    float t=0;

    void Start()
    {
        ProgressStory();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            AudioManager.instance.Play("select");
            if (currentStorySection == storySections.Length)
            {
                FadeIn.GetComponent<Animator>().Play("FadeToBlack");
            }
            else
            {
                ProgressStory();
            }
        }

        if (currentStorySection >= storySections.Length)
        {
            currentStorySection = storySections.Length;
            t+=Time.deltaTime;
            if (t>2)
                SceneManager.LoadScene(nextScene);
        }
    }

    private void ProgressStory()
    {
        storyText.GetComponent<Text>().text = storySections[currentStorySection];
        storyPicture.GetComponent<Image>().sprite = imgPerPanel[currentStorySection];
        currentStorySection++;
    }

}
