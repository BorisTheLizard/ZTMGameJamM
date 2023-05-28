using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private GameObject optionsWindow;
    [SerializeField] playerController cc;

    public void OpenCloseOptionswindow()
    {
        if (optionsWindow.activeSelf == false)
        {
            optionsWindow.SetActive(true);
            cc.enabled = false;
            Time.timeScale = 0;
        }
        else
        {
            optionsWindow.SetActive(false);
            cc.enabled = !false;
            Time.timeScale = 1;
        }
    }
}
