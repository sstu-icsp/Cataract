using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GinButton3Script : GunButton {
    override protected void ChoiceGun()
    {
       // GameManage.instance.CurrentGun = GameManage.instance.guns[1];
        base.ChoiceGun();
    }
}
