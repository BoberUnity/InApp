using UnityEngine;

public class ButtonMission : MonoBehaviour{  [SerializeField] private Text textController = null;
    protected virtual void OnPress(bool isPressed)  {    if (isPressed)    {        textController.StartFirstQuestion();    }  }}