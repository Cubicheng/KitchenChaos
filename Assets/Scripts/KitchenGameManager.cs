using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour {

    public event EventHandler OnStateChanged;
    public event EventHandler OnCntChanged;

    public static KitchenGameManager Instance { get; private set; }

    public enum State {
        WaitingToStart,
        CountingDown,
        GamePlaying,
        GameOver
    }

    private const float DELTA_TIME = 1f;
    private const float FULL_TIME = 20f;

    private State state;
    private float waitingToStartTimer = 1f;
    private float countingDownTimer = 0;
    private int cnt = 3;
    private float gamePlayingTimer = FULL_TIME;

    private void Awake() {
        Instance = this;
        SetState(State.WaitingToStart);
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0) {
                    SetState(State.CountingDown);
                }
                break;
            case State.CountingDown:
                countingDownTimer -= Time.deltaTime;
                if (countingDownTimer < 0) {
                    countingDownTimer = DELTA_TIME;
                    cnt--;
                    OnCntChanged?.Invoke(this, new EventArgs());
                    if (cnt < 0) {
                        SetState(State.GamePlaying);
                    }
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0) {
                    SetState(State.GameOver);
                }
                break;
        }
    }

    private void SetState(State state) {
        this.state = state;
        OnStateChanged?.Invoke(this, new EventArgs());
    }

    public State GetState() {
        return state;
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public float GetProgress() {
        return 1 - gamePlayingTimer / FULL_TIME;
    }

}
