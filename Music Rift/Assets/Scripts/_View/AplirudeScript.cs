using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AplirudeScript : Element {
    private Vector3 endPos;
    private Vector3 startPos;
    List<GameObject> lines = new List<GameObject>();
    public float w = 50f; //width of wave
    public float l = 0.1f; //lenght of wave
    public float h = 0.5f; //height of wave

    public float H
    {
        get
        {
            return h;
        }

        set
        {
            if (value <= 100)
                h = value / 200.0f;
            else h = 0.5f;
        }
    }

    void Start()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(gameObject.transform.position);
        startPos = new Vector3(pos.x - gameObject.transform.lossyScale.x, 
            pos.y - gameObject.transform.lossyScale.y * 0.3f,pos.z);
        endPos = new Vector3(startPos.x + 0.001f, startPos.y + 0.001f, startPos.z -1);       
    }

    void Update()
    {
        Draw();
        UpdateLines();
    }

    private void UpdateLines()
    {
        LineRenderer lr;
        Vector3 position;
        float bounce = gameObject.transform.lossyScale.x;
        for (int i = 0; i < lines.Count; i++)
        {
            GameObject item = lines[i];
            lr = item.GetComponent<LineRenderer>();
            position = lr.GetPosition(0);
            lr.SetPosition(0, new Vector3(position.x - 0.05f, position.y, position.z));
            position = lr.GetPosition(1);
            lr.SetPosition(1, new Vector3(position.x - 0.05f, position.y, position.z));


            Debug.Log(position.x + "/" + (Camera.main.ScreenToWorldPoint(gameObject.transform.position).x - bounce));
            if (position.x < Camera.main.ScreenToWorldPoint(gameObject.transform.position).x - bounce
                || position.x > Camera.main.ScreenToWorldPoint(gameObject.transform.position).x + bounce)
            {
                lines.Remove(lines[i]);
                Destroy(item);
            }
        }
        //lines.RemoveAll(Predicate<GameObject> )
    }

    void Draw()
    {
        Debug.Log(startPos + "/" + endPos);
        GameObject myLine = new GameObject();
        myLine.transform.position = startPos;
        myLine.AddComponent<LineRenderer>();
        myLine.GetComponent<LineRenderer>().sortingOrder = 5;
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.startColor = Color.green;
        lr.endColor = Color.green;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, new Vector3(startPos.x, startPos.y, -5));
        lr.SetPosition(1, new Vector3(endPos.x, endPos.y, -5));
        endPos = startPos;
        startPos = new Vector3(startPos.x + l, (Mathf.Sin(startPos.x * w) ) * H + startPos.y, -5);
        lines.Add(myLine);
    }

}
