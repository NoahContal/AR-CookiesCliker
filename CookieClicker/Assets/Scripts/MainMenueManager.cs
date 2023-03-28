using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenueManager : MonoBehaviour
{
    public void Play()
    {
        Debug.Log("miam");
        SceneManager.LoadScene("CookieClicker");
    }
}
