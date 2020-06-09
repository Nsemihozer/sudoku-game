using UnityEngine;

public class LineIndicator : MonoBehaviour
{
    public static LineIndicator instance;
    private int[,] line = new int[9, 9]
    {
        { 0,1,2,    3,4,5,      6,7,8 },
        { 9,10,11,  12,13,14,  15,16,17},
        { 18,19,20, 21,22,23,  24,25,26 },

        { 27,28,29,  30,31,32,  33,34,35 },
        { 36,37,38,  39,40,41,  42,43,44 },
        { 45,46,47,  48,49,50,  51,52,53 },

        { 54,55,56,  57,58,59,  60,61,62 },
        { 63,64,65,  66,67,68,  69,70,71 },
        { 72,73,74,  75,76,77,  78,79,80 }
    };

    private int[] line_flat = new int[81]
    {
         0,1,2,    3,4,5,      6,7,8,
         9,10,11,  12,13,14,  15,16,17,
         18,19,20, 21,22,23,  24,25,26 ,

         27,28,29,  30,31,32,  33,34,35,
         36,37,38,  39,40,41,  42,43,44,
         45,46,47,  48,49,50,  51,52,53,

         54,55,56,  57,58,59,  60,61,62,
         63,64,65,  66,67,68,  69,70,71,
         72,73,74,  75,76,77,  78,79,80
    };

    private int[,] square = new int[9, 9]
    {
        {0,1,2,9,10,11,18,19,20},
        {3,4,5,12,13,14,21,22,23},
        {6,7,8,15,16,17,24,25,26},
        {27,28,29,36,37,38,45,46,47},
        {30,31,32,39,40,41,48,49,50},
        {33,34,35,42,43,44,51,52,53},
        {54,55,56,63,64,65,72,73,74},
        {57,58,59,66,67,68, 75,76,77},
        {60,61,62,69,70,71,78,79,80}
    };
    void Awake()
    {
        if (instance)
            Destroy(instance);
        else
            instance = this;
    }
    private (int,int) getSquarePosition(int index)
    {
        int row_pos = -1;
        int col_pos = -1;
        for(int i=0;i<9;i++)
        {
            for(int j=0;j<9;j++)
            {
                if(line[i,j]==index)
                {
                    row_pos = i;
                    col_pos = j;
                }
            }
        }
        return (row_pos, col_pos);  
    }

    public int[] getRow(int index)
    {
        int[] l = new int[9];
        var row_pos = getSquarePosition(index).Item1;
        for(int i=0;i<9;i++)
        {
            l[i] = line[row_pos, i];
        }
        return l;
    }
    public int[] getColumn(int index)
    {
        int[] l = new int[9];
        var col_pos = getSquarePosition(index).Item2;
        for (int i = 0; i < 9; i++)
        {
            l[i] = line[i, col_pos];
        }
        return l;
    }

    public int[] getSquare(int index)
    {
        int[] l = new int[9];
        int row_pos = -1;

        for(int row=0;row<9;row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if(square[row,col]==index)
                {
                    row_pos = row;
                }
            }
        }
        for (int i = 0; i < 9; i++)
        {
            l[i] = square[row_pos,i];
        }
        return l;
    }

    public int[] getAllSquareIndexes()
    {
        return line_flat;
    }

}
