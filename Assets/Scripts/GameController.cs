using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Dialog, Paused }

public class GameController : MonoBehaviour
{
   [SerializeField] PlayerController playerController;
   [SerializeField] Camera worldCamera;

   GameState state;
   GameState stateBeforePause;

   public static GameController Instance { get; private set; }

   private void Awake ()
   {
       Instance = this;
   }

   private void Start()
   {
       //playerController.OnEncountered += StartBattle;
       //battleSystem.OnBattleOver += EndBattle;
    

        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };

        DialogManager.Instance.OnCloseDialog += () =>
        {
            if (state == GameState.Dialog)
            {
                state = GameState.FreeRoam;
            }
        };
   }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            stateBeforePause = state;
            state = GameState.Paused;
        }
        else
        {
            state = stateBeforePause;
        }
    }

    private void Update()
    {
        if (state == GameState. FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
    }
}
