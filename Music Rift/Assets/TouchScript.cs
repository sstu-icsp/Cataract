using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour
{
    public bool isTouch = false;
    public Vector3 touchPosition;

    void Update() { 

	if (Input.GetMouseButton(0))//Отслеживание нажатия на экран 
        {
            isTouch = true;
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//Запись в переменную pos координат места, где произошло касание экрана.
            GameManage.instance.drawLazer(touchPosition);
            GameManage.instance.DrawLine(touchPosition);
        }
        if (Input.GetMouseButtonUp(0))//Отслеживание нажатия на экран 
        {
            isTouch = false;
        }
    }
}
