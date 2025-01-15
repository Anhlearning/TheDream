using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
public class JoyStick : MonoBehaviour, IPointerDownHandler,IPointerUpHandler    
{
    [SerializeField] private float scaleAmount = 1.1f;
    [SerializeField] private float duration = 0.05f;
    [SerializeField] private float valueUpdatePlayer = 1f;
    public event EventHandler<OnStickValue> OnStickValueUpdate;

    public class OnStickValue : EventArgs
    {
        public float valueUpdate;
    } 

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one * scaleAmount, duration).SetEase(Ease.Linear);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, duration).SetEase(Ease.Linear);
        OnStickValueUpdate?.Invoke(this, new OnStickValue
        {
            valueUpdate = valueUpdatePlayer
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
