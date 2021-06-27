using System;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] GameObject exitPanel;
    [SerializeField] GameObject nameInputPanel;
    [SerializeField] GameObject preStartPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] InputField inputField;
    [SerializeField] Text monstersCountText;
    [SerializeField] Text scoreCountText;
    [SerializeField] Canvas canvas;
    [SerializeField] int scoreToMediumDifficulty = 200;
    [SerializeField] int scoreToHardDifficulty = 500;

    int scoreIncrement = 10;
    int monstersNumberForLoose = 10;
    public bool isGameOver;
    



    public event Action changeDifficulty;
    private int _score;
    public  int Score
    {
        get { return _score; }
        set { if(value - _score == scoreIncrement)
            
            {
                _score = value;
                scoreCountText.text = $"Очки: {_score}";
                if (_score == scoreToMediumDifficulty || _score == scoreToHardDifficulty) changeDifficulty?.Invoke();
            }              
            }
    }

    private int _monsters;
    public int Monsters
    {
        get { return _monsters; }
        set
        {
            _monsters = value;
            monstersCountText.text = $"Монстры: {_monsters}";
            if(_monsters >= monstersNumberForLoose)
            {
                isGameOver = true;
                SetRecords();               
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                gameOverPanel.Show();
                Time.timeScale = 0;
            }               
            
        }
    }

    void Start()
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        if (PlayerPrefs.HasKey(Constants.NAME_PLAYER)) ShowPreStartPanel();
        else ShowNameInputPanel();
        _monsters = 0;
        _score = 0;
        isGameOver = false;
    }
    void SetRecords()
    {
        DataHolder._lastScore = _score;
        for (int i = 0; i < DataHolder.records.Length/3 - 1; i++)
        {
            if( string.IsNullOrEmpty(DataHolder.records[i, 2]))
            {
                DataHolder.records[i, 0] = PlayerPrefs.GetString(Constants.NAME_PLAYER);
                DataHolder.records[i, 1] = DateTime.Now.ToString();
                DataHolder.records[i, 2] = _score.ToString();
                return;
            }
            else
            {
                if(i == DataHolder.records.Length/3 - 2)
                {
                    DataHolder.records[i+1, 0] = PlayerPrefs.GetString(Constants.NAME_PLAYER);
                    DataHolder.records[i+1, 1] = DateTime.Now.ToString();
                    DataHolder.records[i+1, 2] = _score.ToString();
                    return;
                }
            }
        }
        
        
    }
    

    #region StartGame

    public void ShowNameInputPanel()
    {
        nameInputPanel.Show();

    }

    public void SaveName()
    {
        nameInputPanel.Hide();
        var name = inputField.text;
        PlayerPrefs.SetString(Constants.NAME_PLAYER, name);
        ShowPreStartPanel();
    }

    public void ShowPreStartPanel()
    {
        preStartPanel.Show();
        var name = PlayerPrefs.GetString(Constants.NAME_PLAYER);
        preStartPanel.transform.GetChild(0).GetComponent<Text>().text = $" Привет {name}, начнем?";

    }

    public event Action start;
    public void StartGame()
    {
        preStartPanel.Hide();
        start?.Invoke();
    }

    #endregion

    #region Pause
    public delegate void Pause(int time);
    public event Pause setPause;
    public void SetLongPause()
    {
        setPause?.Invoke(100);  //любое число больше 4 в GameManager проверяется условие( < 4)
    }

    public void SetShortPause()
    {
        setPause?.Invoke(3);
    }

    #endregion


    #region Exit
    public void ShowExitPanel()
    {
        exitPanel.Show();
        Time.timeScale = 0;
    }
    public void HideExitPanel()
    {
        exitPanel.Hide();
        Time.timeScale = 1;
    }

    public event Action exit;
    public void Exit()
    {
        gameOverPanel.Hide();
        exit?.Invoke();
    }

    #endregion
}
