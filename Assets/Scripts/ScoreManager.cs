using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;

    private string _startedText;
    private int _scoreBalls = 0;

    private static ScoreManager _instance;

    // Start is called before the first frame update
    void Start()
    {
        if (_instance != null)
        {
            Destroy(_instance);
        }

        _instance = this;
        _startedText = _score.text;

        _score.text = string.Format(_startedText, 0);
    }

    public static void AddScore(int score)
    {
        _instance._scoreBalls += score;
        _instance._score.text = string.Format(_instance._startedText, _instance._scoreBalls);
    }
}
