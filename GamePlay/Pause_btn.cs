using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_btn : MonoBehaviour
{
   
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = sprites[0];
    }

    public void ChangeSprite()
    {
        if(image.sprite == sprites[0])
        {
            image.sprite = sprites[1];
        }
        else
        {
            image.sprite = sprites[0];
        }
    }

    
    
}
