using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Receptor : MonoBehaviour
{
    [SerializeField] private string _position;
    private List<int> _changes = new List<int>() { 50, 50, 50, 50 };
    private string _checkPointName = "";
    private string _miniGameName = "";

    public string GetPosition()
    {
        return _position;
    }

    public string GetCheckPointName()
    {
        return _checkPointName;
    }
    public string GetGameName()
    {
        return _miniGameName;
    }


    public void SetCheckPointName(string checkPointName)
    {
        _checkPointName = checkPointName;
    }

    public void SetGameName(string miniGameName)
    {
        _miniGameName = miniGameName;
    }
    
    public int GetChange(int numberOfChange)
    {
       // Debug.Log("RECEPTOR  " + _changes[numberOfChange]);
        return _changes[numberOfChange];
    }

    public void SetConditionsChanges(IEnumerable<int> changes)
    {
        _changes = (List<int>)changes;
    }

    public bool IsChangeCondition(int id)
    {
        return _changes[id] != 0;
    }
}
