using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject optionsWindow;

    public void OpenCloseOptionswindow()
    {
        if (optionsWindow.activeSelf == false)
        {
            optionsWindow.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            optionsWindow.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
