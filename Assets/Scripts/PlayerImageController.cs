using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerImageController : MonoBehaviour
{

    public Sprite sprite;
    void Start()
    {
        Image img = GetComponent<Image>();
        img.sprite = GameManager.manager.getSprite();
        img.sprite = Resources.Load<Sprite>("c2_e1_l1_test");
        img.sprite = Resources.Load<Sprite>("c3_e3_l3_test");
    }

    void Update()
    {
        Image img = GetComponent<Image>();
        img.sprite = GameManager.manager.getSprite();
    }

    


}
