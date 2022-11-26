using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void Return()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenShopMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenGardenMenu()
    {
        SceneManager.LoadScene(2);
    }
}