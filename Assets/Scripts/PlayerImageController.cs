using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerImageController : MonoBehaviour
{

    public Sprite sprite;
    void Start()
    {
        updateImage();
    }

    void Update()
    {
        updateImage();
    }

    private void updateImage()
    {
        Image img = GetComponent<Image>();
        Sprite defaultSprite = img.sprite;
        img.sprite = (GameManager.manager.getSprite()!=null)?GameManager.manager.getSprite():defaultSprite;
        // transform.scale.x = 0.9;
        // transform.scale.y = 0.9;
        GetComponent<RectTransform>().sizeDelta = Vector2.Scale(GetComponent<Image>().sprite.rect.size, new Vector2(0.85f, 0.85f));
    }




}
