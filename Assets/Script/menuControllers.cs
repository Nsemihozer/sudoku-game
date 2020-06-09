using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuControllers : MonoBehaviour
{
    // Start is called before the first frame update
    public Text clock;
    void Start()
    {
        
    }
    public void loadScreen(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void Easy()
    {
        //AdManager.instance.showInterstitialAd();
        levelSettings.instance.setGameMod(levelSettings.GameMods.kolay);
        SceneManager.LoadScene("GameScene");
        levelSettings.instance.SetExitAfterWon(false);
    }
    public void Special()
    {
        //AdManager.instance.showInterstitialAd();
        levelSettings.instance.setGameMod(levelSettings.GameMods.uzman);
        SceneManager.LoadScene("GameScene");
        levelSettings.instance.SetExitAfterWon(false);
    }
    public void Medium()
    {
        //AdManager.instance.showInterstitialAd();
        levelSettings.instance.setGameMod(levelSettings.GameMods.orta);
        SceneManager.LoadScene("GameScene");
        levelSettings.instance.SetExitAfterWon(false);
    }
    public void Hard()
    {
        //AdManager.instance.showInterstitialAd();
        levelSettings.instance.setGameMod(levelSettings.GameMods.zor);
        SceneManager.LoadScene("GameScene");
        levelSettings.instance.SetExitAfterWon(false);
    }
    public void ActivateObj(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void deActivateObj(GameObject obj)
    {
        obj.SetActive(false);
    }
    public void ContinueAfterGameOver()
    {
        AdManager.instance.showRewardAd();
    }
    public void ReceiveReward()
    {
        Lives.instance.ResetErrors();
        Clock.instance.onGameOverContinue();
    }
    public void setPause(bool pause)
    {
        levelSettings.instance.setPause(pause);
        clock.text= Clock.instance.GetCurrentTimeText().text;
    }
    public void ContinuePreviousGame(bool continue_game)
    {
        if(continue_game)
            AdManager.instance.showInterstitialAd();
        levelSettings.instance.SetContinuePrevious(continue_game);
    }
    public void ExitAfterWon()
    {
        levelSettings.instance.SetExitAfterWon(true);
    }
    public void PrivacyOpen()
    {
        Application.OpenURL("https://cotyoragames.com/projects/classicsudoku-privacy_policy.php");
    }
    public void SiteOpen()
    {
        Application.OpenURL("https://cotyoragames.com/index.php");
    }
}
