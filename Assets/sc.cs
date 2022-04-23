using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;
public class sc : MonoBehaviour
{
    // Start is called before the first frame update
   public TextMeshProUGUI topscore;
    string filepath;
    private List<int> ReadFile()
    {
        using (StreamReader reader = File.OpenText(filepath))
        {

            List<int> numbers = new List<int>();

            while (true)
            {
                string FindMax = reader.ReadLine();
                if (FindMax == null)
                {
                    break;
                }
                int test;
                if (Int32.TryParse(FindMax, out test))
                {
                    numbers.Add(test);
                }
            }
            // Console.WriteLine("the highest number is {0}", numbers.Max());
            int len = numbers.Count;
            List<int> num = new List<int>();
            if (len<10)
            {
            for (int i=0;i<len;i++)
                {
                    num.Add(numbers[i]);
                } 
            }
            else
            {
             for (int i=len-10;i<len;i++)
                {
                    num.Add(numbers[i]);
                }   
            }
            num.Sort((a, b) => b.CompareTo(a));
            return num;
        }
    }

    public void Start1()
    { 
    	
      filepath=Application.persistentDataPath+ "/file1.txt";
      
      if(!File.Exists(filepath))
      {
      	File.Create(filepath).Close();
      }
        // Save();
        List<int> numbers = new List<int>();
        numbers = ReadFile();
        string s = "Score ";
        for(int i=0;i<numbers.Count;i++)
        {
            Debug.Log(i);
            s=s+numbers[i].ToString()+"|";
           // scores[i].text=numbers[i].ToString();
        }
        topscore.SetText(s);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
