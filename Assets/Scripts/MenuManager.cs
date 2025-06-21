using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioSource som;
    void Start(){
        Cursor.visible = true;
        som.loop = true;
        som.Play();
    }

    public void LoadScenes(string cena){
        SceneManager.LoadScene(cena);
    }

    public void Sair(){
        Application.Quit();
    }
}