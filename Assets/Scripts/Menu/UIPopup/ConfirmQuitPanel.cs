using UnityEngine;

public class ConfirmQuitPanel : MonoBehaviour
{
    public void AcceptQuit()
    {
        Application.Quit();
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
