using System;
using UnityEngine;

public class SwipeManag : MonoBehaviour
{
    public static SwipeManag instance;
    private Vector2 startTouch;
    private Vector2 swipeDelta;
    private bool touchMoved = false;
public enum Direction{
    Left,
    Right,
    Up,
    Down
};
bool[] swipe = new bool[4]; 

public delegate void MoveDelegate(bool [] swipes);
public MoveDelegate MoveEvent;

public delegate void ClickDelegate(Vector2 pos);
public ClickDelegate ClickEvent;
    Vector2 TouchPosition()
    {
        return (Vector2)Input.mousePosition;
    }

    bool TouchBegan()
    {
        return Input.GetMouseButtonDown(0);
    }

    bool TouchEnded()
    {
        return Input.GetMouseButtonUp(0);
    } 

    bool GetTouch()
    {
        return Input.GetMouseButton(0);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        instance  = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if(TouchBegan()){
            startTouch = TouchPosition();
            touchMoved = true;
        }
        else if (TouchEnded() && touchMoved == true)
        {
            SendSwipe();
            touchMoved = false;
        }

        swipeDelta = Vector2.zero;
        if(touchMoved && GetTouch())
        {
            swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        if(swipeDelta.magnitude > 50)
        {
           if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
           {
            //    if(swipeDelta.x > 0)
            //    {
            //        Debug.Log("Right");
            //    }
            //    else
            //    {
            //        Debug.Log("Left");
            //    }
            //    if (swipeDelta.y > 0)
            //    {
            //        Debug.Log("Up");
            //    }
            //    else
            //    {
            //        Debug.Log("Down");
            //    }
            swipe[(int)Direction.Left] = swipeDelta.x < 0;
            swipe[(int)Direction.Right] = swipeDelta.x > 0;
           }
           else
           {
            swipe[(int)Direction.Up] = swipeDelta.y > 0;
            swipe[(int)Direction.Down] = swipeDelta.y < 0;
           }
           SendSwipe();
        }
    }

    void SendSwipe(){
        if (swipe[0] || swipe[1] || swipe [2] || swipe[3])
        {
           Debug.Log(swipe[0] + "|" + swipe[1] +"|"+ swipe[2] + "|"+ swipe[3]);
           MoveEvent?.Invoke(swipe);
        }
        else
        {
           Debug.Log("No Swipe");
           ClickEvent?.Invoke(TouchPosition());
        }
        Reset();
    }
    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        touchMoved = false;
        for (int i = 0; i < swipe.Length; i++)
        {
            swipe[i] = false;
        }
    }
}
