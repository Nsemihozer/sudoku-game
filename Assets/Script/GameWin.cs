using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameWin : MonoBehaviour
{
    public GameObject winpopup;
    public Text timetext;
    // Start is called before the first frame update
    void Start()
    {
        winpopup.SetActive(false);
        
    }
    private void OnBoardCompleted()
    {
        winpopup.SetActive(true);
        timetext.text = Clock.instance.GetCurrentTimeText().text;
    }
    private void OnEnable()
    {
        GameEvents.onBoardCompleted += OnBoardCompleted;
    }
    private void OnDisable()
    {
        GameEvents.onBoardCompleted -= OnBoardCompleted;
    }
}
