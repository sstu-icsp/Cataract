using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class RhythmGView : Element
{
    public Image background;
    public GameObject particles;

    private Color32 highlightedColor = new Color32(0xFF, 0xED, 0x00, 0x7F);
    private Color32 initialColor = new Color32(0x00, 0x00, 0x00, 0x7F);
    private Color32 inputColor = new Color32(0x00, 0xFF, 0x21, 0x7F);

    public void AnimateBeat()
    {
        background.color = highlightedColor;
        background.DOColor(initialColor, 0.2f).SetUpdate(true);
    }

    public void AnimateBeatInput(Vector2 pointerPosition)
    {
        background.color = inputColor;
        background.DOColor(initialColor, 0.2f).SetUpdate(true);
    }
}
