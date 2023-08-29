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
    [SerializeField] Transform spwanPointArrow;

    [Header("Arrow Force Area")]
    [SerializeField] private float maxForcePower;
    [SerializeField] private float forcePowerSpeed;
    [SerializeField] private float forcePower;
    [SerializeField] private bool canThrow;
    [SerializeField] private TMP_Text textPower;


    [SerializeField] private float duration;
    [SerializeField] private float pullAmount;
    [SerializeField] private float endPointPos;
    [SerializeField] private float speed;
    private Vector3 initialStringPos;
    private Vector3 initialArrowPos;

    Coroutine inital;
    [HideInInspector] public Arrow temp;

    private void Start()
    {
        SpwanNewArrow();
        initialArrowPos = spwanPointArrow.position;
        initialStringPos = bowString.GetPosition(1);       
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            canThrow = true;
            float t = 1f *Time.deltaTime;
            float updateString = UpdateString(t);
            float endArrowPos = ArrowToEndPos(t);
            inital = StartCoroutine(ChangeArrowPosOnTap(pullAmount, endPointPos));
        }

        if(canThrow && forcePower < maxForcePower)
        {
            forcePower += Time.deltaTime * forcePowerSpeed;
        }

        if (canThrow &&Input.GetMouseButtonUp(0))
        {
            //StopCoroutine(ChangeArrowPosOnTap(pullAmount, endPointPos));
            canThrow = false;
            StopCoroutine(inital);
            temp.isRelease = true;
            BowThrower(forcePower);
            forcePower = 0f;
            ResetPos();
            Invoke(nameof(SpwanNewArrow), 1f);
        }

        if (canThrow)
        {
            textPower.text = "Power:" + forcePower.ToString();
        }
        else
        {
            textPower.text = "Power: 0" ;
        }

    }

    void SpwanNewArrow()
    {
        Arrow arrow = Instantiate(arrowPrefab, spwanPointArrow.position, spwanPointArrow.rotation, spwanPointArrow.transform);
        //arrow.transform.parent = spwanPointArrow.transform;
        temp = arrow;
    }

    IEnumerator ChangeArrowPosOnTap(float strachStringEndPoint, float arrowPosEndPoint)
    {
        float time = 0;
        while (time < duration)
        {
            float strachString = Mathf.Lerp(-0.2f, strachStringEndPoint, time / duration);
            float changeArrowPos = Mathf.Lerp(0f, arrowPosEndPoint, time / duration);
            bowString.SetPosition(1, new Vector3(0f, 0f, strachString));
            Vector3 tempPos = new Vector3(0, 0, changeArrowPos);
            temp.transform.localPosition = tempPos;
            time += Time.deltaTime;
            yield return null;
        }
    }

    private float UpdateString(float t)
    {
        t = Mathf.Clamp01(t);
        float minValue = -0.5f;
        float maxValue = -0.2f;
        pullAmount = minValue + t * (maxValue - minValue);
        Debug.Log("PullAmount" + pullAmount);
        return pullAmount;
    }
    private float ArrowToEndPos(float t)
    {       
        t = Mathf.Clamp01(t);       
        float minValue = -0.3f;
        float maxValue = 0f;
        endPointPos = minValue + t * (maxValue - minValue);
        Debug.Log("endPointPos" + endPointPos);
        return endPointPos;
    }

    private void ResetPos()
    {
        bowString.SetPosition(1, initialStringPos);
    }

    public void BowThrower(float forcePower)
    {
        var force = spwanPointArrow.TransformDirection( spwanPointArrow.position * forcePower);
        temp.Thrower(force);
        temp = null;
    }
}
        //Rigidbody arrowRb = temp.GetComponent<Rigidbody>();
        //arrowRb.isKinematic = false;
        ////arrowRb.AddForce(new Vector3(0f, 0f, 1500f));
        //arrowRb.velocity = Camera.main.transform.forward * Mathf.Abs(endPointPos) * 100f;
        //temp = null;    

    //public float PullValue(Vector3 pullPosition)
    //{
    //    Vector3 pullDirection = pullPosition - arrowStartPoint.position;
    //    Vector3 targetDirection = arrowEndPoint.position - arrowStartPoint.position;

    //    float maxLength = targetDirection.magnitude;
    //    targetDirection.Normalize();

    //    float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;
    //    return Mathf.Clamp(pullValue, 0f, 1f);
    //}

    //public void UpdatePullingString()
    //{
    //    Vector3 linePosition = Vector3.forward * Mathf.Lerp(arrowStartPoint.localPosition.x, arrowEndPoint.localPosition.y, pullAmount);
    //    spwanPointArrow.localPosition = new Vector3(spwanPointArrow.localPosition.x, spwanPointArrow.localPosition.y, linePosition.z + 0.2f);
    //    bowString.SetPosition(1, linePosition);
    //}



