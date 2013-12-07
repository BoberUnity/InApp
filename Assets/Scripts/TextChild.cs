using System;
using System.Collections;
using UnityEngine;

public class TextChild : MonoBehaviour
{
    //[SerializeField] private TextAsset textAsset = null;
    [SerializeField] private Render3D render3D = null;
    [SerializeField] private UILabel labelQuetion = null;
    [SerializeField] private UILabel labelA = null;
    [SerializeField] private UILabel labelB = null;
    [SerializeField] private UILabel labelC = null;
    [SerializeField] private UILabel labelD = null;
    //[SerializeField] private string[] allBox0;
    //[SerializeField] private string[] allBox1;
    //[SerializeField] private string[] linesTemp;
    //[SerializeField] private string[][] allBox;
    [SerializeField] private Text t = null;//
    private int numQuestion = 0;
    private int currQuestion = 0;//Ответ, который дал юзер
    private GameObject instance = null;
    private GameObject instance2 = null;
    
    //private bool instance1Old = false;
   
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

    private bool first = true;
    private int stlb = 0;

    private void Start()
    {
        t = GameObject.Find("TextController(Clone)").GetComponent<Text>();
        NumQuestion = 1;

        StartCoroutine(StartFirstQuestion(0.02f));
    }

  //private void Start()
  //{
  //    int numStr = 0;//vo vsem doke
  //    var allText = textAsset.text;//File.ReadAllLines(textAsset);

  //    for (int i = 0; i < allText.Length; i++)
  //    {
  //        if (allText.Substring(i, 1) == "\n")
  //            numStr++;//count all lines
  //        else
  //        {
  //            linesTemp[numStr] += allText.Substring(i, 1);
  //        }
  //    }
  //    Array.Resize(ref allBox, numStr);
      
  //    for (int i = 0; i < numStr; i++)
  //    {
  //        Array.Resize(ref allBox[i], 9);
          
  //        for (int sm = 0; sm < linesTemp[i].Length; sm++)
  //        {
  //            if (linesTemp[i].Substring(sm, 1) == "'")
  //            {
  //               first = !first;
  //               if (first)
  //               {
  //                   stlb = Mathf.Min(7, stlb += 1);
  //               }
  //            }
  //            else
  //            {
  //                allBox[i][stlb] += linesTemp[i].Substring(sm, 1);
  //                if (allBox[i][stlb].Length == 1 && allBox[i][stlb] == ",")//Uberem razdelitel ","
  //                {
  //                    allBox[i][stlb] = "";
  //                }
  //                //correct 2,
  //                if (allBox[i][stlb].Length == 2)
  //                {
  //                    if (allBox[i][stlb] == "1," || allBox[i][stlb] == "2," || allBox[i][stlb] == "3," || allBox[i][stlb] == "4,")
  //                    {
  //                        allBox[i][stlb] = allBox[i][stlb].Substring(0, 1);
  //                        stlb += 1;
  //                    }
  //                }
  //            }
  //        }
  //        stlb = 0;
  //    }

  //    allBox0 = allBox[0];
  //    allBox1 = allBox[2];
  //}

    private IEnumerator LoadNextQuestion (float time)
    {
        yield return new WaitForSeconds(time);
        Debug.LogWarning("LoadNextQuestion");
        
        ButtonsActivate(true);
        NumQuestion++;

        instance2 = instance;
        if (Resources.Load<GameObject>("Prefs/" + t.allBox[NumQuestion][0].Substring(0, t.allBox[NumQuestion][0].Length - 1)) == null)
        {
             instance = Instantiate(Resources.Load<GameObject>("Prefs/1.1.")) as GameObject;
             Debug.LogWarning("Resources = null in path Prefs/" + t.allBox[NumQuestion][0].Substring(0, t.allBox[NumQuestion][0].Length - 1));
        }
        else
        {
            instance = Instantiate(Resources.Load<GameObject>("Prefs/" + t.allBox[NumQuestion][0].Substring(0, t.allBox[NumQuestion][0].Length - 1))) as GameObject;
            Debug.LogWarning("Resources in path Prefs/" + t.allBox[NumQuestion][0].Substring(0, t.allBox[NumQuestion][0].Length - 1) + " load sucessfull");
        }
        render3D.Play = true;
        LoadPref();

    }

    public void ButtonsActivate(bool value)
    {
        labelA.transform.parent.gameObject.GetComponent<UIButton>().enabled = value;
        labelB.transform.parent.gameObject.GetComponent<UIButton>().enabled = value;
        labelC.transform.parent.gameObject.GetComponent<UIButton>().enabled = value;
        labelD.transform.parent.gameObject.GetComponent<UIButton>().enabled = value;
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
            Debug.LogWarning("Color white");
        }
        
    }

    public void UnloadPref()
    {
        Destroy(instance2);
        Resources.UnloadUnusedAssets();
    }

    private void LoadPref()
    {
        if (instance != null)
        {

            Camera cam = instance.GetComponentInChildren<Camera>();
            if (cam != null)
            {
                cam.rect = new Rect(0.04f, 0.59f, 0.92f, 0.22f);
                render3D.camera2 = render3D.camera1;
                render3D.camera1 = cam;
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
        //NumQuestion = 1;
        Debug.LogWarning("Prefs/" + t.allBox[NumQuestion][0]);
        Debug.LogWarning("L" + t.allBox[NumQuestion][0].Length);
        instance = Instantiate(Resources.Load<GameObject>("Prefs/" + t.allBox[NumQuestion][0].Substring(0, t.allBox[NumQuestion][0].Length - 1))) as GameObject;
        LoadPref();
    }
}
