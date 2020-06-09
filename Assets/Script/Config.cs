using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
public class Config : MonoBehaviour
{
#if UNITY_ANDROID && !UNITY_EDITOR
       private static string dir = Application.persistentDataPath;
#else
    private static string dir = Directory.GetCurrentDirectory();
#endif
    private static string file = @"\board_data.ini";
    private static string path = dir + file;

    public static void DeleteDataFile()
    {
        File.Delete(path);
    }
    public static void SaveBoardData(sudokuData.SudokuBoardData boardData, string level, int boardindex, int error_number, Dictionary<string, List<string>> grid_notes)
    {
        File.WriteAllText(path, string.Empty);
        StreamWriter writer = new StreamWriter(path, false);
        string current_time = "#time:"+ Clock.getCurrentTime();
        string level_ = "#level:" + level;
        string error = "#errors:" + error_number;
        string hint = "#hints:" + Lives.instance.getHintNumber().ToString();
        string board_index = "#board_index:" + boardindex.ToString();
        string unsolved = "#unsolved:";
        string solved = "#solved:";

        foreach (var unsolved_data in boardData.unsolved)
        {
            unsolved += unsolved_data.ToString() + ",";
        }
        foreach (var solved_data in boardData.solved)
        {
            solved += solved_data.ToString() + ",";
        }
        writer.WriteLine(current_time);
        writer.WriteLine(level_);
        writer.WriteLine(error);
        writer.WriteLine(hint);
        writer.WriteLine(board_index);
        writer.WriteLine(unsolved);
        writer.WriteLine(solved);

        foreach(var square in grid_notes)
        {
            string square_string = "#" + square.Key + ":";
            bool save = false;
            foreach(var note in square.Value)
            {
                if(note != " ")
                {
                    square_string += note + ",";
                    save = true;
                }
            }
            if(save)
            {
                writer.WriteLine(square_string);
            }
        }
        writer.Close();
    }

    public static Dictionary<int,List<int>> getGridNotes()
    {
        Dictionary<int, List<int>> gridNotes = new Dictionary<int, List<int>>();
        string line;
        StreamReader file = new StreamReader(path);

        while((line = file.ReadLine())!= null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#square_note")
            {
                int square_index = -1;
                List<int> notes = new List<int>();
                int.TryParse(word[1], out square_index);
                string[] substing = Regex.Split(word[2],",");

                foreach(var note in substing)
                {
                    int note_number = -1;
                    int.TryParse(note, out note_number);
                    if(note_number > 0)
                    {
                        notes.Add(note_number);
                    }
                }
                gridNotes.Add(square_index, notes);
            }
        }

        file.Close();
        return gridNotes;
    }

    public static string ReadBoardLevel()
    {
        string line, level = "";
        StreamReader file = new StreamReader(path);
        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if(word[0]=="#level")
            {
                level = word[1];

            }
        }
        file.Close();
        return level;
    }

    public static sudokuData.SudokuBoardData ReadGridData()
    {
        string line;
        StreamReader file = new StreamReader(path);
        int[] unsolved = new int[81];
        int[] solved = new int[81];

        int unsolved_index = 0, solved_index = 0;
        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#unsolved")
            {
                string[] substring = Regex.Split(word[1], ",");
                foreach(var data in substring)
                {
                    int square_number = -1;
                    if(int.TryParse(data,out square_number))
                    {
                        unsolved[unsolved_index] = square_number;
                        unsolved_index++;
                    }
                }
            }
            if (word[0] == "#solved")
            {
                string[] substring = Regex.Split(word[1], ",");
                foreach (var data in substring)
                {
                    int square_number = -1;
                    if (int.TryParse(data, out square_number))
                    {
                        solved[solved_index] = square_number;
                        solved_index++;
                    }
                }
            }
        }
        file.Close();
        return new sudokuData.SudokuBoardData(unsolved, solved);

    }

    public static int ReadGameBoardLevel()
    {
        int level = -1;
        string line;
        StreamReader file = new StreamReader(path);
        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#board_index") 
            {
                int.TryParse(word[1], out level);
            }
        }
        file.Close();
        return level;
    }

    public static float ReadGameTime()
    {
        float time = -1f;
        string line;
        StreamReader file = new StreamReader(path);
        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#time")
            {
                float.TryParse(word[1], out time);
            }
        }
        file.Close();
        return time;
    }

    public static int ReadErrorNumber()
    {
        int errors = 0;
        string line;
        StreamReader file = new StreamReader(path);
        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#errors")
            {
                int.TryParse(word[1], out errors);
            }
        }
        file.Close();
        return errors;
    }
    public static int ReadHintNumber()
    {
        int hints = 0;
        string line;
        StreamReader file = new StreamReader(path);
        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#hints")
            {
                int.TryParse(word[1], out hints);
            }
        }
        file.Close();
        return hints;
    }

    public static bool GameDatafileExist()
    {
        return File.Exists(path);
    }
 
   
}
