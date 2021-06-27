using UnityEngine;


public class Monster : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] protected int health = 10;

    ViewController viewController;

    private void Start()
    {
        viewController = FindObjectOfType<ViewController>();
    }

    public void TakeDamage()
    {
        viewController.Score += 10;
        this.health -= 10;
        if (this.health <= 0)
        {
            if (anim) anim.SetTrigger(Constants.ANIM_DAMAGE);
            else Death();

        }
    }
    public void Death()
    {
        viewController.Monsters--;
        this.gameObject.Hide();
    }



}
