using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileJoystick : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private RectTransform joystickOutline;
    [SerializeField] private RectTransform joystickKnob;

    [Header(" Settings ")]
    [SerializeField] private float moveFactor;
    private Vector3 clickedPosition;
    private Vector3 move;
    private bool canControl;

    
    void Start()
    {
        HideJoystick(); //Hiding the joystick so that the player is unable o see it untill he clicks on a viable area
    }

    private void OnDisable()
    {
        HideJoystick();  //Hiding the joustick when the gameobject is disabled
    }

    // Update is called once per frame
    void Update()
    {
        if(canControl)
            ControlJoystick();  //If the player can control the joystick, i.e, the joystick is visible call the control joystick method
    }

    public void ClickedOnJoystickZoneCallback()  //Moves the joystick to the clicked position
    {
        clickedPosition = Input.mousePosition;
        joystickOutline.position = clickedPosition;

        ShowJoystick();
    }

    private void ShowJoystick()  //Umhides the joystick and sets the canControl bool to true
    {
        joystickOutline.gameObject.SetActive(true);
        canControl = true;
    }

    private void HideJoystick()  //Hides the joystick, set the canControl bool to false and resets the move vector
    {
        joystickOutline.gameObject.SetActive(false);
        canControl = false;

        move = Vector3.zero;
    }

    private void ControlJoystick()
    {
        Vector3 currentPosition = Input.mousePosition; //Gets the current position of the joystick 
        Vector3 direction = currentPosition - clickedPosition; //Gets the direction the knob is supposed to move

        float canvasScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.x; //Gets the canvas scale 

        float moveMagnitude = direction.magnitude * moveFactor * canvasScale;  

        float absoluteWidth = joystickOutline.rect.width / 2;
        float realWidth = absoluteWidth * canvasScale;

        moveMagnitude = Mathf.Min(moveMagnitude, realWidth);

        move = direction.normalized * moveMagnitude;
        
        Vector3 targetPosition = clickedPosition + move;

        joystickKnob.position = targetPosition;

        if (Input.GetMouseButtonUp(0))
            HideJoystick();
    }

    public Vector3 GetMoveVector()
    {
        float canvasScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.x;
        return move / canvasScale;
    }
}
