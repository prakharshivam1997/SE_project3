using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement; 
public class GameScript : MonoBehaviour
{   public GameObject ball;
    public GameObject[] scoreboxes;
    public int round,ball_no;
    
    public AudioSource asource;
    public AudioClip aclip;
    string file_path = "Assets/file1.txt";

    int score = 0,sum=0, flag = 0;
    float time = 0;
    GameObject[] pins;
    Vector3[] rotationVector;
    Vector3[] positions;
    bool mutex=true;

    int CompareObNames( GameObject x, GameObject y )
    {
        return x.name.CompareTo( y.name );
    }

    // Start is called before the first frame update
    void Start()
    {   
        round = 0;
        ball_no = 0;
        pins = GameObject.FindGameObjectsWithTag("pins");
        Array.Sort(pins, CompareObNames);
        scoreboxes = GameObject.FindGameObjectsWithTag("scoreboxes");
        Array.Sort(scoreboxes, CompareObNames);
        for (int i = 0 ; i < scoreboxes.Length-1 ; i++){
            scoreboxes[i].GetComponent<Text>().gameObject.SetActive(false);
        }
        scoreboxes[20].GetComponent<Text>().text = sum.ToString();
        // using (System.IO.StreamWriter sw = System.IO.File.AppendText(file_path))
        // {                                                
        //     sw.WriteLine(sum.ToString());
        // }

        positions = new Vector3[pins.Length];
        rotationVector = new Vector3[pins.Length];
        for(int i = 0 ; i <pins.Length; i++){
            positions[i] = pins[i].transform.position ;
            rotationVector[i] =  pins[i].transform.rotation.eulerAngles; 
            // rotation Vector .z = 0; 
            // transform.rotation = Quaternion.Euler(rotationVector[i]);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
    	if(other.gameObject.tag == "pins"){
            asource.PlayOneShot(aclip);
        }
        if(other.gameObject.name == "PinsCollider" || other.gameObject.name == "Gutter")
        {
            Debug.Log("Here "+other.gameObject.name);
            if(mutex)
                StartCoroutine(logic(4));
        }
    }
    IEnumerator logic(float waitTime)
    {
        mutex=false;
        scoreboxes[ball_no].GetComponent<Text>().text = score.ToString();
        scoreboxes[ball_no].GetComponent<Text>().gameObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        Debug.Log(ball_no);
        
        // if(flag == 0 || time < 5){
            CountPinsDown();
            yield return new WaitForSeconds(2);
        // }else{
            flag = 0;
            time = 0;
            ball_no++;
            if(ball_no%2 == 1 && score == 10){
                ball_no++;
            }
            sum += score;
            scoreboxes[20].GetComponent<Text>().text = sum.ToString();
            ResetPins();
        // }
        mutex=true;
    }
    // Update is called once per frame
    void Update()
    {   
    }

    void MoveBall()
    {
        Vector3 position = ball.transform.position;
        position += Vector3.up * Input.GetAxis("Horizontal") * Time.deltaTime;
        position.z = Mathf.Clamp(position.z,-4f,4f);
        ball.transform.position = position;
        ball.transform.Translate(Vector3.up * Input.GetAxis("Horizontal") * Time.deltaTime);
    }

    void CountPinsDown(){
        Vector3 position = ball.transform.position;
        score=0;
        for (int i=0; i < pins.Length ; i++){
            if ((pins[i].transform.eulerAngles.z > 5 && pins[i].transform.eulerAngles.z < 355) && pins[i].activeSelf){
                score++;
                pins[i].SetActive(false);
            }
        }
        scoreboxes[ball_no].GetComponent<Text>().text = score.ToString();
        // if(score != 0){
        //     time += Time.deltaTime;
        //     Debug.Log(time);
        // }
        
        Debug.Log(score);
        // if(position.y < -1 ){
        //     flag = 1;
        // }
        
    }

    void ResetPins(){
        score =0;
        if(ball_no%2 == 0){
            for(int i = 0 ; i <pins.Length; i++){
                pins[i].SetActive(true);
                pins[i].transform.position = positions[i];
                pins[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                pins[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                pins[i].transform.rotation = Quaternion.Euler(rotationVector[i]);
            }
        }
        ball.transform.position = new Vector3(-23f,1f,0f);
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        ball.transform.rotation = Quaternion.identity;

        if (ball_no == 20){
         string file_path=Application.persistentDataPath+ "/file1.txt";
         using (System.IO.StreamWriter sw = System.IO.File.AppendText(file_path))
         {                                                
             sw.WriteLine(sum.ToString());
         }
         SceneManager.LoadScene("MainMenu");
            // Add to top 10 scores
            // Scene change to main menu
        } 
    }
}
