using UnityEngine;

public class TableCanvas : MonoBehaviour
{
    public static Canvas tableCanvas;

    private void Awake()
    {
        tableCanvas = GetComponent<Canvas>();
    }
}