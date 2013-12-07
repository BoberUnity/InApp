using UnityEngine;

public class Render3D : MonoBehaviour
{
    [SerializeField] private Transform screen1 = null;
    [SerializeField] private Transform screen2 = null;
    [SerializeField] private Transform scroll = null;
    [SerializeField] private TextChild textController = null;
    [SerializeField] private float speed = 1;
    [SerializeField] private bool play = false;
    private float actualAspect = 0.75f;
    public Camera camera1 = null;
    public Camera camera2 = null;
    public bool cam1Left = false;

    public bool Play
    {
        set {play = value;}
    }

    private void Start()
    {
        actualAspect = (float)Screen.height / (float)Screen.width;
    }

    private void Update() 
    {
        transform.position = new Vector3(transform.position.x, scroll.localPosition.y / (321 * actualAspect) + 0.28f, transform.position.z);//Движение за скроллом
        
        if (play)//Анимация
        {
            screen1.localPosition = new Vector3(screen1.localPosition.x - Time.deltaTime * speed, screen1.localPosition.y,
                                                screen1.localPosition.z);
        
            screen2.localPosition = new Vector3(screen2.localPosition.x - Time.deltaTime * speed, screen2.localPosition.y,
                                                screen2.localPosition.z);

            camera1.rect = (new Rect(screen1.localPosition.x / 1.5f + 0.04f, 0.59f, 0.92f, 0.22f));
            camera2.rect = (new Rect(screen2.localPosition.x / 1.5f + 0.04f, 0.59f, 0.92f, 0.22f));

            if (screen1.localPosition.x < -1.5f)
            {
                screen1.localPosition = new Vector3(1.5f, screen1.localPosition.y, screen1.localPosition.z);
                screen2.localPosition = new Vector3(0, screen1.localPosition.y, screen1.localPosition.z);
                play = false;
                textController.UnloadPref();
                camera1.rect = (new Rect(1.04f, 0.59f, 0.92f, 0.22f));
                camera2.rect = (new Rect(0.04f, 0.59f, 0.92f, 0.22f));
            }

            if (screen2.localPosition.x < -1.5f)
            {
                screen2.localPosition = new Vector3(1.5f, screen2.localPosition.y, screen2.localPosition.z);
                screen1.localPosition = new Vector3(0, screen1.localPosition.y, screen1.localPosition.z);
                textController.UnloadPref();
                play = false;
                camera2.rect = (new Rect(1.04f, 0.59f, 0.92f, 0.22f));
                camera1.rect = (new Rect(0.04f, 0.59f, 0.92f, 0.22f));
            }
        }   
    }
}
