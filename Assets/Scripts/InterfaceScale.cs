using UnityEngine;

public class InterfaceScale : MonoBehaviour
{
    [SerializeField] private bool position = true;
    [SerializeField] private bool scaleY = false;
    [SerializeField] private bool scaleX = false;

    private void Start()
    {
        float sx = transform.localScale.x;
        float sy = transform.localScale.y;
        float sz = transform.localScale.z;
        if (position)
          transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y * 0.5625f * Screen.height / Screen.width, transform.localPosition.z);
      if (scaleY || scaleX)
      {
          float scaleFactor = 0.5625f*(Screen.height/Screen.width);
          if (scaleX && !scaleY)
              transform.localScale = new Vector3(sx * scaleFactor, sy, sz);
          if (scaleY && !scaleX)
              transform.localScale = new Vector3(sx, sy * scaleFactor, sz * scaleFactor);
          if (scaleY && scaleX)
              transform.localScale = new Vector3(sx * scaleFactor, sy * scaleFactor, sz * scaleFactor);

      }
  }
}
