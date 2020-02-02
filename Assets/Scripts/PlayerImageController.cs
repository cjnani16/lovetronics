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
        img.sprite = (GameManager.manager.getSprite() != null) ? GameManager.manager.getSprite() : defaultSprite;
        GetComponent<RectTransform>().sizeDelta = Vector2.Scale(GetComponent<Image>().sprite.rect.size, new Vector2(0.45f, 0.45f));
    }




}
