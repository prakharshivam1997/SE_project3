using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using System.IO;
using UnityEngine.UI;
public class hscore : MonoBehaviour
{
    public Text[] scores;
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

    void Start()
    {   filepath=Application.persistentDataPath+ "/file1.txt";
        // Save();
        List<int> numbers = new List<int>();
        numbers = ReadFile();
        for(int i=0;i<numbers.Count;i++)
        {
            Debug.Log(i);
            scores[i].text=numbers[i].ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
