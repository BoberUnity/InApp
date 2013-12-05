using UnityEngine;

public class Render3D : MonoBehaviour
{
    [SerializeField] private Transform screen1 = null;
    [SerializeField] private Transform screen2 = null;
    [SerializeField] private Transform scroll = null;
    [SerializeField] private float speed = 1;
    [SerializeField] private bool play = false;
    private float actualAspect = 0.75f;

    public bool Play
    {
        set { play = value; }
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

            if (screen1.localPosition.x < -1.5f)
            {
                screen1.localPosition = new Vector3(1.5f, screen1.localPosition.y,
                                                    screen1.localPosition.z);
                screen2.localPosition = new Vector3(0, screen1.localPosition.y,
                                                    screen1.localPosition.z);
                play = false;
            }

            if (screen2.localPosition.x < -1.5f)
            {
                screen2.localPosition = new Vector3(1.5f, screen2.localPosition.y,
                                                    screen2.localPosition.z);
                screen1.localPosition = new Vector3(0, screen1.localPosition.y,
                                                    screen1.localPosition.z);
                play = false;
            }
        }   
    }
}
