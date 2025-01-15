using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerCheckHitBox checkHitBox;  
    [SerializeField] Transform[] hand;
    [SerializeField] JoyStick JoyStick;
    [SerializeField] private float distanceMoveY;
    [SerializeField] private float durationMove;
    [SerializeField] private float pushingPower;
    [SerializeField] private float duraionPush;
    [SerializeField] private float durationHitPunch;
    [SerializeField] private float handMovePunch;
    [SerializeField] private float handMovepull;
    private bool attackLeft;

    private bool isMoved;
    private void Awake()
    {
        attackLeft = false;
        rb = GetComponent<Rigidbody2D>();
        checkHitBox=GetComponent<PlayerCheckHitBox>();
    }
    void Start()
    {
        JoyStick.OnStickValueUpdate += Player_OnStickValueUpdate;
    }

    private void Player_OnStickValueUpdate(object sender, JoyStick.OnStickValue e)
    {
        ///Move 
        transform.DOMoveY(transform.position.y+distanceMoveY, durationMove).SetEase(Ease.OutQuad).OnComplete(
            () =>
            {
                checkHitBox.CheckHitBox(new Vector2(0,1));
            });

        //Animation
        if (!attackLeft)
        {
            attackLeft = true;
            hand[0].DOLocalMoveY(handMovePunch, durationHitPunch).SetEase(Ease.OutQuad);
            hand[1].DOLocalMoveY(handMovepull, durationHitPunch).SetEase (Ease.OutQuad);
        }
        else
        {
            attackLeft = false;
            hand[1].DOLocalMoveY(handMovePunch, durationHitPunch).SetEase(Ease.OutQuad);
            hand[0].DOLocalMoveY(handMovepull, durationHitPunch).SetEase(Ease.OutQuad);
        }
        //left right
    }
    public void pushing()
    {
        transform.DOMoveY(transform.position.y - pushingPower, durationMove).SetEase(Ease.OutQuad);   
    }
    public bool IsMoved()
    {
        return isMoved;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
