using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] ViewController viewController;
    [SerializeField] Spawn spawn;

    public bool isGamePlayed;

    private int difficulty;
    private const int EASY = 3;
    private const int MEDIUM = 2;
    private const int HARD = 1;
    AudioSource soundClick;

    
    void Start()
    {
        Debug.Log(DataHolder.records.Length);
        viewController.exit += Exit;
        viewController.start += StartGame;
        viewController.setPause += SetPause;
        viewController.changeDifficulty += ChangeDifficulty;
        difficulty = EASY;
        soundClick = GetComponent<AudioSource>();
    }

    private void ChangeDifficulty()
    {
        if (difficulty == EASY)
        {
            difficulty = MEDIUM;
            return;
        }
        else if (difficulty == MEDIUM)
        {
            difficulty = HARD;
            return;
        }
        else return;
    }

    private void Exit()
    {
        Time.timeScale = 1;
        isGamePlayed = false;
        StopAllCoroutines();
        viewController.exit -= Exit;
        viewController.start -= StartGame;
        viewController.setPause -= SetPause;
        viewController.changeDifficulty -= ChangeDifficulty;
        if(viewController.Score >= Int32.Parse(DataHolder.records[8, 2]))
        {
            DataHolder._lastScore = viewController.Score;
            DataHolder._sceneNumber = 1;
            SceneManager.LoadScene(Constants.LEADER_BOARD);
        }
        else
        {
            SceneManager.LoadScene(Constants.SCENE_0);
        }
    }
    private void StartGame()
    {
        isGamePlayed = true;
        StartCoroutine(nameof(SpawnMonster));
    }
    IEnumerator SpawnMonster()
    {
        while (isGamePlayed)
        {
            spawn.SpawnMonster(difficulty);
            viewController.Monsters++;
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f,0.7f)*difficulty);
        }        
    }

    private void SetPause(int time)
    {
        if (isGamePlayed)
        {
            if (time < 4)
            {
                StopCoroutine(nameof(SpawnMonster));
                Invoke(nameof(StartGame), 3f);
            }
            else
            {
                StopCoroutine(nameof(SpawnMonster));
                isGamePlayed = false;
                Time.timeScale = 0;
            }
        }
        else
        {
            Time.timeScale = 1;
            Invoke(nameof(StartGame), 1f);
        }
        
    }
    private void Update()
    {
        if (isGamePlayed && !viewController.isGameOver)
        {
            RaycastHit2D ray = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (ray && ray.collider.gameObject.GetComponent<Monster>() && Input.GetMouseButtonDown(0))
            {
                ray.collider.gameObject.GetComponent<Monster>().TakeDamage();
                soundClick.Play();
            }
        }
       
    }
    }
