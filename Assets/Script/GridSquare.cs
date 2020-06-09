using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class GridSquare : Selectable,IPointerClickHandler,ISubmitHandler,IPointerUpHandler,IPointerExitHandler
{
    public GameObject number_text;
    public List<GameObject> note_numbers;
    private bool note_active;
    private int number = 0,correct_number=0;
    private bool selected = false;
    private int square_index = -1;
    private bool is_default;
    
    // Start is called before the first frame update
    override protected void  Start()
    {
        note_active = false;
        selected = false;
        if(levelSettings.instance.getContinuePrevious())
        {
            setClearEmptyNotes();
        }
        else
        {
            setNoteNumberValue(0);
        }
        
    }

    public List<string> getSquareNotes()
    {
        List<string> notes = new List<string>();
        foreach(var number in note_numbers)
        {
            notes.Add(number.GetComponent<Text>().text);
        }
        return notes;
    }

    private void setClearEmptyNotes()
    {
        foreach (var number in note_numbers)
        {
            if(number.GetComponent<Text>().text=="0")
            {
                number.GetComponent<Text>().text = " ";
            }
        }
    }
    private void setNoteNumberValue(int value)
    {
        foreach (var number in note_numbers)
        {
            if (value <=0)
            {
                number.GetComponent<Text>().text = " ";
            }
            else
            {
                number.GetComponent<Text>().text = value.ToString();
            }
        }
    }

    private void setNoteSingleNumberValue(int value,bool update=false)
    {
        if (note_active == false && update == false)
            return;
        if(value <=0)
        {
            note_numbers[value - 1].GetComponent<Text>().text = " ";
        }
        else
        {
            if(note_numbers[value-1].GetComponent<Text>().text == " " || update)
            {
                note_numbers[value - 1].GetComponent<Text>().text = value.ToString();
            }
            else
            {
                note_numbers[value - 1].GetComponent<Text>().text = " ";
            }
        }
    }

    public void SetGridNotes(List<int> notes)
    {
        foreach (var note in notes)
        {
            setNoteSingleNumberValue(note, true); 
        }
    }
    public void OnNotesActive(bool active)
    {
        note_active = active;
    }

    public void setDefault(bool def)
    {
        is_default=def;
    }
    public bool getDefault()
    {
        return is_default;
    }
    public bool isSelected()
    {
        return selected;
    }
    public void setCorrectNumber(int number)
    {
        correct_number = number;
        if(this.number !=0 && this.number != correct_number)
        {
            number_text.GetComponent<Text>().color = Color.red;
        }
    }
    public void setCorrectNumber()
    {
        number = correct_number ;
        number_text.GetComponent<Text>().color = Color.white;
        displayText();
        is_default = true;
    }
    public void setSquareIndex(int index)
    {
        square_index = index;
    }
    public int getSquareNumber()
    {
        return number;
    }
    public int getCorrectNumber()
    {
        return correct_number;
    }
    public void displayText()
    {
        if(number<=0)
        {
            number_text.GetComponent<Text>().text = " ";
        }
        else
        {
            number_text.GetComponent<Text>().text = number.ToString();
        }
    }
    public void setNumber(int num)
    {
        number = num;
        displayText();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        selected = true;
        GameEvents.SquareSelectedMethod(square_index);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        
    }
    override protected void OnEnable()
    {
        GameEvents.onUpdate += onSet;
        GameEvents.onSelected += onSelected;
        GameEvents.onNotesActive += OnNotesActive;
        GameEvents.onClearNumber += OnClearNumber;
        GameEvents.onHint += onHint;
    }
    private void OnDisable()
    {
        GameEvents.onUpdate -= onSet;
        GameEvents.onSelected -= onSelected;
        GameEvents.onNotesActive -= OnNotesActive;
        GameEvents.onClearNumber -= OnClearNumber;
        GameEvents.onHint -= onHint;
    }
    public void OnClearNumber()
    {
        if(selected && !is_default)
        {
            number = 0;
            setNoteNumberValue(0);
            displayText();
        }
    }
    public void onHint()
    {
        if(selected && !is_default && this.number!=this.correct_number)
        {
            if(Lives.instance.getHintNumber() == 1)
            {
                Debug.Log(Lives.instance.getHintNumber());
                setNoteNumberValue(0);
                setCorrectNumber();
                Lives.instance.setHintNumber(0); 
            }
            else
            {
                AdManager.instance.showInterstitialAd();
                setNoteNumberValue(0);
                setCorrectNumber();
            }
            GameEvents.CheckNumberCompletedMethod(number);
            GameEvents.CheckboardCompletedMethod();
        }
        
    }
    public void onSet(int number)
    {
        if(selected && !is_default)
        {
            if (note_active == true)
            {
                setNoteSingleNumberValue(number);
            }
            else
            {
                setNoteNumberValue(0);
                setNumber(number);
                if (number != correct_number)
                {
                    Color color = new Color(242f / 255f, 100f / 255f, 100f / 255f);
                    number_text.GetComponent<Text>().color = color;
                    GameEvents.onWrongNumberMethod();
                }
                else
                {
                    is_default = true;
                    Color color = new Color(0, 128f/255f, 255f/255f);
                    number_text.GetComponent<Text>().color = color;
                    GameEvents.CheckNumberCompletedMethod(number);
                }
            }
            GameEvents.CheckboardCompletedMethod();
        }
        
    }
    public void onSelected(int index)
    {
        if(square_index != index)
        {
            selected = false;
        }  
    }
    public void setSquareColor(Color col)
    {
        var colors = this.colors;
        colors.normalColor = col;
        this.colors = colors;
    }
    public bool isCorrectNumberSet()
    {
        return number == correct_number;
    }
}
