﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Song
{
    public string name;
    public string composer;
    public int bpm;
    public Sprite sprite;
}

public class StageMenu : MonoBehaviour
{
    [SerializeField] Song[] songList = null;

    [SerializeField] Text txtSongName = null;
    [SerializeField] Text txtSongComposer = null;
    [SerializeField] Text txtSongScore = null;
    [SerializeField] Image imgDisk = null;

    [SerializeField] GameObject TitleMenu = null;

    DataManager theData;

    int currentSong = 0;

    void OnEnable()
    {
        if(theData == null)
            theData = FindObjectOfType<DataManager>();
        SettingSoing();
    }

    public void BtnNext()
    {
        AudioManger.instance.PlaySFX("Touch");

        if (++currentSong > songList.Length - 1)
            currentSong = 0;
        SettingSoing();
    }

    public void BtnPrior()
    {
        AudioManger.instance.PlaySFX("Touch");

        if (--currentSong < 0)
            currentSong = songList.Length  -1;
        SettingSoing();
    }

    void SettingSoing()
    {
        txtSongName.text = songList[currentSong].name;
        txtSongComposer.text = songList[currentSong].composer;
        txtSongScore.text = string.Format("{0:#,##0}", theData.score[currentSong]);
        imgDisk.sprite = songList[currentSong].sprite;

        AudioManger.instance.PlayBGM("BGM" + currentSong);
    }

    public void BtnBack()
    {
        TitleMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void BtnPlay()
    {
        int t_bpm = songList[currentSong].bpm;

        GameManager.instance.GameStart(currentSong, t_bpm);
        this.gameObject.SetActive(false);
    }
}
