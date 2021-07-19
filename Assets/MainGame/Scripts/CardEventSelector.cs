using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(Collider2D))]
public class CardEventSelector : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private List<Receptor> _receptors = new List<Receptor>();
    [SerializeField] private List<Condition> _conditions = new List<Condition>();
    public Text SolutionText;
    private BoxCollider2D _selfCollider;
    private byte amountOfActiveReceptors = 0;
    private Receptor _activeReceptor = null;
    public Eventor Eventor;
    public Plot Plot;
    public MiniGameLauncher GameLauncher;

    private void ChangeConditions(Receptor activeReceptor)
    {
        int numberOfCondition = 0;
        foreach (var condition in _conditions)
        {          
            condition.ChangeFullness(activeReceptor.GetChange(numberOfCondition));
            numberOfCondition++;
        }
        numberOfCondition = 0;
    }

    private void Start()
    {
        _selfCollider = GetComponent<BoxCollider2D>();
  //      SolutionText = GetComponentInChildren<Text>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        amountOfActiveReceptors--;
        if (amountOfActiveReceptors == 0)
        {
            _activeReceptor = null;
            SolutionText.text = default;
            foreach (var condition in _conditions)
            {
                condition.RemoveSelection();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var receptor in _receptors)
        {
            if (receptor.gameObject == collision.gameObject)
            {
                _activeReceptor = receptor;
                SolutionText.text = Eventor.CurrentEvent.GetSolutionText(receptor.GetPosition());
                if  (Eventor.CurrentEvent.IsChanges)
                    receptor.SetConditionsChanges(Eventor.CurrentEvent.GetConditionChanges(receptor.GetPosition()));
                receptor.SetCheckPointName(Eventor.CurrentEvent.GetPlotChanges(receptor.GetPosition()));
                receptor.SetGameName(Eventor.CurrentEvent.GetMiniGame(receptor.GetPosition()));
                amountOfActiveReceptors++;
                foreach (var condition in _conditions)
                {
                    if (receptor.IsChangeCondition(condition.GetID()))
                        condition.SetSelection();
                    else
                        condition.RemoveSelection();
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("TEST OnPointerUp");
        if (_activeReceptor != null)
        {
            Plot.SetCheckPointReached(_activeReceptor.GetCheckPointName());
            Plot.RefreshCheckPointsInfo();
            GameLauncher.LoadMiniGame(_activeReceptor.GetGameName());
            if (Eventor.CurrentEvent.IsChanges)
                ChangeConditions(_activeReceptor);         
            Eventor.RefreshCurrentEvent();            
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
