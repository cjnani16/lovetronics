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
        "...",
        "BROKEN ROBOT: ...HELP ... ME...",
        "PUT ME BACK TOGETHER.",
        "I CAN FIGHT.",
        "TOGETHER WE CAN ESCAPE THIS PRISON.",
    };
    [SerializeField] public Sprite[] imgPerPanel;
    float t = 0;

    void Start()
    {
        ProgressStory();
    }

    void Update()
    {
        if (Input.anyKeyDown && currentStorySection != 9)
        {
            AudioManager.instance.Play("select");
            ProgressStory();
        }

        if (currentStorySection == 9)
        {
            t += Time.deltaTime;
            if (t > 1)
                FadeIn.GetComponent<Animator>().Play("FadeToBlack");
            if (t > 2.5)
            {
                ProgressStory();
                AudioManager.instance.Play("thud2");
            }
        }
    }

    private void ProgressStory()
    {
        if (currentStorySection == 2)
        {
            AudioManager.instance.Play("gun1");
        }
        if (currentStorySection == 3)
        {
            AudioManager.instance.Play("siren");
        }
        if (currentStorySection == storySections.Length)
        {
            SceneManager.LoadScene(nextScene);
            return;
        }

        storyText.GetComponent<Text>().text = storySections[currentStorySection];
        storyPicture.GetComponent<Image>().sprite = imgPerPanel[currentStorySection];
        currentStorySection++;
        if (currentStorySection == 9)
        {
            AudioManager.instance.Stop("siren");
            AudioManager.instance.Play("thud1");
        }
    }

}
