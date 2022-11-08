using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueController : MonoBehaviour 
{
    public float minTalkDistance;
    public Transform target;
    public List<string> talkingStrings;
    public GameObject dialogueScreen;
    public TMP_Text textToModify;

    private int talkTextIndex;
    private bool showingDialgoue;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minTalkDistance);
    }

	// Use this for initialization
	void Start () 
    {
        talkTextIndex = 0;
        showingDialgoue = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if( target )
        {
            if( IsWithinDistance( minTalkDistance ) )
            {
                DetectInput();
            }
            else if( showingDialgoue )
            {
                EndDialogue();
            }
        }
	}

    private void DetectInput()
    {
        if( Input.GetKeyUp( KeyCode.E ) )
        {
            if( showingDialgoue == false )
            {
                BeginDialogue();
            }
            else if( talkTextIndex < talkingStrings.Count - 1 )
            {
                ProgressDialogue();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    private void BeginDialogue()
    {
        showingDialgoue = true;

        talkTextIndex = 0;
        textToModify.text = talkingStrings[talkTextIndex];

        dialogueScreen.SetActive(true);
    }

    private void ProgressDialogue()
    {
        talkTextIndex++;
        textToModify.text = talkingStrings[talkTextIndex];
    }

    private void EndDialogue()
    {
        showingDialgoue = false;
        dialogueScreen.SetActive(false);
    }

    public virtual bool IsWithinDistance(float distance)
    {
        return (GetDirection().magnitude < distance);
    }

    public virtual Vector3 GetDirection()
    {
        return target.position - transform.position;
    }
}
