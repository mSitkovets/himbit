using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;
    [SerializeField] private string emotion;

    // getters
    public string[] Dialogue => dialogue;

    public Response[] Responses => responses;

    public string Emotion => emotion;

    public bool HasResponses => Responses != null && Responses.Length > 0; 
}
