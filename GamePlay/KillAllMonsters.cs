using UnityEngine;

public class KillAllMonsters : MonoBehaviour
{
    [SerializeField] ViewController viewController;
    [SerializeField] GameObject thunderbolt;
    AudioSource soundThunderbolt;

    private void Start()
    {
        soundThunderbolt = GetComponent<AudioSource>();
        thunderbolt.Hide();
    }

    public void KillMonsters()
    {
        ShowThunderbolt();
        soundThunderbolt.Play();
        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy)
            {
                transform.GetChild(i).gameObject.Hide();
                viewController.Monsters--;
                viewController.Score += 10;
            }
            
        }
    }

    void ShowThunderbolt()
    {
        thunderbolt.Show();
        Invoke(nameof(HideThunderbolt), 0.5f);
    }

    void HideThunderbolt()
    {
        thunderbolt.Hide();
    }
}
