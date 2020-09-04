using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPointer : MonoBehaviour
{
    [SerializeField]
    private Vector3 _targetPosition;
    [SerializeField]
    private RectTransform _pointerRectTransform;

    private void Awake()
    {
        _targetPosition = new Vector3(200, 45);
        //_pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPosition = _targetPosition;
        var fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        var dir = (toPosition - fromPosition).normalized;
        var angle = Vector3.AngleBetween(Vector3.forward, dir);
    }
}
