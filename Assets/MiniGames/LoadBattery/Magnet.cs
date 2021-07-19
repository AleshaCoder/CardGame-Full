using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 4f;
    [SerializeField] private float _speedOfAttracting;
    [SerializeField] private List<GameObject> metals;
    private bool SpacePressed = false;
    private bool IsMoving = false;
    private bool _isAttracting = true;
    private Vector3 _currentPosition;
    private IEnumerator enumerator;
    public GameObject CurrentMetal { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Is Trigger");
        if (SpacePressed)
        {
            if (CurrentMetal == null)
            {
                CurrentMetal = GetNeededMetal(other);
                Attract(CurrentMetal);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (SpacePressed)
        {
            if (CheckMoving())
            {
                if ((enumerator != null) && (_isAttracting))
                {
                    StopCoroutine(enumerator);
                    FastChangePosition(transform.position - new Vector3(0, 0.5f, 0));
                }
            }
        }
    }

    private void Update()
    {
        Move();
    }

    private bool CheckMoving()
    {
        return _currentPosition != transform.position;
    }

    private void Attract(GameObject CurrentMetal)
    {
        _currentPosition = transform.position;
        if (CurrentMetal != null)
            enumerator = SlowChangePosition(_speedOfAttracting, CurrentMetal.transform.position, _currentPosition - new Vector3(0, 0.5f, 0));
        StartCoroutine(enumerator);
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        SpacePressed = true;
        if (_isAttracting)
            transform.position += new Vector3(horizontal, 0, vertical) * MoveSpeed * Time.deltaTime;
    }

    private GameObject GetNeededMetal(Collider other)
    {
        GameObject NeededObj = null;
        foreach (var metal in metals)
        {
            if (other.gameObject == metal)
            {
                NeededObj = metal;
                return NeededObj;
            }
        }
        return NeededObj;
    }

    public void RemoveMetallFromList(Collider other)
    {
        foreach (var metal in metals)
        {
            if (other.gameObject == metal)
            {
                metals.Remove(metal);
                break;
            }
        }
    }

    private void FastChangePosition(Vector3 needVector)
    {
        if (CurrentMetal != null)
            CurrentMetal.transform.position = needVector;
    }

    private IEnumerator SlowChangePosition(float slowingTime, Vector3 baseVector, Vector3 needVector)
    {
        float timer = 0;
        var heading = CurrentMetal.transform.position - needVector;
        _isAttracting = false;
        while (timer < slowingTime)
        {
            if (CurrentMetal == null)
                break;
            CurrentMetal.transform.position = Vector3.Lerp(baseVector, needVector, timer / slowingTime);
            yield return null;
            timer += Time.deltaTime;
        }
        CurrentMetal.transform.position = needVector;
        _isAttracting = true;
        Debug.Log("Position changed");
    }
}

