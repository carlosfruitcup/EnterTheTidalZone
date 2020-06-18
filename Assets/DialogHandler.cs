using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
/// <summary>This is used in Global, it is used for triggering dialog boxes from anywhere.
/// <para>This is a MonoBehaviour class. </para>
/// <seealso cref="GlobalVariables"/>
/// </summary>
public class DialogHandler : MonoBehaviour

{
	public bool goBusy = false;
	public bool dontDissapear = false;
	public Vector3[] xyValues;
	public bool startAfterFadeIn = false;
	public string[] dialogue;
	public Sprite[] icons;
	public UnityEvent[] sequentialEvents;
	public AudioClip popUp;
	public AudioClip popDown;
	private GameObject nextButton;
	private Image iconImage;
	private TextMeshProUGUI textObject;
	private Vector3 originalPosition;
	private bool started = false;
	private bool started2 = false;
	private bool goBack = false;
	private bool goBack2 = false;
	private int currentDialog = 0;
	private AudioSource snd;
    public float duration = 1f;
	public float duration2 = 0.5f;
    private float startTime;
	private Coroutine typing;
	public UnityEvent finished;
	void Awake()
	{
		nextButton = transform.Find("Next").gameObject;
		iconImage = transform.Find("Icon").GetComponent<Image>();
		textObject = transform.Find("Text").GetComponent<TextMeshProUGUI>();
		snd = transform.GetComponent<AudioSource>();
		originalPosition = transform.position;
	}
	// Start is called before the first frame update
	void Start()
	{
		textObject.text = "";
		iconImage.sprite = icons[currentDialog];
		transform.position = new Vector3(transform.position.x,-363.5f,transform.position.z);
	   	if(startAfterFadeIn == true)
	   	{
			StartCoroutine(FadeThenTrigger());
	   	}
	}
	void Update()
	{
		if(started)
		{
			float t = (Time.time - startTime) / duration;
			transform.position = new Vector3(transform.position.x,Mathf.SmoothStep(-363.5f,originalPosition.y+22,t),transform.position.z);
			if(transform.position.y == originalPosition.y+22)
			{
				started = false;
				startTime = Time.time;
				started2 = true;
			}
		}
		if(started2)
		{
			float t = (Time.time - startTime) / duration2;
			transform.position = new Vector3(transform.position.x,Mathf.SmoothStep(originalPosition.y+22,originalPosition.y,t),transform.position.z);
			if(transform.position.y == originalPosition.y)
			{
				started2 = false;
			}
		}
		if(goBack)
		{
			float t = (Time.time - startTime) / duration2;
			transform.position = new Vector3(transform.position.x,Mathf.SmoothStep(originalPosition.y,originalPosition.y+22,t),transform.position.z);
			if(transform.position.y == originalPosition.y+22)
			{
				goBack = false;
				startTime = Time.time;
				goBack2 = true;
			}
		}
		if(goBack2)
		{
			float t = (Time.time - startTime) / duration;
			transform.position = new Vector3(transform.position.x,Mathf.SmoothStep(originalPosition.y+22,-363.5f,t),transform.position.z);
			if(transform.position.y == -363.5f)
			{
				goBack2 = false;
			}
		}
	}
	public void Next()
	{
		Debug.Log(transform.position);
		if(sequentialEvents.Length > currentDialog)
		{
			sequentialEvents[currentDialog].Invoke();
		}
		else
		{
			Debug.Log("something was not data for dialog!");
		}
		if(dialogue.Length-1 > currentDialog)
		{
			snd.clip = popUp;
			snd.Play();
			textObject.text = "";
			startTime = Time.time;
			started = true;
			currentDialog += 1;
			originalPosition = xyValues[currentDialog];
			transform.position = new Vector3(xyValues[currentDialog].x,-363.5f,xyValues[currentDialog].z);
			iconImage.sprite = icons[currentDialog];
			StopCoroutine(typing);
			typing = StartCoroutine(TypeWriter(dialogue[currentDialog]));
		}
		else
		{
			if(!dontDissapear) CloseDialogue();
			finished.Invoke();
		}
	}
	public void TriggerDialogue()
	{
		TriggerDialogue(0);
	}

	public void TriggerDialogue(int dialogToStart)
	{
		currentDialog = dialogToStart;
		if(goBusy)
			GlobalVariables.global.busy = true;
		snd.clip = popUp;
		snd.Play();
		startTime = Time.time;
		started = true;
		textObject.text = "";
		typing = StartCoroutine(TypeWriter(dialogue[currentDialog]));
	}
	public void CloseDialogue()
	{
		snd.clip = popDown;
		snd.Play();
		startTime = Time.time;
		StopCoroutine(typing);
		goBack = true;
		if(goBusy)
			GlobalVariables.global.busy = false;
	}
	private IEnumerator FadeThenTrigger()
	{
		yield return new WaitForSeconds(0.5f);
		TriggerDialogue();
	}
	private IEnumerator TypeWriter(string text)
	{
		foreach (char c in text) 
		{
			textObject.text += c;
			yield return new WaitForSeconds (0.025f);
		}
	}
}
