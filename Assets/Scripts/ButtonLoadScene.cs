using UnityEngine;

public class ButtonLoadScene : MonoBehaviour{  [SerializeField] private Animation animation1;
  [SerializeField] private Animation animation2;
  [SerializeField] private AnimationClip animationClip1;
  [SerializeField] private AnimationClip animationClip2;
  private UIAnchor[] anchors;  protected virtual void OnPress(bool isPressed)  {    if (isPressed)
    {
        animation1.clip = animationClip1;
        animation1.Play();
        animation2.clip = animationClip2;
        animation2.Play();
        //foreach (var anim in animations)
        //{
        //    anchors = anim.GetComponentsInChildren<UIAnchor>();
        //    foreach (var an in anchors)
        //    {
        //        an.enabled = false;
        //    }
        //    anim.Play();
        //}
    }  }}