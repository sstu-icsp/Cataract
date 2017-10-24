using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GinButton1Script : GunButton {

    override protected void ChoiceGun()
    {
        // GameManage.instance.CurrentGun = GameManage.instance.guns[1];
        base.ChoiceGun();
    }
}
