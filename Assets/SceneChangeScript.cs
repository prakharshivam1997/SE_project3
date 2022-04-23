using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
using System.IO;
using System;
public class SceneChangeScript : MonoBehaviour{
	public void PlayScene() 
	{  
		SceneManager.LoadScene("PlayScene");  
	}
    public void exitgame() {  
        Debug.Log("exitgame");  
        Application.Quit();  
    }
    public void readTextFile()
	{
   	string file_path = "Assets/file1.txt";
   	StreamReader inp_stm = new StreamReader(file_path);

  	while(!inp_stm.EndOfStream)
   	{
       	string inp_ln = inp_stm.ReadLine();
       	// Do Something with the input.
       	Debug.Log(inp_ln);
   	}

   	inp_stm.Close( );
   	}
}
