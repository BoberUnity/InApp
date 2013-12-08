using UnityEngine;

public class Render3D : MonoBehaviour
{
    [SerializeField] private Transform screen1 = null;
    [SerializeField] private Transform screen2 = null;
    [SerializeField] private Transform scroll = null;
    [SerializeField] private TextChild textController = null;
    [SerializeField] private float speed = 1;
    private bool play = false;
    private float actualAspect = 0.75f;
    private float viewHeight = 0.22f;
    public Camera camera1 = null;
    public Camera camera2 = null;
    private bool cam1Left = false;

    private float y = 0;

    public bool Play
    {
        set { play = value;}
    }

    public bool Cam1Left
    {
        get { return cam1Left;}
        set { cam1Left = value;}
    }

    private void Start()
    {
        actualAspect = (float)Screen.height / (float)Screen.width;
        transform.localScale = new Vector3(16/(actualAspect *9), 16/(actualAspect *9), 1);
        viewHeight = 0.22f*1.7777f/actualAspect;
    }

    private void Update()
    {
        y = (scroll.localPosition.y - 65) / (1150 / (1.7777f / actualAspect));//scroll.localPosition.y / (321 * actualAspect) + 0.28f;//Движение за скроллом

        
        
        if (play)//Анимация
        {
            screen1.localPosition = new Vector3(screen1.localPosition.x - Time.deltaTime * speed, screen1.localPosition.y,
                                                screen1.localPosition.z);
        
            screen2.localPosition = new Vector3(screen2.localPosition.x - Time.deltaTime * speed, screen2.localPosition.y,
                                                screen2.localPosition.z);

            

            if (screen1.localPosition.x < -1.13f)
            {
                screen1.localPosition = new Vector3(1.13f, screen1.localPosition.y, screen1.localPosition.z);
                screen2.localPosition = new Vector3(0, screen1.localPosition.y, screen1.localPosition.z);
                play = false;
                textController.UnloadPref();
                camera1.rect = (new Rect(1.04f, y + 0.59f, 0.92f, viewHeight));
                camera2.rect = (new Rect(0.04f, y + 0.59f, 0.92f, viewHeight));
            }

            if (screen2.localPosition.x < -1.13f)
            {
                screen2.localPosition = new Vector3(1.13f, screen2.localPosition.y, screen2.localPosition.z);
                screen1.localPosition = new Vector3(0, screen1.localPosition.y, screen1.localPosition.z);
                textController.UnloadPref();
                play = false;
                camera2.rect = (new Rect(1.04f, y + 0.59f, 0.92f, viewHeight));
                camera1.rect = (new Rect(0.04f, y + 0.59f, 0.92f, viewHeight));
            }
        }

        if (camera1 != null)
            camera1.rect = (new Rect(screen1.localPosition.x / 1.13f + 0.04f, y + 0.59f, 0.92f, viewHeight));
        if (camera2 != null)
            camera2.rect = (new Rect(screen2.localPosition.x / 1.13f + 0.04f, y + 0.59f, 0.92f, viewHeight));
    }
}
