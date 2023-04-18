using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkWalls : MonoBehaviour
{
 


    GameObject murGauche;
    GameObject murDroit;
    Vector3 maxDistance;
    Vector3 minDistance;

    [Header("Lerp Parameters")]
    [SerializeField] public bool direction;

    [SerializeField]
    [Range(0f,1f)]
    float shrinkFactor = 0.6f;

    [SerializeField]
    float maxTime;

    SecondOrderDynamics wallSOD_Gauche;
    SecondOrderDynamics wallSOD_Droit;
    Vector3 leftWallLocalTargetPos;
    Vector3 rightWallLocalTargetPos;

    [SerializeField]
    float fWall, zWall, rWall;

    float timer;
    private void Awake()
    {

        timer = maxTime;

        murGauche = transform.GetChild(0).gameObject;
        murDroit = transform.GetChild(1).gameObject;
        maxDistance = murGauche.transform.localPosition;

        leftWallLocalTargetPos = maxDistance;
        rightWallLocalTargetPos = -maxDistance;


    }

    

    // Start is called before the first frame update
    void Start()
    {
        murGauche.transform.position = leftWallLocalTargetPos;
        murDroit.transform.position = rightWallLocalTargetPos;

        wallSOD_Gauche = new SecondOrderDynamics(fWall, zWall, rWall, murGauche.transform.position);
        wallSOD_Droit = new SecondOrderDynamics(fWall, zWall, rWall, murDroit.transform.position);



        //StartCoroutine(DelayWallThing(3));
    }

    // Update is called once per frame
    void Update()
    {

        minDistance = maxDistance * shrinkFactor;
        if (!direction)
        {
            //if(timer > 0) timer -= 1 * Time.deltaTime;
            leftWallLocalTargetPos = maxDistance;
            rightWallLocalTargetPos = -maxDistance;
        }
        else 
        {
            leftWallLocalTargetPos = minDistance;
            rightWallLocalTargetPos = -minDistance;
            //if (timer < maxTime) timer += 1 * Time.deltaTime;

        }
        UpdatePos();
    }

    /*public IEnumerator DelayWallThing(float _delay)
    {
        Debug.Log("babar");
        yield return new WaitForSeconds(_delay);
        leftWallLocalTargetPos = minDistance;
        rightWallLocalTargetPos = -minDistance;
    }*/


    public void UpdatePos()
    {

        //murGauche.transform.localPosition = Vector3.Lerp(minDistance, maxDistance, timer / maxTime);
        //murDroit.transform.localPosition = Vector3.Lerp(-minDistance,- maxDistance, timer / maxTime);
        murGauche.transform.localPosition = wallSOD_Gauche.GetUpdatedPos(fWall, zWall, rWall, leftWallLocalTargetPos);
        murDroit.transform.localPosition = wallSOD_Droit.GetUpdatedPos(fWall, zWall, rWall, rightWallLocalTargetPos);

    }
    public void directionTrue()
    {
        direction = true;
    }

    public void directionFalse()
    {
        direction = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("patate");
        if (other.gameObject.CompareTag("_photoNormale"))
        {
            Debug.Log("babar");
            direction = false;
        }
    }
}
