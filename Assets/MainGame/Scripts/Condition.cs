using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Condition : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _id;
    private float _fullness = 75;
    [SerializeField] private Color _selectedColor;
    private Color _standartColor;
    [SerializeField] private Image _image;
    private RectTransform _childrenRectTransform => _image.GetComponent<RectTransform>();
    private IEnumerator _courutine;
    private bool Updated = false;
    public string GetName()
    {
        return _name;
    }

    public int GetID()
    {
        return _id;
    }

    public  float GetFullness()
    {
        return _fullness;
    }

    public void SetSelection()
    {
        ChangeColor(_selectedColor);
    }

    public void RemoveSelection()
    {
        ChangeColor(_standartColor);
    }

    public void FastChange(int change)
    {
        var needFullness = _fullness + change;
        var needFullnessVector = new Vector3(needFullness / 100.0f, needFullness / 100.0f, needFullness / 100.0f);
        _childrenRectTransform.localScale = needFullnessVector;
    }

    public void ChangeFullness(int change)
    {
        var needFullness = _fullness + change;
        if (needFullness > 100) needFullness = 100;
        if (needFullness < 0) needFullness = 0;
        var baseFullnessVector = _childrenRectTransform.localScale;
        var needFullnessVector = new Vector3(needFullness / 100.0f, needFullness / 100.0f, needFullness / 100.0f);
        _fullness = needFullness;

        if (_courutine != null) StopCoroutine(_courutine);
        _courutine = SlowChangeFullness(1, baseFullnessVector, needFullnessVector);
        StartCoroutine(_courutine);
    }

    private IEnumerator SlowChangeFullness(float slowingTime, Vector3 baseFullnessVector, Vector3 needFullnessVector)
    {
        float timer = 0;

        while (timer < slowingTime)
        {
            _childrenRectTransform.localScale = Vector3.Lerp(baseFullnessVector, needFullnessVector, timer / slowingTime);
            yield return null;
            timer += Time.deltaTime;
        }
        _childrenRectTransform.localScale = needFullnessVector;
    }

    private void ChangeColor(Color color)
    {
        _image.color = color;
    }

    private void Start()
    {
        _standartColor = _image.color;
        if (SaveLoadController.HasSaving)
        {
        }
    }
}
