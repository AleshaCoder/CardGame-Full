using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Solution
{
    [SerializeField] private string _solutionText;
    [SerializeField] private List<int> _conditionChanges = new List<int>(4);
    [SerializeField] private bool _hasCheckPoint;
    [SerializeField] private bool _hasMiniGame;
    [SerializeField] private string _CheckPointName = "";
    [SerializeField] private string _miniGame = "";

    public bool TryGetCheckPoint(ref string checkPointName)
    {
        checkPointName = _CheckPointName;
        return _hasCheckPoint;
    }

    public bool TryGetMiniGame(ref string miniGame)
    {
        miniGame = _miniGame;
        return _hasMiniGame;
    }

    public string GetText()
    {
        return _solutionText;
    }

    public List<int> GetChanges()
    {
        return _conditionChanges;
    }
}

[System.Serializable]
public class Event
{    
    //[SerializeField] private Solution _solutionUp;
    //[SerializeField] private Solution _solutionDown;
    [SerializeField] private Solution _solutionLeft;
    [SerializeField] private Solution _solutionRight;    
    [SerializeField] private string _eventText;
    //[SerializeField] private GameObject _person;

    public enum Person
    {
        Helper,
        MinEconomy,
        MinHelthy,
        BubkaGog,
        SpanchBoom
    }

    [SerializeField]  private Person _person;

    public bool IsChanges = false;

    public string GetMainText()
    {
        return _eventText;
    }

    public Person GetPerson()
    {
        return _person;
    }

    public string GetSolutionText(string positionReceptor)
    {
        switch (positionReceptor)
        {
            case "Up":
                //return _solutionUp.GetText();
            case "Down":
               // return _solutionDown.GetText();
            case "Left":
                return _solutionLeft.GetText();
            case "Right":
                return _solutionRight.GetText();
            default:
                return null;
        }
    }

    public string GetMiniGame(string positionReceptor)
    {
        string miniGame = "";
        switch (positionReceptor)
        {
            case "Up":
                //_solutionUp.TryGetMiniGame(ref miniGame);            
                return miniGame;
            case "Down":
                // _solutionDown.TryGetMiniGame(ref miniGame);
                return miniGame;
            case "Left":
                _solutionLeft.TryGetMiniGame(ref miniGame);
                return miniGame;
            case "Right":
                _solutionRight.TryGetMiniGame(ref miniGame);
                return miniGame;
            default:
                return miniGame;
        }
    }

    public string GetPlotChanges(string positionReceptor)
    {
        string checkPointName = "";
        switch(positionReceptor)
        {
            case "Up":
                //_solutionUp.TryGetCheckPoint(ref checkPointName);            
                return checkPointName;
            case "Down":
               // _solutionDown.TryGetCheckPoint(ref checkPointName);
                return checkPointName;
            case "Left":
                _solutionLeft.TryGetCheckPoint(ref checkPointName);
                return checkPointName;
            case "Right":
                _solutionRight.TryGetCheckPoint(ref checkPointName);
                return checkPointName;
            default:
                return checkPointName;
        }
    }

    public IEnumerable<int> GetConditionChanges(string positionReceptor)
    {
        switch (positionReceptor)
        {
            case "Up":
               // return _solutionUp.GetChanges();
            case "Down":
               // return _solutionDown.GetChanges();
            case "Left":
                return _solutionLeft.GetChanges();
            case "Right":
                return _solutionRight.GetChanges();
            default:
                return null;
        }
    }
}

[System.Serializable]
public class EventPart
{
    public bool IsLinearPlot = false;
    [SerializeField] private List<Event> _events = new List<Event>();
    private bool _opened = false;

    public void Open()
    {
        _opened = true;
    }

    public void Close()
    {
        _opened = false;
    }

    public bool IsOpened()
    {
        return _opened;
    }

    public List<Event> GetEvents()
    {
        return _events;
    }
}

public class Eventor : MonoBehaviour
{
    static public Event CurrentEvent;
    static public GameObject CurrentPerson;
    static public EventPart CurrentPart;
    public Text EventText;
    [SerializeField] private GameObject _helper, _boom, _gog, _miHelthy, _miEconomy;
    [SerializeField] private List<EventPart> _parts = new List<EventPart>();
    private int _eventID = -1;
    private static int _partID = -1;
    private List<Event> _availableEvents = new List<Event>();
    private Random _random = new Random();

    public void RefreshCurrentEvent()
    {
        Debug.Log("Refreshing Event...");
        if (CurrentPerson!= null)
            CurrentPerson.SetActive(false);
        if (_eventID != -1)
        {
            _availableEvents.RemoveAt(_eventID);
        }
        if (CheckAvailableEvents())
        {     
            if (CurrentPart.IsLinearPlot)
            {
                _eventID = 0;
                CurrentEvent = _availableEvents[_eventID];
            }
            else
            {
                _eventID = Random.Range(0, _availableEvents.Count);
                CurrentEvent = _availableEvents[_eventID];
            }
            switch (CurrentEvent.GetPerson())
            {
                case Event.Person.Helper:
                    CurrentPerson = _helper;
                    break;
                case Event.Person.BubkaGog:
                    CurrentPerson = _gog;
                    break;
                case Event.Person.MinEconomy:
                    CurrentPerson = _miEconomy;
                    break;
                case Event.Person.MinHelthy:
                    CurrentPerson = _miHelthy;
                    break;
                case Event.Person.SpanchBoom:
                    CurrentPerson = _boom;
                    break;
            }
            if (CurrentPerson != null)
                CurrentPerson.SetActive(true);
            EventText.text = CurrentEvent.GetMainText();
        }
        else
        {
            ChangePart();
        }        
        Debug.Log("OK");
    }

    public void ChangePart()
    {
        Debug.Log("Changing Part...");
        if (CurrentPart != null)
            CurrentPart.Close();
        _partID++;
        _parts[_partID].Open();
        _availableEvents.Clear();
        CurrentPart = _parts[_partID];
        _availableEvents.AddRange(CurrentPart.GetEvents());
        _eventID = -1;
        RefreshCurrentEvent();
        Debug.Log("OK");
    }

    private bool CheckAvailableEvents()
    {
        return _availableEvents.Count > 0;
    }

    private void Start()
    {
        //DontDestroyOnLoad(this);
        if (!SaveLoadController.HasSaving)
            ChangePart();
        else
        {
            Debug.Log(CurrentEvent.GetMainText());
            _availableEvents.AddRange(CurrentPart.GetEvents());
            RefreshCurrentEvent();
        }
    }
}
