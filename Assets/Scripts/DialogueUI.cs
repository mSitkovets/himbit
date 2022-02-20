using System.Collections; 
using UnityEngine;
using TMPro;
using System.Linq;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel; 
    [SerializeField] private DialogueObject Dialogue;

    // fix bug
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseBox;

    private TypewriterEffect typewriterEffect; 
    private ResponseHandler responseHandler;

    private GameObject tim;
    private GameObject hallway_background;
    private GameObject hims_background;
    public Sprite neutral;
    public Sprite happy;
    public Sprite sad;
    public Sprite angy;

    private void Start(){
        tim = GameObject.FindGameObjectWithTag("tim");
        tim.SetActive(false);

        hallway_background = GameObject.FindGameObjectWithTag("hall");
        hallway_background.SetActive(true);
        hims_background = GameObject.FindGameObjectWithTag("hims");
        hims_background.SetActive(false);

        responseButtonTemplate.gameObject.SetActive(false);
        responseBox.gameObject.SetActive(false);
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
        StartCoroutine("GameStart");
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(5f);
        ShowDialogue(Dialogue);
        yield return null;
    }

    public void ShowDialogue(DialogueObject dialogueObject){
        dialogueBox.SetActive(true);
        if (dialogueObject.Emotion.Equals("Neutral"))
        {
            //set sprite to neutral
            tim.GetComponent<SpriteRenderer>().sprite = neutral;
        }
        else if (dialogueObject.Emotion.Equals("Happy"))
        {
            //set sprite to happy
            tim.GetComponent<SpriteRenderer>().sprite = happy;
            //effect?
        }
        else if (dialogueObject.Emotion.Equals("Sad"))
        {
            //set sprite to sad
            tim.GetComponent<SpriteRenderer>().sprite = sad;
            //effect?
        }
        else if (dialogueObject.Emotion.Equals("Angry"))
        {
            //set sprite to angry
            tim.GetComponent<SpriteRenderer>().sprite = angy;
            //effect?
        }
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject){
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++){
            string dialogue = dialogueObject.Dialogue[i];

            if (dialogue.Contains("!Him Tortons")){
                //change background from hallway to him tortons
                hallway_background.SetActive(false);
                hims_background.SetActive(true);
                Debug.Log("hims");
            }
            //dialogue trigger to make tim appear
             if (dialogue[0].Equals(';'))
            {
                tim.SetActive(true);
                dialogue = dialogue.Remove(0, 1);
            }
             //dialogue trigger to make tim leave
             if (dialogue[0].Equals('*'))
            {
                tim.SetActive(false);
                dialogue = dialogue.Remove(0, 1);
            }
             //dialogue italics for "internal thought" check
            if (dialogue[0].Equals('!'))
            {
                textLabel.fontStyle = FontStyles.Italic;
                dialogue = dialogue.Remove(0, 1);
            }
            //removes italics when not in "internal thought"
            else
            {
                textLabel.fontStyle = FontStyles.Normal;
            }
            yield return typewriterEffect.Run(dialogue, textLabel);
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        if (dialogueObject.HasResponses){
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox(); 
        }
    }

    private void CloseDialogueBox(){
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
