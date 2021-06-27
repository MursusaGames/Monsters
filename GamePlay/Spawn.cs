using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] PoolMonsters poolMonsters;
    private GameObject GetRandomMonster(int difficulty)
    {
        var rand = Random.Range(1, poolMonsters.monstersCount +1 - difficulty);
        return poolMonsters.GetFreeMonster(Random.Range(0, rand));
    }

    private Vector2 GetRandomPlace()
    {
        return new Vector2(Random.Range(-2.5f,2.5f), Random.Range(-2.5f,3.5f));
    }

    public void SpawnMonster(int difficulty)
    {
        var monster = GetRandomMonster(difficulty);
        monster.transform.position = GetRandomPlace();
        
        monster.Show();
    }
    
}
