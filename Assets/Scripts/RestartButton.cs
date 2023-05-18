using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    void Start()
    {
        _button.onClick.AddListener(OnClickHandle);
    }

    private void OnClickHandle()
    {
        _button.onClick.RemoveAllListeners();
        SceneManager.LoadScene(0);
    }
}
