using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionsSaver : MonoBehaviour
{
    [SerializeField]private List<Condition> _conditions = new List<Condition>();
    public static List<float> _conditionsFullness = new List<float>(4);
    public GameObject paneldead;

    private void FixedUpdate()
    {
        for (int i = 0; i < _conditions.Count; i++)
        {
            _conditionsFullness[i] = _conditions[i].GetFullness();
            if (_conditionsFullness[i] < 5)
                paneldead.SetActive(true);
        }
    }

    private void Start()
    {
        if  (SaveLoadController.HasSaving)
        {
            for (int i = 0; i < _conditions.Count; i++)
            {
                _conditions[i].FastChange((int)_conditionsFullness[i]-100);
            }
        }
        else
        {
            for(int i = 0; i < _conditions.Count; i++)
        {
                _conditionsFullness.Add(_conditions[i].GetFullness());
            }
        }
    }
}
