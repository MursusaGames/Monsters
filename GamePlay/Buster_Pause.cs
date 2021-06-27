using UnityEngine;
using UnityEngine.UI;

public class Buster_Pause : MonoBehaviour
{
    Button button;
    Image image;
    bool isAmount = false;
    [SerializeField] float busterDelay = 25;
    [SerializeField] ViewController viewController;
    float tempTime;
    void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        viewController.start += StartAmount;
    }
    private void OnDisable()
    {
        viewController.start -= StartAmount;
    }

    public void StartAmount()
    {
        button.interactable = false;
        image.fillAmount = 0;
        isAmount = true;
        tempTime = Time.time + busterDelay;
    }

    
    void Update()
    {
        if (isAmount)
        {
            
            if(tempTime > Time.time)
            {
                image.fillAmount =  (tempTime - Time.time)/busterDelay;
            }
            if(tempTime <= Time.time)
            {
                button.interactable = true;
                isAmount = false;
            }
        }
               
    }
}
