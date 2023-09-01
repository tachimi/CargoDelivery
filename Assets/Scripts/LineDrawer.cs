using System;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private float _minDrawDistance = 0.5f;
    [SerializeField] private float _maxDrawDistance = 1f;

    public Action<Vector3[]> OnDrawingFinish;

    private LineRenderer _lineRenderer;

    private bool _isDrawing;
    private bool _drawingEnd;

    private Vector3 _lastPoint;


    void Awake()
    {
        _lineRenderer = gameObject.GetComponent<LineRenderer>();
        _lastPoint = _lineRenderer.GetPosition(_lineRenderer.positionCount - 1);
    }

    private void Update()
    {
        if (_drawingEnd)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _isDrawing = true;
        }

        if (_isDrawing)
        {
            StartDrawing();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDrawing = false;

            var positions = new Vector3[_lineRenderer.positionCount];

            for (var i = 0; i < _lineRenderer.positionCount; i++)
            {
                positions[i] = _lineRenderer.GetPosition(i);
            }

            OnDrawingFinish?.Invoke(positions);
            _drawingEnd = true;
        }
    }

    private void StartDrawing()
    {
        var mousePos = Input.mousePosition;
        var newMousePos =
            Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.transform.position.z));
        newMousePos.z = -0.2f;

        var distance = Vector3.Distance(newMousePos, _lastPoint);
        if (distance < _minDrawDistance)
        {
            return;
        }

        if (distance < _maxDrawDistance)
        {
            DrawLine(newMousePos);
        }
    }

    private void DrawLine(Vector3 point)
    {
        var index = ++_lineRenderer.positionCount - 1;
        _lineRenderer.SetPosition(index, point);
        _lastPoint = point;
    }
}