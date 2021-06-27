using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Info : MonoBehaviour
{
    private const string DEVELOPER_URL = "https://play.google.com/store/apps/dev?id=5432149960976309187";
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Scene_0");
    }

    public void LoadInstagramm()
    {
        Application.OpenURL(DEVELOPER_URL);
    }
}
