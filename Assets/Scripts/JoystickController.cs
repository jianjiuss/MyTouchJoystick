using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour
{
    public bool isRight;

    private GameObject handler;
    private RectTransform handlerTrans;
    private RectTransform joystickTrans;

    private float MaxHorizontal;
    private float MinHorizontal;

    private float MaxVertical;
    private float MinVertical;

    private Vector2 output;

    private int? curFingerId = null;

    private Canvas canvas;
    private RectTransform canvasRect;

    private void Start () {
        handler = transform.Find("Handler").gameObject;
        handlerTrans = handler.GetComponent<RectTransform>();
        joystickTrans = GetComponent<RectTransform>();

        MaxHorizontal = joystickTrans.rect.width / 2;
        MinHorizontal = -MaxHorizontal;

        MaxVertical = joystickTrans.rect.height / 2;
        MinVertical = -MaxVertical;

        canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.gameObject.GetComponent<RectTransform>();
	}

    public void HandleDragBegan(BaseEventData baseEventData)
    {
        var rect = new Rect(joystickTrans.anchoredPosition - new Vector2(joystickTrans.rect.size.x / 2, joystickTrans.rect.size.y / 2), joystickTrans.rect.size);
        rect.x = FixRightAnchoredPos(rect.x);

        foreach (var touch in Input.touches)
        {
            var actualPos = GetPosInCanvasPos(touch.position);
            if (rect.Contains(actualPos))
            {
                curFingerId = touch.fingerId;
            }
        }
    }

    private float FixRightAnchoredPos(float x)
    {
        if (isRight)
        {
            var canvasRect = canvas.gameObject.GetComponent<RectTransform>();
            x += canvasRect.rect.width;
        }

        return x;
    }

    public void HandleDrag(BaseEventData baseEventData)
    {
        if(!curFingerId.HasValue)
        {
            return;
        }

        Touch curTouch = new Touch();
        foreach(var touch in Input.touches)
        {
            if(curFingerId == touch.fingerId)
            {
                curTouch = touch;
            }
        }

        var joystickTranAnPos = joystickTrans.anchoredPosition;
        joystickTranAnPos.x = FixRightAnchoredPos(joystickTranAnPos.x);

        Vector3 movePos = Vector3.zero;
        movePos = GetPosInCanvasPos(curTouch.position);

        var dis = Vector3.Distance(movePos, joystickTranAnPos);
        if (Vector3.Distance(movePos, joystickTranAnPos) < MaxHorizontal)
        {
            handlerTrans.anchoredPosition = (Vector2)movePos - joystickTranAnPos;
        }
        else
        {
            var dragPosRelativeToPivot1 = movePos - (Vector3)joystickTranAnPos;
            handlerTrans.anchoredPosition = dragPosRelativeToPivot1.normalized * MaxHorizontal;
        }

        Vector2 tempOutput = new Vector2(handlerTrans.anchoredPosition.x / MaxHorizontal, handlerTrans.anchoredPosition.y / MaxVertical);

        output = CircleToSquare(tempOutput);
    }

    private Vector2 GetPosInCanvasPos(Vector2 input)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, input, canvas.worldCamera, out pos);
        var x = canvasRect.rect.width / 2;
        var y = canvasRect.rect.height / 2;
        return new Vector2(x, y) + pos;
    }

    public void HandleDragEnd(BaseEventData baseEventData)
    {
        handlerTrans.anchoredPosition = Vector3.zero;
        output = Vector2.zero;
        curFingerId = null;
    }

    public float GetAxis(string axisName)
    {
        if(axisName.Equals("axisX"))
        {
            return output.x;
        }
        else if(axisName.Equals("axisY"))
        {
            return output.y;
        }
        else
        {
            return 0;
        }
    }

    private Vector2 CircleToSquare(Vector2 input)
    {
        Vector2 output = Vector2.zero;

        output.x = (Mathf.Sqrt(2 + Mathf.Pow(input.x, 2) - Mathf.Pow(input.y, 2) + 2 * Mathf.Sqrt(2) * input.x) / 2) - (Mathf.Sqrt(2 + Mathf.Pow(input.x, 2) - Mathf.Pow(input.y, 2) - 2 * Mathf.Sqrt(2) * input.x) / 2);
        output.y = (Mathf.Sqrt(2 - Mathf.Pow(input.x, 2) + Mathf.Pow(input.y, 2) + 2 * Mathf.Sqrt(2) * input.y) / 2) - (Mathf.Sqrt(2 - Mathf.Pow(input.x, 2) + Mathf.Pow(input.y, 2) - 2 * Mathf.Sqrt(2) * input.y) / 2);

        return output;
    }
}
