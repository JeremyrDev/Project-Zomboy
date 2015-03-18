using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class generateNames : MonoBehaviour 
{
    public Text Names;
    string[] c = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z" };
    string[] v = { "a", "e", "i", "o", "u", "y" };
    int cycle = 0;
    int numberOfLetters = 4;
    string currentLetter;
    string word;
    string previousLetter;
    bool vowel = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Names.text = Generate()+"\n";
        }
    }

    public string Generate()
    {
        word = "";
        print("Generating");
        numberOfLetters = Random.Range(3, 8);

        int vc = Random.Range(0, 5);
        if (vc != 1)
            vowel = true;
        else
            vowel = false;

        while(cycle < numberOfLetters)
        {
            if(vowel)
            {
                currentLetter = c[Random.Range(0, 19)];
                vowel = false;
            }
            else
            {
                currentLetter = v[Random.Range(0, 6)];
                if(currentLetter == "y")
                    currentLetter = v[Random.Range(0, 6)];
                vowel = true;
            }
            word += currentLetter;
            cycle++;
            print(word);
        }
        cycle = 0;
        return word;
    }
}
