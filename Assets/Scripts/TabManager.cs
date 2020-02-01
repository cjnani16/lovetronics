using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public List<GameObject> pages;
    public List<GameObject> tabs;

    public int selectedTab = 0; // 0 = chassis, 1 = leg, 2 = equip

    void Start()
    {
        setSelectedTabColor();
    }

    public void swapTab(int newTab)
    {
        selectedTab = newTab;
        setSelectedTabColor();
        for (int i = 0; i < pages.Count; i++)
        {
            if (i == selectedTab)
            {
                pages[i].SetActive(true);
            }
            else
            {
                pages[i].SetActive(false);
            }
        }
    }

    private void setSelectedTabColor()
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            if (i == selectedTab)
            {
                tabs[i].GetComponent<Image>().color = new Color32(140, 200, 200, 255);
            }
            else
            {
                tabs[i].GetComponent<Image>().color = Color.white;
            }
        }
    }

}
