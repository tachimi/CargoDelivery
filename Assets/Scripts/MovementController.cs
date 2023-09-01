using System.Collections;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private LineDrawer _lineDrawer;
    [SerializeField] private FinishPoint _finishPoint;
    [SerializeField] private float _speed = 1f;

    private Coroutine _coroutine;

    private void Awake()
    {
        _lineDrawer.OnDrawingFinish += StartMoving;
        _finishPoint.OnFinishPoint += StopMoving;
    }

    private void StartMoving(Vector3[] positions)
    {
        _coroutine = StartCoroutine(MoveToNextPosition(positions));
    }

    private IEnumerator MoveToNextPosition(Vector3[] positions)
    {
        foreach (var position in positions)
        {
            while (transform.position != position)
            {
                transform.position = Vector3.MoveTowards(transform.position, position, _speed * Time.deltaTime);
                yield return null;
            }
        }
    }

    private void StopMoving()
    {
        StopCoroutine(_coroutine);
    }

    private void OnDestroy()
    {
        _lineDrawer.OnDrawingFinish -= StartMoving;
        _finishPoint.OnFinishPoint -= StopMoving;
    }
}