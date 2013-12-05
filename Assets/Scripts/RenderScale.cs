using UnityEngine;

public class RenderScale : MonoBehaviour
{
    [SerializeField] private bool position = true;
    //[SerializeField] private bool scale = false;
    [SerializeField] private bool scaleX = false;

    private void Start()
    {
        float sx = transform.localScale.x;
        float sy = transform.localScale.y;
        float sz = transform.localScale.z;
        if (position)
          transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y * 0.5625f * Screen.height / Screen.width, transform.localPosition.z);
      //if (scale)
      //{
          float scaleFactor = 0.5625f*Screen.height/Screen.width;
          if (scaleX)
              transform.localScale = new Vector3(sx *scaleFactor, sy, sz);
          //else
          //    transform.localScale = new Vector3(sx, sy * scaleFactor, sz);
      //}
  }
}
