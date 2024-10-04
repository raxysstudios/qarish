using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Monolog", menuName = "Subtitles&Sound Data", order = 51)]
public class CharacterDialogue : ScriptableObject
 
{
    public string[] monolog;
    public AudioBehaviour voice;

    private void ManagerMonolog()
    {
        if (Input.GetKey(KeyCode.E))
        {
            foreach (var mono in monolog)
            {
                Debug.Log("Linear Monologues");
            }
        }
    }
    
}
