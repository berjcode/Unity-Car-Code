using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carcontroller : MonoBehaviour
{

    [SerializeField] float carSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float steerAngle;
    [SerializeField] float Traction;

    float dragAmount=0.99f;

    Vector3 _rotVec;
    Vector3 _moveVec;


    // Start is called before the first frame update
    public Transform lw, rw;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _moveVec += transform.forward * carSpeed * Time.deltaTime;
        transform.position += _moveVec * Time.deltaTime;

        _rotVec += new Vector3(0, SimpleInput.GetAxis("Horizontal"), 0);

        transform.Rotate(Vector3.up * SimpleInput.GetAxis("Horizontal") * steerAngle * Time.deltaTime * -_moveVec.magnitude);

        _moveVec *= dragAmount;
        _moveVec= Vector3.ClampMagnitude(_moveVec,maxSpeed);
        _moveVec=Vector3.Lerp(_moveVec.normalized,transform.forward,Traction*Time.deltaTime)*_moveVec.magnitude;

        _rotVec = Vector3.ClampMagnitude(_rotVec, steerAngle);

        lw.localRotation = Quaternion.Euler(_rotVec);
        rw.localRotation = Quaternion.Euler(_rotVec);
    }
}
