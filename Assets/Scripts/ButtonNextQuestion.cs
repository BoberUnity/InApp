using System.Collections;
using UnityEngine;

public class ButtonNextQuestion : MonoBehaviour
{
  [SerializeField] private TextChild textController = null;
  [SerializeField] private int id = 0;
  [SerializeField] private Color rightColor = Color.green;
  [SerializeField] private Color errorColor = Color.red;
  //[SerializeField] private UIButton[] otherButtons;

  protected virtual void OnPress(bool isPressed)
  {
      if (!isPressed)
      {
          if (textController.CurrQuestion(id))
              GetComponent<UIButton>().defaultColor = rightColor;
          else
              GetComponent<UIButton>().defaultColor = errorColor;

          textController.ButtonsActivate(false);
      }
  }

  //private IEnumerator DisableButtons(float time)
  //{
  //    yield return new WaitForSeconds(time);
  //    textController.ButtonsActivate(false);
  //}
}
