using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour
{
    public bool isTouch = false;
    public Vector3 touchPosition;

    void Update() {
        Debug.Log(GameManager.instance.IsPaused);
    if (Input.GetMouseButtonDown(0) && !GameManager.instance.IsPaused)//Отслеживание нажатия на экран 
        {
            
            isTouch = true;
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//Запись в переменную pos координат места, где произошло касание экрана.
            GameManager.instance.drawLazer(touchPosition);
        }
        if (Input.GetMouseButtonUp(0))//Отслеживание нажатия на экран 
        {
            isTouch = false;
        }
    }
}
