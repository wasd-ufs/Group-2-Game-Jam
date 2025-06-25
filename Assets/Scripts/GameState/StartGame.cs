using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void LateUpdate()
    {
        GameManager.GoToProgrammingState();
        Destroy(this);
    }
}