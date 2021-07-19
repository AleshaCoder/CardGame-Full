using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConclusionDiplomacy : MonoBehaviour
{
    [SerializeField] private Image _mainSolution;
    [SerializeField] private Image _badSolution;
    [SerializeField] private Image _goodSolution;

    [SerializeField] private float _maxLeft;
    [SerializeField] private float _maxRight;
    [SerializeField] private float _speedMovement;

    private bool _isGoodMainSolution;

    private IEnumerator _badMover;
    private IEnumerator _goodMover;

    private void Start()
    {
        _badMover = MoveRight(_badSolution.gameObject, _badMover);
        _goodMover = MoveLeft(_goodSolution.gameObject, _goodMover);
        StartCoroutine(_badMover);
        StartCoroutine(_goodMover);
    }

    private IEnumerator MoveLeft(GameObject gameObject, IEnumerator enumerator)
    {
        float residue = Random.Range(0.1f, 0.5f);
        while (Mathf.Abs(gameObject.transform.position.x-_maxLeft)> residue)
        {
            gameObject.transform.position -= new Vector3(_speedMovement * Time.deltaTime,0,0);
            yield return null;
        }
        enumerator = MoveRight(gameObject, enumerator);
        StartCoroutine(enumerator);
    }

    private IEnumerator MoveRight(GameObject gameObject, IEnumerator enumerator)
    {
        float residue = Random.Range(0.1f, 0.5f);
        while (Mathf.Abs(gameObject.transform.position.x-_maxRight)> residue)
        {
            gameObject.transform.position += new Vector3(_speedMovement * Time.deltaTime,0,0);
            yield return null;
        }
        enumerator = MoveLeft(gameObject, enumerator);
        StartCoroutine(enumerator);
    }



}
