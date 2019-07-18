using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadioController : MonoBehaviour, IPointerClickHandler
{
    public Sprite[] VedioSprites;
    // Use this for initialization
    void Start()
    {
        ClickTimes = 0;
        SelfImage = GetComponent<Image>();
        VolunmSlider = GetComponentInChildren<Slider>();
    }
    // Update is called once per frame
    void Update()
    {
        if (VolunmSlider.value > 0)
        {
            ChangeMode(0);
        }
       else
            ChangeMode(1);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SelfImage.sprite = VedioSprites[++ClickTimes % 2];
        VolunmSlider.value = 0;
    }

    private  Image SelfImage;
    private static int ClickTimes;
    private  Slider VolunmSlider;

    private void ChangeMode(int i)
    {
        SelfImage.sprite = VedioSprites[i];
    }

}
