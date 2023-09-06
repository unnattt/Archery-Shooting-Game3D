using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BowController : MonoBehaviour
{
    [SerializeField] LineRenderer bowString;
    [SerializeField] Arrow arrowPrefab;
    [SerializeField] Transform arrowStartPoint;
    [SerializeField] Transform arrowEndPoint;
    [SerializeField] Transform pointBetweenStartAndEnd;

    [Header("Arrow Force Area")]  
    [SerializeField] private float forcePower;
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;
    [SerializeField] private bool canThrow;
    [SerializeField] private TMP_Text textPower;


    [SerializeField] private float duration;
    [SerializeField] private Vector3 pullAmount;    
    [SerializeField] private float speed;
    private Vector3 initialStringPos;
    private Vector3 initialArrowPos;

    //Coroutine inital;
    public Arrow currentArrow;

    private void Start()
    {
        SpwanNewArrow();
        initialArrowPos = pointBetweenStartAndEnd.position;
        //initialStringPos = bowString.GetPosition(1);
    }

    void Update()
    {       
        UpdatePullingString(pointBetweenStartAndEnd.localPosition);              

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            canThrow = true;           
        }
        if(canThrow &&Input.GetKeyDown(KeyCode.Space))
        {
            forcePower = PullValue();
            BowThrower(forcePower);
        }
    }

    public void SpwanNewArrow()
    {
         Instantiate(arrowPrefab, arrowStartPoint.position, arrowStartPoint.rotation);        
    }

    public void AssignArrow(Arrow arrow)
    {
        currentArrow = arrow;
    }

    public void UnAssignArrow()
    {
        currentArrow = null;
    }

    public void ResetPos()
    {
        pointBetweenStartAndEnd.position = initialArrowPos;        
    }
   

    public void BowThrower(float forcePower)
    {        
        Vector3 force = transform.forward * forcePower;
        currentArrow.Thrower(force);
        ResetPos();
        Invoke(nameof(SpwanNewArrow), 1f);
    }

    public float PullValue()
    {
        float pullDirection = Vector3.Distance(arrowStartPoint.position, arrowEndPoint.position);
        float targetDirection = Vector3.Distance(arrowStartPoint.position, pointBetweenStartAndEnd.position);       
        float pullValue = targetDirection / pullDirection;
        float t = Mathf.Clamp(pullValue, 0, 1);
        float streach = minValue + t * (maxValue - minValue);
        Debug.Log("streach : " + streach);
        //return endPointPos;
        return streach;
    }

    public Vector3 NearestPointOnFiniteLine(Vector3 start, Vector3 end, Vector3 pnt)
    {
        var line = (end - start);
        var len = line.magnitude;
        line.Normalize();

        var v = pnt - start;
        var d = Vector3.Dot(v, line);
        d = Mathf.Clamp(d, 0f, len);
        return start + line * d;
    }

    public Vector3 NearestPointOnLine(Vector3 linePoint, Vector3 lineDirection, Vector3 point)
    {
        lineDirection.Normalize();//this needs to be a unit vector
        //var v = point - lineDirection;
        //var d = Vector3.Dot(v, lineDirection);
        return linePoint + Vector3.Project(point - linePoint, lineDirection);
        //return linePoint + lineDirection * d;
    }
    public void UpdatePullingString(Vector3 updatedString)
    {
        Vector3 linePosition = updatedString;
        bowString.SetPosition(1, linePosition);
        forcePower = PullValue();       
        //currentArrow.transform.localPosition = new Vector3(currentArrow.transform.localPosition.x, currentArrow.transform.localPosition.y, linePosition.z + 0.2f);
    }
}
//Rigidbody arrowRb = temp.GetComponent<Rigidbody>();
//arrowRb.isKinematic = false;
////arrowRb.AddForce(new Vector3(0f, 0f, 1500f));
//arrowRb.velocity = Camera.main.transform.forward * Mathf.Abs(endPointPos) * 100f;
//temp = null;    



//float updateString = UpdateString(t);
//pullAmount = NearestPointOnFiniteLine(arrowStartPoint.position, arrowEndPoint.position, pointBetweenStartAndEnd.position);
//UpdatePullingString(pullAmount);
//float t = 0.0001f * Time.deltaTime;
//float endArrowPos = ArrowToEndPos(t);
//Vector3 tempPos = new Vector3(0, 0, endArrowPos);
//float d = Vector3.Distance(currentArrow.transform.position, arrowStartPoint.position);
//forcePower = Remap(d, 10f, 50f, 0f, -0.2f);

//IEnumerator ChangeArrowPosOnTap(float strachStringEndPoint, float arrowPosEndPoint)
//{
//    float time = 0;
//    while (time < duration)
//    {
//        //float strachString = Mathf.Lerp(-0.2f, strachStringEndPoint, time / duration);
//        float changeArrowPos = Mathf.Lerp(0f, arrowPosEndPoint, time / duration);
//        //bowString.SetPosition(1, new Vector3(0f, 0f, strachString));
//        Vector3 tempPos = new Vector3(0, 0, changeArrowPos);
//        currentArrow.transform.localPosition = tempPos;
//        PullValue(tempPos);
//        time += Time.deltaTime;
//        yield return null;
//    }
//}

//private float UpdateString(float t)
//{
//    t = Mathf.Clamp01(t);
//    float minValue = -0.5f;
//    float maxValue = -0.2f;
//    pullAmount = minValue + t * (maxValue - minValue);
//    Debug.Log("PullAmount" + pullAmount);
//    return pullAmount;
//}


//private float ArrowToEndPos(float t)
//{
//    //t = Mathf.Clamp01(t);

//}
//public float Remap(float unscaledNum, float minAllowed, float maxAllowed, float min, float max)
//{
//    return (maxAllowed - minAllowed) * (unscaledNum - min) / (max - min) + minAllowed;
//}


//if (canThrow)
//{
//    textPower.text = "Power:" + forcePower.ToString();

//}
//else
//{
//    textPower.text = "Power: 0";
//}

//if (canThrow && forcePower < maxForcePower)
//{
//    forcePower += Time.deltaTime * forcePowerSpeed;
//}

//if (canThrow && Input.GetKeyDown(KeyCode.Space))
//{
//    //StopCoroutine(ChangeArrowPosOnTap(pullAmount, endPointPos));
//    canThrow = false;
//    //StopCoroutine(inital);
//    BowThrower(forcePower);
//    ResetPos();
//    forcePower = 0f;
//}