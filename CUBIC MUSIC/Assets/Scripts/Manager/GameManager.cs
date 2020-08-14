﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] goGameUI = null;
    [SerializeField] GameObject goTitleUI = null;

    public static GameManager instance;

    public bool isStartGame = false;

    ComboManager theCombo;
    ScoreManager theScore;
    TimingManager theTiming;
    StatusManager theStatus;
    PlayerController thePlayer;
    StageManager theStage;
    NoteManager theNote;
    [SerializeField] Centerflame theMusic = null;  // 비활성화 했기 때문에 FindObjectOfType 으로 찾을 수 없음


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        theCombo = FindObjectOfType<ComboManager>();
        theScore = FindObjectOfType<ScoreManager>();
        theTiming = FindObjectOfType<TimingManager>();
        theStatus = FindObjectOfType<StatusManager>();
        thePlayer = FindObjectOfType<PlayerController>();
        theStage = FindObjectOfType<StageManager>();
        theNote = FindObjectOfType<NoteManager>();
    }

    public void GameStart(int p_songNum, int p_bpm)
    {
        for(int i=0;i<goGameUI.Length;i++)
        {
            goGameUI[i].SetActive(true);
        }
        theMusic.bgmName = "BGM" + p_songNum;
        theNote.bpm = p_bpm;
        theStage.RemoveStage();
        theStage.SettingStage();
        theCombo.ResetCombo();
        theScore.Initialized();
        theTiming.Initialized();
        theStatus.Initialized();
        thePlayer.Initialized();


        AudioManger.instance.StopBGM();

        isStartGame = true;
    }

    public void MainMenu()
    {
        for(int i=0;i<goGameUI.Length;i++)
        {
            goGameUI[i].SetActive(false);
        }

        goTitleUI.SetActive(true);
    }
}
