using System;
using UnityEngine;

public class Text : MonoBehaviour
{
    [SerializeField] private TextAsset textAsset = null;
    [SerializeField] private int numStr = 0;//vo vsem doke
    [SerializeField] private UILabel labelQuetion = null;
    [SerializeField] private UILabel labelA = null;
    [SerializeField] private UILabel labelB = null;
    [SerializeField] private UILabel labelC = null;
    [SerializeField] private UILabel labelD = null;
    [SerializeField] private string[] allBox0;
    [SerializeField] private string[] allBox1;
    [SerializeField] private string[] linesTemp;
    [SerializeField] private string[][] allBox;
    private int numQuestion = 1;

    public int NumQuestion
    {
        get { return numQuestion;}
        set { 
                numQuestion = value;
                labelQuetion.text = allBox[numQuestion][1];
                labelA.text = allBox[numQuestion][4];
                labelB.text = allBox[numQuestion][5];

                if (allBox[numQuestion][6].Length == 1)
                    labelC.transform.parent.gameObject.SetActive(false);
                else
                {
                    labelC.transform.parent.gameObject.SetActive(true);
                    labelC.text = allBox[numQuestion][6];
                }

                if (allBox[numQuestion][7].Length == 1)
                    labelD.transform.parent.gameObject.SetActive(false);
                else
                {
                    labelD.transform.parent.gameObject.SetActive(true);
                    labelD.text = allBox[numQuestion][7];
                }

            }
    }

    private bool first = true;
    private int stlb = 0;

  private void Start()
  {
      
      var allText = textAsset.text;//File.ReadAllLines(textAsset);
      //len = allText.Length;
      //var lin = 0;
      for (int i = 0; i < allText.Length; i++)
      {
          if (allText.Substring(i, 1) == "\n")
              numStr++;//count all lines
          else
          {
              linesTemp[numStr] += allText.Substring(i, 1);
          }
      }
      Array.Resize(ref allBox, numStr);
      for (int i = 0; i < numStr; i++)
      {
          Array.Resize(ref allBox[i], 9);
          
          for (int sm = 0; sm < linesTemp[i].Length; sm++)
          {
              if (linesTemp[i].Substring(sm, 1) == "'")
              {
                 first = !first;
                 if (first)
                     stlb = Mathf.Min(7, stlb += 1);
              }
              else
              {
                  allBox[i][stlb] += linesTemp[i].Substring(sm, 1);
                  if (allBox[i][stlb].Length == 1 && allBox[i][stlb] == ",")//Uberem razdelitel ","
                  {
                      allBox[i][stlb] = "";
                  }
                  //correct 2,
                  if (allBox[i][stlb].Length == 2)
                  {
                      if (allBox[i][stlb] == "1," || allBox[i][stlb] == "2," || allBox[i][stlb] == "3," || allBox[i][stlb] == "4,")
                      {
                          allBox[i][stlb] = allBox[i][stlb].Substring(0, 1);
                          stlb += 1;
                      }
                  }
              }
          }
          stlb = 0;
      }

      allBox0 = allBox[0];
      allBox1 = allBox[1];
      

      
  }
}
