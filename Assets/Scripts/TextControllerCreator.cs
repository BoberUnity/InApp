using UnityEngine;

public class TextControllerCreator : MonoBehaviour{    [SerializeField] private GameObject textControllerPref = null;  private void Start ()  {
      if (GameObject.Find("TextController(Clone)") == null)
          Instantiate(textControllerPref, transform.position, Quaternion.identity);  }}