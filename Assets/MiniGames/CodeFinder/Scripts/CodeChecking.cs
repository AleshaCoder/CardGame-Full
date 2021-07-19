using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodeChecking : MonoBehaviour
{
    [SerializeField] private int _numberOfAttempts = 3;
    [SerializeField] private int _needCodeLength = 4;
    [SerializeField] private int _needCode = 1233;
    [SerializeField] private TextMeshProUGUI _code;
    [SerializeField] private GameObject _codeDisplay;
    [SerializeField] private Material _errorMaterial;
    [SerializeField] private Material _goodMaterial;
    [SerializeField] private Material _defaultMaterial;


    private bool IsFullCode(int needLength)
    {
        Debug.Log("Code length - " + (_code.text.Length+1));
        return _code.text.Length == needLength;
    }

    private void ClearCode()
    {
        _code.text = "";
    }

    private void ChangeDisplayMaterial(Material material)
    {
        _codeDisplay.GetComponent<MeshRenderer>().material = material;
    }

    private void CheckCode()
    {
        if (IsFullCode(_needCodeLength))
        {
            int enteredCode = 11111;
            int.TryParse(_code.text, out enteredCode);
            if (enteredCode == _needCode)
            {
                ChangeDisplayMaterial(_goodMaterial);
                _code.text =  "Верный код";
            }
            else
            {
                ChangeDisplayMaterial(_errorMaterial);
                ClearCode();
            }
        }
        else
        {
            ChangeDisplayMaterial(_defaultMaterial);
        }
    }

    private void OnEnable()
    {
        CodeInput.OnClicked += CheckCode;
    }

    private void OnDisable()
    {
        CodeInput.OnClicked -= CheckCode;
    }

}
