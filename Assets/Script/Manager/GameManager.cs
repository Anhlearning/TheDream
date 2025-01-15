using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    WattingToStart,
    CountDownToStart,
    GamePlaying,
    EndGame
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private float countDownTime;
    [SerializeField] private float distanceAcceptable;
    [SerializeField] private Transform pointEndPlayer1;
    [SerializeField] private Transform pointEndPlayer2;

    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    private int countPointPlayer1;
    private int countPointPlayer2;

    private float timeCount = 0f;
    private bool player1Win;
    private bool player2Win;    
    public event EventHandler OnStateChanged;
    private State state;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        state = State.WattingToStart;
        countPointPlayer1 = 0;
        countPointPlayer2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.WattingToStart:
                state = State.CountDownToStart;
                break;
            case State.CountDownToStart:
                timeCount += Time.deltaTime;
                if (timeCount >= countDownTime)
                {
                    timeCount = 0f;
                    state = State.GamePlaying;
                }
                break;
            case State.GamePlaying:
                if(countPointPlayer1 >= 3)
                {
                    state = State.EndGame;
                    player1Win = true;
                    break;
                }
                else if(countPointPlayer2 >= 3)
                {
                    state= State.EndGame;
                    player2Win = true;
                    break;
                }
                if(Vector2.Distance(player1.position, pointEndPlayer1.position) < distanceAcceptable)
                {
                    state = State.CountDownToStart;
                   countPointPlayer2++;
                    
                }
                else if(Vector2.Distance(player2.position, pointEndPlayer2.position) < distanceAcceptable)
                {
                    state = State.CountDownToStart;
                    countPointPlayer1++;
                }
                break;
            case State.EndGame:
                if (player1Win)
                {
                    Debug.Log("Player 1 Win");
                }
                else
                {
                    Debug.Log("Player 2 Win");
                }
                break;


        }
    }
}
