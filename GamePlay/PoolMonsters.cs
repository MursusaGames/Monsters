using System.Collections.Generic;
using UnityEngine;

public class PoolMonsters : MonoBehaviour
{
    public int monstersCount;
    [SerializeField] List<GameObject> monsters = new List<GameObject>();
    private GameObject[,] poolMonsters;
    [SerializeField] GameObject parent;
    [SerializeField] int maxNumberMonsters = 10;


    private void Awake()
    {
        monstersCount = monsters.Count;
        poolMonsters = new GameObject[monsters.Count, maxNumberMonsters];
    }
    void Start()
    {
        for(int i = 0; i < monsters.Count; i++)
        {
            for(int j = 0; j < maxNumberMonsters; j++)
            {
                var tempGO = Instantiate(monsters[i], parent.transform);
                var randomScale = Random.Range(0.2f, 0.4f);
                tempGO.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                tempGO.Hide();
                poolMonsters[i, j] = tempGO;
            }          
        }
    }

    public GameObject GetFreeMonster(int index)
    {
        for(int i = 0; i < maxNumberMonsters; i++)
        {
            if(poolMonsters[index, i].activeInHierarchy == false)
            {
                return poolMonsters[index, i];
            }
        }
        return null;
    }

   
}
