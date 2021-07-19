using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CheckPoint
{
    public bool Reached { get; private set; }
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    public void SetReached()
    {
        Reached = true;
    }

    public bool GetReached()
    {
        return Reached;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetDescription()
    {
        return _description;
    }
}

public class Plot : MonoBehaviour
{
    [SerializeField] private GameObject _prefabCheckPoint;
    [SerializeField] private Button _Attention;
    [SerializeField] private int _timeAttention;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private List<CheckPoint> _checkPoints = new List<CheckPoint>();
    private List<GameObject> _checkPointBlocks = new List<GameObject>();
    private static List<bool> _checkPointReached = new List<bool>();
    private IEnumerator _courutine;

    public void SetCheckPointReached(string name)
    {
        foreach (var checkPoint in _checkPoints)
        {
            if (checkPoint.GetName() == name)
            {
                checkPoint.SetReached();
                //_Attention.
                _Attention.gameObject.SetActive(true);
                _Attention.GetComponentInChildren<Text>().text = "Вы прошли чекпоинт " + name;
                _courutine = SlowChangeTransperent(_timeAttention, _Attention, 0);
                StartCoroutine(_courutine);
                Debug.Log("Вы прошли чекпоинт" + name);
            }
        }
    }

    public void RefreshCheckPointsInfo()
    {
        foreach (var checkPointUI in _checkPointBlocks)
        {
             for (int i = 0; i < _checkPoints.Count; i++)
            {
                if (checkPointUI.transform.Find("CheckPointName").GetComponent<Text>().text == _checkPoints[i].GetName())
                {
                    checkPointUI.transform.Find("CheckPointReaching").gameObject.SetActive(_checkPoints[i].GetReached());
                    _checkPointReached[i] = _checkPoints[i].GetReached();
                }
            }
        }
    }

    private void GenerateCheckPointUI()
    {
        for (int i = 0; i < _checkPoints.Count; i++)
        {
            GameObject checkPointUI = Instantiate(_prefabCheckPoint);
            checkPointUI.transform.Find("CheckPointName").GetComponent<Text>().text = _checkPoints[i].GetName();
            checkPointUI.transform.Find("CheckPointDescription").GetComponent<Text>().text = _checkPoints[i].GetDescription();
            if (!SaveLoadController.HasSaving)
                _checkPointReached.Add(false);
            else if (_checkPointReached[i] == true)
                _checkPoints[i].SetReached();
            checkPointUI.transform.Find("CheckPointReaching").gameObject.SetActive(_checkPoints[i].GetReached());
            checkPointUI.transform.SetParent(_scrollRect.content, false);
            _checkPointBlocks.Add(checkPointUI);
        }
    }

    private IEnumerator SlowChangeTransperent(float slowingTime, Button button, float needAlpha)
    {
        var text = button.GetComponentInChildren<Text>();
        var textColor = text.color;
        var colorsButton = button.colors;
        var normalColor = colorsButton.normalColor;
        while (normalColor.a > needAlpha)
        {
            normalColor.a -= Time.deltaTime / slowingTime;
            textColor.a -= Time.deltaTime / slowingTime;
            text.color = textColor;
            colorsButton.normalColor = normalColor;
            button.colors = colorsButton;
            yield return null;
        }
        button.gameObject.SetActive(false);
        normalColor.a = 1;
        textColor.a = 1;
        text.color = textColor;
        colorsButton.normalColor = normalColor;
        button.colors = colorsButton;
    }

    private void Start()
    {
        GenerateCheckPointUI();
    }
}
