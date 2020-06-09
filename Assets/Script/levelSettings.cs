using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSettings : MonoBehaviour
{
    public enum GameMods
    {
        not_set,
        kolay,
        orta,
        zor,
        uzman
    }
    public static levelSettings instance;
    private GameMods gameMod;
    private bool _continuePrevious = false, _exitAfterWon = false;
    private bool _paused=false;
    public void SetExitAfterWon(bool set)
    {
        _exitAfterWon = set;
        _continuePrevious = false;
        
    }

    public bool getExitAfterWon()
    {
        return _exitAfterWon;
    }
    public bool getContinuePrevious()
    {
        return _continuePrevious;
    }

    public void SetContinuePrevious(bool set)
    {
        _continuePrevious = set;
    }

    private void Awake()
    {
        _paused = false;
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
            Destroy(this);
    }
    public void setPause(bool pause) { _paused = pause; }
    public bool getPause() { return _paused; }
    void Start()
    {
        gameMod = GameMods.not_set;
        _continuePrevious = false;
    }
    public void setGameMod(GameMods mod)
    {
        gameMod = mod;
    }
    public void setGameMod(string mod)
    {
        switch (mod)
        {
            case "kolay":
                setGameMod(GameMods.kolay);
                break;
            case "orta":
                setGameMod(GameMods.orta);
                break;
            case "zor":
                setGameMod(GameMods.zor);
                break;
            case "uzman":
                setGameMod(GameMods.uzman);
                break;
            default:
                setGameMod(GameMods.not_set);
                break;
        }
    }
    public string getGameMod()
    {
        switch (gameMod)
        {
            case GameMods.kolay:
                return "kolay";
            case GameMods.orta:
                return "orta";
            case GameMods.zor:
                return "zor";
            case GameMods.uzman:
                return "uzman";
        }
        Debug.LogError("Game level not set");
        return " ";
    }
}
