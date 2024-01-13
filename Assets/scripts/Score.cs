using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{

    private int score;
    
    // as far as I'm aware, there is no way to do this in the untiy editor directly
    [SerializeField] TextMeshProUGUI scoreTextMesh;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public void IncreaseScore()
    {
        score++;
        scoreTextMesh.text = "Score: " + score;
    }
}
