using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject objeto;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider slider;

    void Start(){
        Cursor.visible = true;
        Volume();
    }
    public void LoadScenes(string cena){
        SceneManager.LoadScene(cena);
    }
    public void Sair(){
        Application.Quit();
    }

    public void AtivarDesativarObjeto()
    {
        if (objeto.activeInHierarchy)
        {
            objeto.SetActive(false);
        }
        else
        {
            objeto.SetActive(true);
        }
    }
    private void AtivarObjeto()
    {
        objeto.SetActive(true);
    }
    private void DesativarObjeto()
    {
        objeto.SetActive(false);
    }

    public void Volume()
    {
        float volume = slider.value;
        mixer.SetFloat("Music", Mathf.Log10(volume)*20);
    }

}