using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenueManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("CookieClicker");
    }
}
