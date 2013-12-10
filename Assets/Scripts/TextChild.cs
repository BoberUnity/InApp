using System;
using System.Collections;
using System.Globalization;
using UnityEngine;

public class TextChild : MonoBehaviour
{
    [SerializeField] private Render3D render3D = null;
    [SerializeField] private UILabel labelQuetion = null;
    [SerializeField] private UILabel labelA = null;
    [SerializeField] private UILabel labelB = null;
    [SerializeField] private UILabel labelC = null;
    [SerializeField] private UILabel labelD = null;
    [SerializeField] private Text t = null;//
    [SerializeField] private GameObject load = null;
    private int numQuestion = 0;
    private GameObject instance = null;
    private GameObject instance2 = null;
    
    public bool CurrQuestion(int id)
    {
        if (instance != null)
        {
            if (instance.GetComponent<Animation>() != null)
            {
                if (instance.animation.clip != null)
                {
                    instance.animation.Play();
                    StartCoroutine(LoadNextQuestion(instance.animation.clip.length));
                    StartCoroutine(ShowLoad(instance.animation.clip.length-0.01f));
                }
                else
                {
                    StartCoroutine(LoadNextQuestion(2));//Error
                    Debug.LogWarning("Prefab animation has not clip");
                }
            }
            else
            {
                Debug.LogWarning("Prefab has not animation");
            }
        }
        else
            Debug.LogWarning("Prefab was not found");
        

        return id.ToString() == t.allBox[numQuestion][3];
    }

    public int NumQuestion
    {
        get { return numQuestion;}
        set
        {
            numQuestion = value;
            labelQuetion.text = t.allBox[numQuestion][1];
            labelA.text = t.allBox[numQuestion][4];
            labelB.text = t.allBox[numQuestion][5];

            if (t.allBox[numQuestion][6] == "\r")
            {
                labelC.transform.parent.gameObject.SetActive(false);
                labelD.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                labelC.transform.parent.gameObject.SetActive(true);
                labelC.text = t.allBox[numQuestion][6];

                if (t.allBox[numQuestion][7] == "\r")
                    labelD.transform.parent.gameObject.SetActive(false);
                else
                {
                    labelD.transform.parent.gameObject.SetActive(true);
                    labelD.text = t.allBox[numQuestion][7];
                }
            }
         }
    }

    private void Start()
    {
        StartCoroutine(StartFirstQuestion(0.01f));
    }

  
    private IEnumerator LoadNextQuestion (float time)//По завершении анимации
    {
        yield return new WaitForSeconds(time);
        //Debug.LogWarning("LoadNextQuestion");
        labelA.text = "";
        labelB.text = "";
        labelC.text = "";
        labelD.text = "";
        labelQuetion.text = "";
        //NumQuestion++;//text
        PreloadScene();
        render3D.Play = true;
    }

    private IEnumerator ShowLoad(float time)//По завершении анимации
    {
        yield return new WaitForSeconds(time);
        load.SetActive(true);
    }

    private void PreloadScene()
    {
        instance2 = instance;
        if (Resources.Load<GameObject>("Prefs/" + t.allBox[NumQuestion+1][0].Substring(0, t.allBox[NumQuestion+1][0].Length - 1)) == null)
        {
            instance = Instantiate(Resources.Load<GameObject>("Prefs/1.1.")) as GameObject;
            Debug.LogWarning("Resources was not found path: Prefs/" + t.allBox[NumQuestion+1][0].Substring(0, t.allBox[NumQuestion+1][0].Length - 1));
        }
        else
        {
            instance = Instantiate(Resources.Load<GameObject>("Prefs/" + t.allBox[NumQuestion+1][0].Substring(0, t.allBox[NumQuestion+1][0].Length - 1))) as GameObject;
            //Debug.LogWarning("Resources in path Prefs/" + t.allBox[NumQuestion][0].Substring(0, t.allBox[NumQuestion][0].Length - 1) + " load sucessfull");
        }

        SetCamera();
        if (render3D.Cam1Left)
            SetLayer(instance, 10);
        else
            SetLayer(instance, 9);
    }

