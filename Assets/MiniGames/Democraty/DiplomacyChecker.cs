using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DiplomacyChecker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _badScoreText, _goodScoreText;
    private int _badScore = 0, _goodScore = 0;

    private enum Target
    {
        None,
        Bad,
        Good
    };

    private Target _mainTarget;
    private Queue<Target> _diplomacies = new Queue<Target>();

    public void TrySetDiplomacy()
    {
        Debug.Log("Ok/Try");
        foreach (var diplomacy in _diplomacies)
        {
            if (_mainTarget == diplomacy)
            {
                if (_mainTarget == Target.Bad)
                {
                    _badScore++;
                    _badScoreText.text = _badScore.ToString();
                }
                else if (_mainTarget == Target.Good)
                {
                    _goodScore++;
                    _goodScoreText.text = _goodScore.ToString();
                }                
                break;
            }
        }
        ChangeMainSolution();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            TrySetDiplomacy();
        }
    }

    private void ChangeMainSolution()
    {
        _mainTarget = (Target)Random.Range(1, 3);
        if (_mainTarget == Target.Bad)
            GetComponent<Image>().color = Color.red;
        else if (_mainTarget == Target.Good)
            GetComponent<Image>().color = Color.green;
    }

    private void Start()
    {
        ChangeMainSolution();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ok/Trig");
        if (collision.gameObject.tag == "Bad")
            _diplomacies.Enqueue(Target.Bad);

        if (collision.gameObject.tag == "Good")
            _diplomacies.Enqueue(Target.Good);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _diplomacies.Dequeue();
    }
}
