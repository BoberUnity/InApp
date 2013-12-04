using UnityEngine;

public class ButtonNextQuestion : MonoBehaviour{  [SerializeField] private Text textController = null;  protected virtual void OnPress(bool isPressed)  {    if (isPressed)
    {
        textController.NumQuestion++;
    }  }}