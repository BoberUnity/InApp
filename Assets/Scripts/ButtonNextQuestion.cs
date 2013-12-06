using System.Collections;
using UnityEngine;

public class ButtonNextQuestion : MonoBehaviour
{
  [SerializeField] private Text textController = null;
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
          //GetComponent<TweenColor>().from = Color.red;
          //textController.NumQuestion++;
          //TweenColor.Begin(gameObject, GetComponent<UIButton>().duration, errorColor);
          //GetComponent<TweenColor>().enabled = true;
          //GetComponent<TweenColor>().from = Color.white;
          //GetComponent<TweenColor>().to = Color.red;
          //StartCoroutine(DisableButtons(GetComponent<UIButton>().duration));
          textController.ButtonsActivate(false);
      }
  }

  private IEnumerator DisableButtons(float time)
  {
      yield return new WaitForSeconds(time);
      textController.ButtonsActivate(false);
  }
}
