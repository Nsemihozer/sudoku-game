using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public delegate void CheckBoardCompleted();
    public static event CheckBoardCompleted onCheckBoardCompleted;

    public static void CheckboardCompletedMethod()
    {
        if (onCheckBoardCompleted != null)
        {
            onCheckBoardCompleted();
        }
    }
    public delegate void CheckNumberCompleted(int number);
    public static event CheckNumberCompleted onCheckNumberCompleted;

    public static void CheckNumberCompletedMethod(int number)
    {
        if (onCheckNumberCompleted != null)
        {
            onCheckNumberCompleted(number);
        }
    }
    public delegate void UpdateSquareNumber(int number);
    public static event UpdateSquareNumber onUpdate;

    public static void UpdateSquareNumberMethod(int number)
    {
        if(onUpdate != null)
        {
            onUpdate(number);
        }
    }
    public delegate void SquareSelected(int square_index);
    public static event SquareSelected onSelected;

    public static void SquareSelectedMethod(int square_index)
    {
        if (onSelected != null)
        {
            onSelected(square_index);
        }
    }

    public delegate void WrongNumber();
    public static event WrongNumber onWrong;

    public static void onWrongNumberMethod()
    {
        if (onWrong != null)
        {
            onWrong();
        }
    }

    public delegate void GameOver();
    public static event GameOver onGameOver;

    public static void onGameOverMethod()
    {
        if (onGameOver != null)
        {
            onGameOver();
        }
    }

    public delegate void notesActive(bool active);
    public static event notesActive onNotesActive;

    public static void onNotesActiveMethod(bool active)
    {
        if (onNotesActive != null)
        {
            onNotesActive(active);
        }
    }

    public delegate void ClearNumber();
    public static event ClearNumber onClearNumber;
    public static void onClearNumberMethod()
    {
        if (onClearNumber != null)
        {
            onClearNumber();
        }
    }

    public delegate void Hint();
    public static event Hint onHint;
    public static void onHintMethod()
    {
        if (onHint != null)
        {
            onHint();
        }
    }

    public delegate void BoardCompleted();
    public static event BoardCompleted onBoardCompleted;
    public static void onBoardCompletedMethod()
    {
        if (onBoardCompleted != null)
        {
            onBoardCompleted();
        }
    }
    public delegate void NumberCompleted(int number);
    public static event NumberCompleted onNumberCompleted;
    public static void onNumberCompletedMethod(int number)
    {
        if (onNumberCompleted != null)
        {
            onNumberCompleted(number);
        }
    }
}
