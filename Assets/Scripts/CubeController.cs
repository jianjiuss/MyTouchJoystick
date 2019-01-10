using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float speed;

    public JoystickController joystickController;

    void Update ()
    {
        float axisX = joystickController.GetAxis("axisX") * speed;
        float axisY = joystickController.GetAxis("axisY") * speed;

        gameObject.transform.Rotate(Vector3.up, -axisX, Space.World);
        gameObject.transform.Rotate(Vector3.left, -axisY, Space.World);

    }


    //public void OnGUI()
    //{
    //    GUIStyle style = new GUIStyle();
    //    style.fontSize = 25;

    //    GUI.Label(new Rect(20, 20, 300, 50), "dis:" + JoystickController.Ins.dis, style);

    //    GUI.Label(new Rect(20, 80, 300, 50), "horizontal:" + JoystickController.Ins.GetAxis("axisX"), style);
 
    //    GUI.Label(new Rect(20, 140, 300, 50), "vertical:" + JoystickController.Ins.GetAxis("axisY"), style);

    //    GUI.Label(new Rect(20, 200, 300, 50), "touchPos:" + (Input.touchCount > 0 ? Input.GetTouch(0).position : Vector2.zero), style);

    //    if(Input.touchCount > 0)
    //    {
    //        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.GetTouch(0).position, canvas.worldCamera, out pos);
    //        GUI.Label(new Rect(20, 260, 300, 50), pos.ToString(), style);

    //        var x = rectTransform.rect.width / 2;
    //        var y = rectTransform.rect.height / 2;
    //        GUI.Label(new Rect(20, 320, 300, 50), (new Vector2(x, y) + pos).ToString() + " rect1:" + JoystickController.Ins.rect1 + " actualPos1:" + JoystickController.Ins.actualPos1 + " include:" + JoystickController.Ins.include, style);

            
    //    }

    //    GUI.Label(new Rect(20, 380, 300, 50), JoystickController.Ins.began.ToString(), style);
    //    GUI.Label(new Rect(20, 440, 300, 50), joystick.GetComponent<RectTransform>().anchoredPosition.ToString() + "  size:" + joystick.GetComponent<RectTransform>().rect.size, style);
    //    GUI.Label(new Rect(20, 500, 300, 50), JoystickController.Ins.touchIndex.ToString(), style);
    //}
}
