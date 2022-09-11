using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using System.Linq;


public class BonePlacement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject []bone;
    public Transform []boneHolder;
    [SerializeField]
    private GameObject []boneClone;
    public GameObject silouette;
    private GameObject current;
    private bool state = true;

    public QuizManager quizM;

    public Text currentBoneName;
    private string []boneName = {"Skull","RibCage","Femur","Femur","Clavical","Clavical","Finger","Finger","Foot","Foot","Humerus","Humerus","Sternum","Ulna","Ulna","Vertebrate","Pelvis","Tibia","Tibia","Scapula","Scapula","Patella","Patella"};

    private int []left = {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22};

    private int []done = {};

    private int currentIndex;
    private HaddiScript haddi;
    

    void Start()
    {
        boneHolder = silouette.GetComponentsInChildren<Transform>();
        
        boneClone = new GameObject[bone.Length];
        for(int i=0;i<bone.Length;i++){
            boneClone[i] = Instantiate(bone[i],boneHolder[i+1].position,boneHolder[i+1].rotation);
            BoneDragScript bs = boneClone[i].GetComponentInChildren<BoneDragScript>();
            bs.enabled = false;
        }
        haddi=FindObjectOfType<HaddiScript>().GetComponent<HaddiScript>();
    }

    private static readonly Random getrandom = new Random();

    public static int GetRandomNumber(int min, int max){
        lock(getrandom) // synchronize
        {
        return getrandom.Next(min, max);
        }
    }

    public void next(){
        haddi.values.Add("Correct Answer is "+quizM.question.answer);
         current.GetComponentInChildren<SpriteRenderer>().color = Color.white;
         current=null;
        state = true;
    }

    public void correctAnswer(){
        current.GetComponentInChildren<SpriteRenderer>().color = Color.white;
         current=null;
        state = true;
        makeDone();
    }

    public void wrongAnswer(){
        haddi.values.Add("Wrong Answer");
        next();
    }
    

    public void makeDone(){
        done = done.Concat(new int[] {currentIndex}).ToArray();
        left = left.Where(val => val != currentIndex).ToArray();

        boneClone[currentIndex].GetComponentInChildren<SpriteRenderer>().color = Color.green;

        if(left.Length==0){
            Debug.Log("Game Complete");
        }
    }

    void Update(){
        if(state){
            int index = GetRandomNumber(0,left.Length);
            int r = left[index];
            currentIndex = r;
            current = boneClone[r];
            current.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            currentBoneName.text = boneName[r];
            quizM.question.answer = boneName[r];
            quizM.SetQuestion();
            state=false;
        }
    } 
}