    public void ButtonsActivate(bool value)
    {
        labelA.transform.parent.gameObject.GetComponent<UIButton>().isEnabled = value;
        labelB.transform.parent.gameObject.GetComponent<UIButton>().isEnabled = value;
        labelC.transform.parent.gameObject.GetComponent<UIButton>().isEnabled = value;
        labelD.transform.parent.gameObject.GetComponent<UIButton>().isEnabled = value;
        Debug.Log("ButtonsActivate "+value);
        if (value)
        {
            //labelA.transform.parent.GetComponent<TweenColor>().from = Color.green;
            labelA.transform.parent.GetComponent<TweenColor>().to = Color.white;
            labelA.transform.parent.GetComponent<UIButton>().defaultColor = Color.white;

            labelB.transform.parent.GetComponent<TweenColor>().to = Color.white;
            labelB.transform.parent.GetComponent<UIButton>().defaultColor = Color.white;

            if (labelC.transform.parent.GetComponent<TweenColor>() != null)
                labelC.transform.parent.GetComponent<TweenColor>().to = Color.white;
            labelC.transform.parent.GetComponent<UIButton>().defaultColor = Color.white;

            if (labelD.transform.parent.GetComponent<TweenColor>() != null)
                labelD.transform.parent.GetComponent<TweenColor>().to = Color.white;
            labelD.transform.parent.GetComponent<UIButton>().defaultColor = Color.white;
            //Debug.LogWarning("Color white");
        }
        
    }

    public void UnloadPref()
    {
        Destroy(instance2);
        Resources.UnloadUnusedAssets();
        
    }

    private void SetCamera()
    {
        if (instance != null)
        {
            Camera cam = instance.GetComponentInChildren<Camera>();
            if (cam != null)
            {
                //cam.rect = new Rect(0.04f, 0.59f, 0.92f, 0.22f);
                if (render3D.Cam1Left)
                {
                    render3D.camera1 = cam;
                    render3D.camera1.rect = new Rect(1.04f, 0.59f, 0.92f, 0.22f);
                }
                else
                {
                    render3D.camera2 = cam;
                    render3D.camera2.rect = new Rect(1.04f, 0.59f, 0.92f, 0.22f);
                }

                render3D.Cam1Left = !render3D.Cam1Left;
            }
            else
                Debug.LogWarning("Camera was not founded in prefab");

        }
        else
            Debug.LogWarning("instance was not founded in prefab");
    }

    private IEnumerator StartFirstQuestion(float time)
    {
        yield return new WaitForSeconds(time);
        t = GameObject.Find("TextController(Clone)").GetComponent<Text>();
        NumQuestion = 1;
        //NumQuestion = 1;
        //Debug.LogWarning("Prefs/" + t.allBox[NumQuestion][0]);
        //Debug.LogWarning("L" + t.allBox[NumQuestion][0].Length);
        instance = Instantiate(Resources.Load<GameObject>("Prefs/" + t.allBox[NumQuestion][0].Substring(0, t.allBox[NumQuestion][0].Length - 1))) as GameObject;
        SetLayer(instance, 9);
        Camera cam = instance.GetComponentInChildren<Camera>();
        if (cam != null)
        {
            //cam.rect = new Rect(0.04f, 0.59f, 0.92f, 0.22f);
            render3D.camera1 = cam;
            render3D.camera1.rect = new Rect(0.04f, 0.59f, 0.92f, 0.22f);
        }
    }

    private void SetLayer(GameObject inst, int layer)
    {
        if (inst != null)
        {
            inst.layer = layer;
            Transform[] ts = inst.GetComponentsInChildren<Transform>();
            foreach (Transform to in ts)
            {
                to.gameObject.layer = layer;
            }

            Light[] ls = inst.GetComponentsInChildren<Light>();
            foreach (Light l in ls)
            {
                l.cullingMask = 1 << layer;
                l.gameObject.SetActive(false);
                l.gameObject.SetActive(true);
            }

            Camera[] cams = inst.GetComponentsInChildren<Camera>();
            foreach (Camera c in cams)
            {
                c.cullingMask = 1 << layer;
                AudioListener al = c.GetComponent<AudioListener>();
                if (al != null)
                    Destroy(al);
            }

            Projector[] prs = inst.GetComponentsInChildren<Projector>();
            foreach (Projector p in prs)
            {
                p.gameObject.SetActive(false);
                p.gameObject.SetActive(true);
            }
            
        }
    }
}
