using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LeaderBoard : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] GameObject rowLeaderBoardPrefab;
    [SerializeField] GameObject titleRowLeaderBoardPrefab;
    int rowCount = 10;

    

    private void Start()
    {
        Instantiate(titleRowLeaderBoardPrefab, parent.transform);
        
        SortMassive();        
    }

    
    void SortMassive()
    {
        int x;
        string[,] temp = new string[1,3];
        for (int i = 0; i < rowCount; i++)
        {
            x = i;
            for(int j = i + 1; j < rowCount; j++)
            {
                if(DataHolder.records[x, 2] != null && DataHolder.records[j, 2] != null)
                {
                    if (Int32.Parse(DataHolder.records[x, 2]) <= Int32.Parse(DataHolder.records[j, 2]))
                    {
                        x = j;
                    }
                }
                
                

            }
            temp[0, 0] = DataHolder.records[i, 0];
            temp[0, 1] = DataHolder.records[i, 1];
            temp[0, 2] = DataHolder.records[i, 2];
            DataHolder.records[i, 0] = DataHolder.records[x, 0];
            DataHolder.records[i, 1] = DataHolder.records[x, 1];
            DataHolder.records[i, 2] = DataHolder.records[x, 2];
            DataHolder.records[x, 0] = temp[0, 0];
            DataHolder.records[x, 1] = temp[0, 1];
            DataHolder.records[x, 2] = temp[0, 2];            
                        
        }

        SetBoard();
    }

    void SetBoard()
    {
        for(int j = 0; j < rowCount - 1; j++)
        {
            var go = Instantiate(rowLeaderBoardPrefab, parent.transform);
            go.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = DataHolder.records[j, 0];
            go.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = DataHolder.records[j, 1];
            go.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = DataHolder.records[j, 2];
            PlayerPrefs.SetString($"{j},0", DataHolder.records[j, 0]);
            PlayerPrefs.SetString($"{j},1", DataHolder.records[j, 1]);
            PlayerPrefs.SetString($"{j},2", DataHolder.records[j, 2]);
            Int32.TryParse(DataHolder.records[j, 2], out int result);
            if ( result == 0)
            {
                var tempColor = go.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().color;
                tempColor.a = 0;
                go.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().color = tempColor;
            }
            
            if (DataHolder._sceneNumber == 1 && Int32.Parse(DataHolder._lastScore.ToString()) - Int32.Parse(DataHolder.records[j, 2]) == 0 )
            {
                
                go.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().color = Color.yellow;
                go.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().color = Color.yellow;
                go.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().color = Color.yellow;
            }
        }
       
        
    }   
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Constants.SCENE_0);
    }
   
}
