using System;
using UnityEngine;

public class SavePlayerPrefs : MonoBehaviour
{
    
    void Start()
    {
        SetDataFromPlayerPrefs();
    }
    void SetDataFromPlayerPrefs()
    {

        for (int i = 0; i < 9; i++)
        {
            if (PlayerPrefs.HasKey($"{i},2"))
            {
                DataHolder.records[i, 0] = PlayerPrefs.GetString($"{i},0");
                DataHolder.records[i, 1] = PlayerPrefs.GetString($"{i},1");
                
                Int32.TryParse(PlayerPrefs.GetString($"{i},2"), out int result);
                DataHolder.records[i, 2] = result.ToString();
                
            }
            

        }
    }
    
}
