using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryController : MonoBehaviour
{
    public GameObject storyText;
    public string nextScene;
    private int currentStorySection = 0;
    private string[] storySections = new string[] {
        "Story part 1 goes here.",
        "This is the second part of the story",
        "And this is the final part."
    };

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

                SceneManager.LoadScene(nextScene);
            }
            else
            {
                ProgressStory();
            }
        }
    }

    private void ProgressStory()
    {
        storyText.GetComponent<Text>().text = storySections[currentStorySection];
        currentStorySection++;
    }

}
