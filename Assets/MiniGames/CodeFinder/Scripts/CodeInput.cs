using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

[System.Serializable]
public class CodeButton
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private byte _numberOfButton;

    public int GetNumber()
    {
        return _numberOfButton;
    }

    public GameObject GetGameObject()
    {
        return _gameObject;
    }
}

public class CodeInput : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public delegate void ClickAction();
    public static event ClickAction OnClicked;

    [SerializeField] private List<CodeButton> _buttons = new List<CodeButton>();
    [SerializeField] private TextMeshProUGUI _code;
    [SerializeField] private Camera _mainCamera;
    private CodeButton _clickedCodeButton;


    private CodeButton FindClickedCodeButton(RaycastHit hit)
    {
        foreach (var button in _buttons)
        {
            if (hit.collider.gameObject == button.GetGameObject())
            {
                return button;
            }
        }
        return null;
    }

    private void CheckCodeButtonClick()
    {
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
        _clickedCodeButton = FindClickedCodeButton(hit);
    }

    private void RefreshCodeText()
    {
        if (_clickedCodeButton != null)
        {
            _code.text += _clickedCodeButton.GetNumber();
            _clickedCodeButton = null;
        }
    }

    private void OnEnable()
    {
        OnClicked += CheckCodeButtonClick;
        OnClicked += RefreshCodeText;
    }

    private void OnDisable()
    {
        OnClicked -= CheckCodeButtonClick;
        OnClicked -= RefreshCodeText;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClicked();
        Debug.Log("Mouse Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
