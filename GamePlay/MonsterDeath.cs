using UnityEngine;

public class MonsterDeath : MonoBehaviour
{
    [SerializeField] Monster monster;

    public void Death()
    {
        monster.Death();
    }
}
