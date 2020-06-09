using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SudokuGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject gridsquare;
    private float square_offset = 2.2f;
    private Vector2 start_position = new Vector2(-435f, 442f);
    private float square_scale = 0.9f,square_gap=5.1f;
    private int col = 9, row = 9;
    private List<GameObject> grid_squares = new List<GameObject>();
    private int selected_grid = -1;
    public Color linehighcol = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        //MobileAds.Initialize(initStatus => { });
        gridCreate();
        if(levelSettings.instance.getContinuePrevious())
        {
            SetGridFromFile();
        }
        else
            setGridNumbers(levelSettings.instance.getGameMod());
        //RequestBanner();
        AdManager.instance.showBannerAd();
    }

    void SetGridFromFile()
    {
        string level = levelSettings.instance.getGameMod();
        selected_grid = Config.ReadGameBoardLevel();
        var data = Config.ReadGridData();
        Lives.instance.setHintNumber(Config.ReadHintNumber());
        setGridData(data);
        SetGridNotes(Config.getGridNotes());
    }

    void SetGridNotes(Dictionary<int,List<int>>notes)
    {
        foreach(var note in notes)
        {
            grid_squares[note.Key].GetComponent<GridSquare>().SetGridNotes(note.Value);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void gridCreate()
    {
        spawnGridSquares();
        setSquaresPosition();
    }
    private void spawnGridSquares()
    {
        int square_index = 0;
        for(int i=0;i<row;i++)
        {
            for(int j=0;j<col;j++)
            {
                grid_squares.Add(Instantiate(gridsquare) as GameObject);
                grid_squares[grid_squares.Count - 1].GetComponent<GridSquare>().setSquareIndex(square_index);
                grid_squares[grid_squares.Count - 1].transform.SetParent(this.transform);
                grid_squares[grid_squares.Count - 1].transform.localScale = new Vector3(square_scale, square_scale, square_scale);
                square_index++;
            }
        }
    }
    private void setSquaresPosition()
    {
        var square_rect = grid_squares[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        Vector2 gapNumber = new Vector2(0.0f, 0.0f);
        bool row_moved=false;
        offset.x = square_rect.rect.width * square_rect.transform.localScale.x+ square_offset;
        offset.y = square_rect.rect.height * square_rect.transform.localScale.y + square_offset;
        int col_number = 0,row_number=0;
        foreach (GameObject square in grid_squares)
        {
         if(col_number+1>col)
            {
                row_number++;
                col_number = 0;
                gapNumber.x = 0;
                row_moved = false;
            }
            var pos_x_offset = offset.x * col_number + (gapNumber.x* square_gap);
            var pos_y_offset = offset.y * row_number+ (gapNumber.y * square_gap);

            if(col_number >0 && col_number %3 ==0)
            {
                gapNumber.x++;
                pos_x_offset += square_gap;
            }
            if(row_number >0 && row_number %3 == 0 && row_moved == false)
            {
                row_moved = true;
                gapNumber.y++;
                pos_y_offset += square_gap;
            }
            square.GetComponent<RectTransform>().anchoredPosition = new Vector3(start_position.x + pos_x_offset, start_position.y - pos_y_offset);
            col_number++;
        }
    }
    private void setGridNumbers(string level)
    {
        selected_grid = Random.Range(0, sudokuData.instance.game[level].Count);
        var data = sudokuData.instance.game[level][selected_grid];
        setGridData(data);
        
    }
    private void setGridData(sudokuData.SudokuBoardData data)
    {
        for(int i=0;i<grid_squares.Count;i++)
        {
            grid_squares[i].GetComponent<GridSquare>().setNumber(data.unsolved[i]);
            grid_squares[i].GetComponent<GridSquare>().setCorrectNumber(data.solved[i]);
            grid_squares[i].GetComponent<GridSquare>().setDefault(data.unsolved[i] == data.solved[i] ? true : false);
        }
    }
    private void OnEnable()
    {
        GameEvents.onSelected += OnSquareSelected;
        GameEvents.onCheckBoardCompleted += CheckCompleted;
        GameEvents.onCheckNumberCompleted += CheckNumberCompleted;
    }

    private void OnDisable()
    {
        GameEvents.onSelected -= OnSquareSelected;
        GameEvents.onCheckBoardCompleted -= CheckCompleted;
        GameEvents.onCheckNumberCompleted -= CheckNumberCompleted;


        var solved_data = sudokuData.instance.game[levelSettings.instance.getGameMod()][selected_grid].solved;
        int[] unsolved_data = new int[81];  //sudokuData.instance.game[levelSettings.instance.getGameMod()][selected_grid].solved;
        Dictionary<string, List<string>> grid_notes = new Dictionary<string, List<string>>();
        for (int i=0;i< grid_squares.Count;i++)
        {
            var comp = grid_squares[i].GetComponent<GridSquare>();
            unsolved_data[i] = comp.getSquareNumber();
            string key = "square_note:"+i.ToString();
            grid_notes.Add(key, comp.getSquareNotes());
        }
        sudokuData.SudokuBoardData current_game_data = new sudokuData.SudokuBoardData(unsolved_data, solved_data);
        if(levelSettings.instance.getExitAfterWon() == false)
        {
            Config.SaveBoardData(current_game_data,levelSettings.instance.getGameMod(),selected_grid,Lives.instance.getErrorNumber(),grid_notes);
        }
        else
        {
            Config.DeleteDataFile();
        }
        AdManager.instance.HideBanner();
    }
    private void SetSquaresColor(int [] data, Color col)
    {
        foreach(var index in data)
        {
            var comp = grid_squares[index].GetComponent<GridSquare>();
            if(comp.isSelected()==false)
            {
                comp.setSquareColor(col);
            }
        }
    }
    public void OnSquareSelected(int index)
    {
        var horizontal = LineIndicator.instance.getRow(index);
        var vertical = LineIndicator.instance.getColumn(index);
        var square = LineIndicator.instance.getSquare(index);

        SetSquaresColor(LineIndicator.instance.getAllSquareIndexes(), Color.white);
        SetSquaresColor(horizontal, linehighcol);
        SetSquaresColor(vertical, linehighcol);
        SetSquaresColor(square, linehighcol);
    }

    private void CheckCompleted()
    {
        foreach(var square in grid_squares)
        {
            var comp = square.GetComponent<GridSquare>();
            if(!comp.isCorrectNumberSet())
            {
                return;
            }
        }
        GameEvents.onBoardCompletedMethod();
    }

    private void CheckNumberCompleted(int number)
    {
        int count = 0;
        foreach (var square in grid_squares)
        {
            var comp = square.GetComponent<GridSquare>();
            if (comp.getCorrectNumber()== number)
            {
                if(!comp.isCorrectNumberSet())
                {
                    return;
                }
            }
        }
        GameEvents.onNumberCompletedMethod(number);
    }
    public void solve()
    {
        foreach (var square in grid_squares)
        {
            var comp = square.GetComponent<GridSquare>();
            comp.setCorrectNumber();
        }
        CheckCompleted();
    }
}
