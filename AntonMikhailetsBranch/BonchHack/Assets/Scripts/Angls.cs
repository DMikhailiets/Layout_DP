using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Angls : MonoBehaviour
{

	public GameObject targ1;


	public Text text1;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text1.text =  "Global: " + "\r\n" + "X: " + (targ1.gameObject.transform.rotation.x* Mathf.Rad2Deg).ToString()+ "\r\n"
        		+    "Y: " + (targ1.gameObject.transform.rotation.y* Mathf.Rad2Deg).ToString() + "\r\n"
        		+	 "Z: " + (targ1.gameObject.transform.rotation.z* Mathf.Rad2Deg).ToString();


    }
}
