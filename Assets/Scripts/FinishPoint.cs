using System;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private GameObject _rope;
    [SerializeField] private BoxController _boxController;
    [SerializeField] private float _rayLength = 50f;
    
    public Action OnFinishPoint;
    
    void Update()
    {
        var ray = new Ray(transform.position, Vector3.up);
        Debug.DrawRay(transform.position, Vector3.up * _rayLength);
        if (Physics.Raycast(ray, out var hitInfo, _rayLength))
        {
            if (hitInfo.collider.gameObject == _rope)
            {
                OnFinishPoint?.Invoke();
                _boxController.DropDown(transform.position);
            }
        }
    }
}