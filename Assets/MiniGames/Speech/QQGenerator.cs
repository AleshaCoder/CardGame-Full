using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QQGenerator : MonoBehaviour
{
    [SerializeField] private float _timeAnswer = 1.0f;
    [SerializeField] private float _timeResponse = 1.0f;
    [SerializeField] private float _timeAsking = 1.0f;
    [SerializeField] private float _timeSpeech = 30.0f;
    [SerializeField] private List<string> _questions;

    [SerializeField] private Button _questionBox;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Vector3 _questionPosition;
    [SerializeField] private float _randomizingPosition;
    [SerializeField] private MiniGameLauncher GameLauncher;

    private int _answerScore = 0;
    private IEnumerator _questiomBoxCreator;
    private IEnumerator _questiomBoxDecreator;
    private Queue<Button> _activeButtons = new Queue<Button>();

    public void SolveQuestion()
    {
        if (_activeButtons.Count > 0)
            Destroy(_activeButtons.Dequeue().gameObject);
        _answerScore++;
        _scoreText.text = _answerScore.ToString();
    }


    private void Start()
    {
        _questiomBoxCreator = DisplayQuestion();
        StartCoroutine(_questiomBoxCreator);
    }

    private IEnumerator DisplayQuestion()
    {
        while (_timeSpeech > 0)
        {
            _timeSpeech -= _timeAsking;
            var button = Instantiate(_questionBox, _questionPosition+ new Vector3(Random.Range(-_randomizingPosition, _randomizingPosition), Random.Range(-_randomizingPosition, _randomizingPosition), 0), Quaternion.identity, _canvas.transform);
           button.GetComponentInChildren<TextMeshProUGUI>().text = _questions[Random.Range(0, _questions.Count)];
            button.onClick.AddListener(() =>
            {
                SolveQuestion();
            });
            _activeButtons.Enqueue(button);
            yield return new WaitForSeconds(_timeAsking);
            if (_questiomBoxDecreator == null)
            {
                _questiomBoxDecreator = HideQuestion();
                StartCoroutine(_questiomBoxDecreator);
            }
        }
        GameLauncher.LoadMiniGame("MainGameScene");
    }

    private IEnumerator HideQuestion()
    {
        while (_timeSpeech > 0)
        {
            if (_activeButtons.Count > 0)
                Destroy(_activeButtons.Dequeue().gameObject);
            yield return new WaitForSeconds(_timeResponse);
        }
    }
}
