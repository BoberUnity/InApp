using System.IO;
using UnityEngine;

public class Text : MonoBehaviour
{
    [SerializeField] private TextAsset textAsset = null;
    [SerializeField] private int str = 0;
    [SerializeField]
    private int len = 0;
    [SerializeField] private string[] lines;
    [SerializeField] private string[][] allBox;
    [SerializeField] private string[] line1;

  private void Start()
  {
      var allText = textAsset.text;//File.ReadAllLines(textAsset);
      len = allText.Length;
      for (int i = 0; i < allText.Length; i++)
      {
          if (allText.Substring(i, 1) ==  "'" )
          {
              str += 1;
          }
          else
          {
              if (str <700)lines[str] += allText.Substring(i, 1);
          }
      }

      //for (int i = 1; i < lines[0].Length; i++)
      //{
      //    if (lines[0].Substring(i, 1) == "")
      //    {
              
      //    }
      //    line1[0] += lines[0].Substring(i, 1);
      //}

  }
}
