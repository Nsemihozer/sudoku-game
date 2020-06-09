using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ContinueButton : MonoBehaviour
{
    public Text time, level;
    // Start is called before the first frame update
    void Start()
    {
        if(Config.GameDatafileExist() == false)
        {
            gameObject.GetComponent<Button>().interactable = false;
            time.text = " ";
            level.text = " ";
        }
        else
        {
            float delta = Config.ReadGameTime();
            delta += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(delta);
            string min = leadingZero(span.Minutes + (span.Hours * 60));
            string sec = leadingZero(span.Seconds);
            time.text = min + ":" + sec;
            level.text = Config.ReadBoardLevel();
        }
    }
    string leadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
    // Update is called once per frame
    public void SetGameData()
    {
        levelSettings.instance.setGameMod(Config.ReadBoardLevel());
    }
}
