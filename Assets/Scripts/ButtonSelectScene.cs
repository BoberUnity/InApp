using UnityEngine;

public class ButtonSelectScene : MonoBehaviour{  [SerializeField] private int id;
    protected virtual void OnPress(bool isPressed)  {    if (!isPressed)
    {
        Application.LoadLevel(id);
    }  }}