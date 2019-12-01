using UnityEngine;
using UnityEngine.UI;
using System;


[Serializable]
public enum DriveType
{
	RearWheelDrive,
	FrontWheelDrive,
	AllWheelDrive
}

public class WheelDrive : MonoBehaviour
{
    [Tooltip("Maximum angle of the wheels")]
	public float maxAngle = 30f;
	[Tooltip("Maximum torque")]
	public float maxTorque = 300f;
	[Tooltip("Maximum brake torque")]
	public float brakeTorque = 30000f;
	[Tooltip("Drag the wheel shape here.")]
	public GameObject wheelShape;

	[Tooltip("The vehicle's speed (in m/s).")]
	public float criticalSpeed = 5f;
	[Tooltip("Simulation sub-steps when the speed is above critical.")]
	public int stepsBelow = 5;
	[Tooltip("Simulation sub-steps when the speed is below critical.")]
	public int stepsAbove = 1;

	[Tooltip("The vehicle's drive type")]
	public DriveType driveType;

    private WheelCollider[] m_Wheels;


    public GameObject targ;
	public Text text;
	public Text text1;

    public float x;
    public float y;
    public float z;

    public float x1;
    public float y1;
    public float z1;

    public float xDelayMin;
    public float xDelayMax;
    public float yDelay;
    public float zDelay;

    float xP;
    float yP;
    float zP;
    bool CheckAnglesP;

    float handBrake;

	void Start()
	{
		m_Wheels = GetComponentsInChildren<WheelCollider>();

		for (int i = 0; i < m_Wheels.Length; ++i) 
		{
			var wheel = m_Wheels [i];

			if (wheelShape != null)
			{
				var ws = Instantiate (wheelShape);
				ws.transform.parent = wheel.transform;
			}
		}
	}

	void Update()
	{
		m_Wheels[0].ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);

		x = targ.gameObject.transform.rotation.x* Mathf.Rad2Deg - xP;
		y = targ.gameObject.transform.rotation.y* Mathf.Rad2Deg - yP;
		z = targ.gameObject.transform.rotation.z* Mathf.Rad2Deg - zP;


		if(x>xDelayMin||x<-xDelayMin+7){
			Mathf.Clamp(x,xDelayMin,xDelayMax);
			x1 = x-3;
		}
		else x1 = 0;
		if(y>yDelay||y<-yDelay)y1 = y;
		else y1 = 0;
		if(z>zDelay||z<-zDelay)z1 = z*1.5f;
		else z1 = 0;

		text.text =  "Global: " + "\r\n" + "X: " + x.ToString()+ "\r\n"
        		+    "Y: " + y.ToString() + "\r\n"
        		+	 "Z: " + z.ToString();

        text1.text =  "With delay: " + "\r\n" + "X1: " + x1.ToString()+ "\r\n"
        		+    "Y1: " + y1.ToString() + "\r\n"
        		+	 "Z1: " + z1.ToString();

		float angle;// = maxAngle * Input.GetAxis("Horizontal");
		float torque;// = maxTorque * Input.GetAxis("Vertical");

		angle = z1;
		torque = x1*maxTorque;

		//angle= maxAngle * Input.GetAxis("Horizontal");
		//torque= maxTorque * Input.GetAxis("Vertical");


		// handBrake = Input.GetKey(KeyCode.X) ? brakeTorque : 0;
		foreach (WheelCollider wheel in m_Wheels)
		{

			if(x1<0&&wheel.transform.localPosition.z > 0)handBrake = brakeTorque;
			else handBrake =0;

			if (wheel.transform.localPosition.z > 0)
				wheel.steerAngle = angle;

			if (wheel.transform.localPosition.z < 0)
			{
				wheel.brakeTorque = handBrake;
			}

			if (wheel.transform.localPosition.z < 0 && driveType != DriveType.FrontWheelDrive)
			{
				wheel.motorTorque = torque;
			}

			if (wheel.transform.localPosition.z >= 0 && driveType != DriveType.RearWheelDrive)
			{
				wheel.motorTorque = torque;
			}

			// Update visual wheels if any.
			if (wheelShape) 
			{
				Quaternion q;
				Vector3 p;
				wheel.GetWorldPose (out p, out q);

				Transform shapeTransform = wheel.transform.GetChild (0);

                if (wheel.name == "a0l" || wheel.name == "a1l" || wheel.name == "a2l")
                {
                    shapeTransform.rotation = q * Quaternion.Euler(0, 180, 0);
                    shapeTransform.position = p;
                }
                else
                {
                    shapeTransform.position = p;
                    shapeTransform.rotation = q;
                }
			}
		}
	}

	public void CheckAnglesPFunction()
	{
		if(!CheckAnglesP){
			CheckAnglesP = true;
			//xP = targ.gameObject.transform.rotation.x* Mathf.Rad2Deg;
			//yP = targ.gameObject.transform.rotation.y* Mathf.Rad2Deg;
			//zP = targ.gameObject.transform.rotation.z* Mathf.Rad2Deg; 
		}
	}

	public void ResetCheckAnglesP()
	{
		CheckAnglesP = false;
	}
}
