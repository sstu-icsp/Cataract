using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIView : Element
{
    public Text playerHP;
    public GameObject gameInterface, fightInterface;
    public Image fightPanelBackground;
    public Color32 transparent = new Color32(0x00, 0x00, 0x00, 0x00);

    public void AnimateFightStart()
    {
        gameInterface.SetActive(!gameInterface.activeSelf);
        fightInterface.SetActive(!fightInterface.activeSelf);
        Sequence fightAnim = DOTween.Sequence().SetUpdate(true);
        fightAnim.Append(DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0.1f, 1)
            .OnComplete(() =>
            {
                app.controller.fight.StartFight();
            }));
        fightAnim.Join(DOTween.To(() => Time.fixedDeltaTime, x => Time.fixedDeltaTime = x, 0.002f, 1));
        fightAnim.Join(fightPanelBackground.DOColor(transparent, 1).From());
    }

    public void AnimateFightEnd()
    {
        Sequence fightAnim = DOTween.Sequence().SetUpdate(true);
        fightAnim.Append(DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, 1)
            .OnComplete(() =>
            {
                app.controller.fight.EndFight();
                gameInterface.SetActive(!gameInterface.activeSelf);
                fightInterface.SetActive(!fightInterface.activeSelf);
            }));
        fightAnim.Join(DOTween.To(() => Time.fixedDeltaTime, x => Time.fixedDeltaTime = x, 0.02f, 1));
        fightAnim.Join(fightPanelBackground.DOColor(transparent, 1));
    }

    public void setHealth(int h)
    {
        playerHP.text = h.ToString();
    }

}