using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public SpriteRenderer image;
    public bool isOpened = false;

    private Sprite bufferedLogo;
    private Sprite buffered;

    private int number;
    private void Awake()
    {
        image = GetComponent<SpriteRenderer>();
    }
    public void Open()
    {
        image.sprite = buffered;
        isOpened = true;
    }
    public void Close()
    {
        image.sprite = bufferedLogo;
        isOpened = false;
    }
    public void SetLogo(Sprite image)
    {
        this.image.sprite = image;
        bufferedLogo = image;
    }
    public void SetSprite(Sprite image)
    {
        buffered = image;
    }
    public int Number { get { return number; } set { number = value; } }
}
