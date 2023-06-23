using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;

public class movement_sqlLite : MonoBehaviour

{
    public controller2D control;
    public Animator anime;
    public float Pspeed=50f;
    float Hmove=0f;
    bool isJump=false;
    public Text text1;
    public IDbConnection dbcon;
    public IDbCommand dbcmd;

   
    void Start()
    {
        text1 = GameObject.FindWithTag("mytxt").GetComponent<Text>();
        SqliteSetup();

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
	    IDbCommand cmnd = dbcon.CreateCommand();
	    cmnd.CommandText = "INSERT INTO tbl (items) VALUES (1)";
	    cmnd.ExecuteNonQuery();
            Destroy(other.gameObject);
        }
	IDbCommand cmnd_read = dbcon.CreateCommand();
		IDataReader reader;
		string query ="SELECT sum(items) FROM tbl";
		cmnd_read.CommandText = query;
		reader = cmnd_read.ExecuteReader();

		while (reader.Read())
		{
			Debug.Log("sum of items: " + reader[0].ToString());
                        text1.text = "Attack Potions: " + reader[0].ToString() ;
                       
		}
    }
    
    void SqliteSetup()
    {

                // Create database
		string connection = "URI=file:" + Application.persistentDataPath + "/" + "dbs";
		
		// Open connection
		dbcon = new SqliteConnection(connection);
		dbcon.Open();

		// Create table
		dbcmd = dbcon.CreateCommand();
		string q_createTable = "CREATE TABLE IF NOT EXISTS tbl (items INTEGER )";
		
		dbcmd.CommandText = q_createTable;
		dbcmd.ExecuteReader();
    }

  

}
