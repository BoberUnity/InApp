using UnityEngine;

public class ButtonNextQuestion : MonoBehaviour
{
  [SerializeField] private Text textController = null;
  [SerializeField] private int id = 0;
  [SerializeField] private Color rightColor = Color.green;
  [SerializeField] private Color errorColor = Color.red;
    [SerializeField] private UIButton[] otherButtons;

  protected virtual void OnPress(bool isPressed)
  {
      if (!isPressed)
      {
          textController.CurrQuestion = id;
          GetComponent<UIButton>().defaultColor = errorColor;
          GetComponent<UIButton>().hover = errorColor;
          //GetComponent<UIButton>().enabled = false;//Дописать, чтобы была анимация цвета и скейла
          //textController.NumQuestion++;
          textController.ButtonsActivate(false);
      }
  }

}
