using UnityEngine;

public class ButtonLoadScene : MonoBehaviour{  [SerializeField] private int id = 0;  protected virtual void OnPress(bool isPressed)  {    if (isPressed)    {      Application.LoadLevel(id);    }  }}