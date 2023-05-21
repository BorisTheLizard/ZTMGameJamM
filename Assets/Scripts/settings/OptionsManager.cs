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
        }
        else
        {
            optionsWindow.SetActive(false);
        }
    }
}
