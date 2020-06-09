using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class numberButtons : Selectable,IPointerEnterHandler,IPointerUpHandler,IPointerClickHandler,ISubmitHandler
{
    public int val = 0;
    public void OnPointerClick(PointerEventData eventData)
    {
        GameEvents.UpdateSquareNumberMethod(val);      
    }

    public void OnSubmit(BaseEventData eventData)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(this.val==1)
        {
            gameObject.SetActive(false); // interactable = false;
        }*/
    }
    void OnEnable()
    {
        GameEvents.onNumberCompleted += NumberCompleted;
    }
    void OnDisable()
    {
        GameEvents.onNumberCompleted -= NumberCompleted;
    }
    private void NumberCompleted(int number)
    {
        if(val==number)
        {
            gameObject.SetActive(false);
        }
    }
}
