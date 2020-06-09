using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Lives : MonoBehaviour
{
    public Text liveText;
    public GameObject gameOver,Reward;
    int lives = 0;
    int error_number = 0;
    int hint = 1;
    public static Lives instance;
    private void Awake()
    {
        if (instance)
            Destroy(instance);
        instance = this;
    }
    void Start()
    {
        lives = 3;
        error_number = 0;
        hint = 1;
        if (levelSettings.instance.getContinuePrevious())
        {
            error_number = Config.ReadErrorNumber();
            liveText.text = error_number.ToString();
            hint = Config.ReadHintNumber();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int getErrorNumber()
    {
        return error_number;
    }
    public int getHintNumber()
    {
        return hint;
    }
    public void setHintNumber(int h)
    {
        hint=h;
    }
    private void WrongNumber()
    {
        if(error_number<lives-1)
        {
            error_number++;
            liveText.text = error_number.ToString();         
        }
        else
        {
            GameEvents.onGameOverMethod();
            error_number++;
            liveText.text = error_number.ToString();
            gameOver.SetActive(true);
        }

    }
    public void ResetErrors()
    {
        error_number = 0;
        liveText.text = error_number.ToString();
    }
    private void OnEnable()
    {
        GameEvents.onWrong += WrongNumber;
    }
    private void OnDisable()
    {
        GameEvents.onWrong -= WrongNumber;
    }
}
