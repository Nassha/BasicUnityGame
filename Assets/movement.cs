using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class movement : MonoBehaviour

{
    public controller2D control;
    public Animator anime;
    public float Pspeed=50f;
    float Hmove=0f;
    bool isJump=false;
    public Text text1;

   
    void Start()
    {
        text1 = GameObject.FindWithTag("mytxt").GetComponent<Text>();

    }


    void Update()
    {
	Hmove=Input.GetAxisRaw("Horizontal")*Pspeed;
	anime.SetFloat("move",Mathf.Abs(Hmove));
        if (Input.GetButtonDown("Jump"))
        {
        	isJump=true;
		anime.SetBool("checkJump",true);
        }

    }

    void FixedUpdate()
    {
	control.Move(Hmove*Time.fixedDeltaTime,isJump);
	isJump=false;
    }
    public void Landing()
    {
	anime.SetBool("checkJump",false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("items"))
        {
	    StartCoroutine(storeCollectedItem("http://localhost/unity/collectData.php","1"));
            Destroy(other.gameObject);
        }
	StartCoroutine(retrieveCollectedItems("http://localhost/unity/FetchData.php"));
    }
    
    
    IEnumerator storeCollectedItem(string url, string items)
{
    WWWForm form = new WWWForm();
    form.AddField("items", items);

    UnityWebRequest uwr = UnityWebRequest.Post(url, form);
    yield return uwr.SendWebRequest();

    if (uwr.result == UnityWebRequest.Result.ConnectionError)
    {
        Debug.Log("Error While Sending: " + uwr.error);
    }
    else
    {
        Debug.Log("Received: " + uwr.downloadHandler.text);
       // text1.text=uwr.downloadHandler.text;
    }
}


IEnumerator retrieveCollectedItems(string url)
{
    WWWForm form = new WWWForm();
    UnityWebRequest uwr = UnityWebRequest.Post(url, form);
    yield return uwr.SendWebRequest();

    if (uwr.result == UnityWebRequest.Result.ConnectionError)
    {
        Debug.Log("Error While Sending: " + uwr.error);
    }
    else
    {
        Debug.Log("Received: " + uwr.downloadHandler.text);
        text1.text="Attack Potions: "+ uwr.downloadHandler.text;
    }
}


}
