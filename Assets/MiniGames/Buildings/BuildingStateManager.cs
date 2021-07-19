using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingStateManager : MonoBehaviour
{
    private int _numOfRecoveries = 0;
    private bool isRecovered = false;
    private bool _isTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == gameObject.tag && _numOfRecoveries < 1 && BuildingMaterialGrid.CountOfMaterials != 0)
        {
            Debug.Log("Is trigger");
            _isTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButton(0)&&_isTrigger && !isRecovered)
        {
            _isTrigger = false;
            transform.localScale *= 1.5f;
            _numOfRecoveries++;
            Destroy(other.gameObject);
            isRecovered = true;
            BuildingMaterialGrid.CountOfMaterials--;
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        if (other.gameObject.tag == gameObject.tag && !isRecovered)
        {
            Debug.Log("End trigger");
            _isTrigger = false;
        }
    }
}
