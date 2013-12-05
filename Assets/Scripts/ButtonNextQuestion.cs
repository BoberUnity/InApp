using UnityEngine;

public class ButtonNextQuestion : MonoBehaviour
{
  [SerializeField] private Text textController = null;
  [SerializeField] private int id = 0;

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
    {
        textController.CurrQuestion = id;
        textController.NumQuestion++;
    }
  }
}
