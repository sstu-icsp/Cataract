using System;
using UnityEngine;

public class ListController : Element
{
    public ListView[] lists;
    public void Awake()
    {
        ListView.OnListCollected += SaveListStatus;
        InitLists();
    }

    private void InitLists()
    {
        foreach(ListView list in lists)
        {
            if (!PlayerPrefs.HasKey("list" + list.id))
            {
                PlayerPrefs.SetInt("list" + list.id, 0);
            }
            if (PlayerPrefs.GetInt("list" + list.id) == 1)
            {
                list.gameObject.SetActive(false);
                list.ActivateInDiary();
            }
        }
    }

    private void SaveListStatus(int id)
    {
        PlayerPrefs.SetInt("list" + id, 1);
        PlayerPrefs.Save();
    }
    
}