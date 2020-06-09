using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sudokuData : MonoBehaviour
{
    public static sudokuData instance;
    public struct SudokuBoardData
    {
        public int[] unsolved;
        public int[] solved;

        public SudokuBoardData(int[] unsolved, int[] solved):this()
        {
            this.unsolved = unsolved;
            this.solved = solved;
        }
    };
    public Dictionary<string, List<SudokuBoardData>> game = new Dictionary<string, List<SudokuBoardData>>();

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        
        game.Add("kolay",EasyData.getData());
        game.Add("orta", MediumData.getData());
        game.Add("zor", HardData.getData());
        game.Add("uzman", SpecialData.getData());
    }

    // Update is called once per frame

}
