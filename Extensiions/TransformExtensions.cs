using UnityEngine;

public static class TransformExtensions 
{
    public static void Show(this GameObject go)
    {
       go.SetActive(true);
    }

    public static void Hide(this GameObject go)
    {
        go.SetActive(false);
    }
}
