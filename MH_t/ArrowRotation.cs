using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation : MonoBehaviour
{
	public Transform arrow;
	public GameObject arrowImage;
	public GameObject target;

	  public	float x;
        public	  float y;
	  public		float z;

	public	bool left;
	public	bool right;
	public	bool up;
	public	bool down;

	public	bool leftUp;
	public	bool leftDown;
	public	bool rightUp;
	public	bool rightDown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	Reload();
     	
     	 x = target.GetComponent<WheelDrive>().x1;
		 y = target.GetComponent<WheelDrive>().y1;
 		 z = target.GetComponent<WheelDrive>().z1;
		
 		 if(x!=0){
 		 	if(x>0){
 		 		up=true;
 		 		down = false;
 		 		}else{ 
 		 			up = false;
 		 			down = true;}
 		 }else{
 		 	up = false;
 		 	down = false;
 		 }

 		  if(z!=0){
 		 	if(z>0){
 		 		right=true;
 		 		left = false;
 		 		}else{ 
 		 			right = false;
 		 			left = true;}
 		 }else{
 		 	right = false;
 		 	left = false;
 		 }
 		 

 		 if(up){
 		 	if(!left&&!right){   Reload(); up = true;  ArrowAppear(); arrow.transform.localRotation = Quaternion.Euler(0, 0, 0);}

 		 	if(left){ Reload(); leftUp = true; ArrowAppear(); arrow.transform.localRotation = Quaternion.Euler(0, 0, 30);}
 		 	
 		 	if(right){ Reload(); rightUp = true; ArrowAppear();	arrow.transform.localRotation = Quaternion.Euler(0, 0, -30);}
 		 }

 		  if(down){
 		  	if(!left&&!right){  Reload(); down = true; ArrowAppear(); arrow.transform.localRotation = Quaternion.Euler(0, 0, 180);}

 		 	if(left){ Reload(); leftDown = true; ArrowAppear(); arrow.transform.localRotation = Quaternion.Euler(0, 0, 135);}
 		 	
 		 	if(right){ Reload(); rightDown = true; ArrowAppear(); arrow.transform.localRotation = Quaternion.Euler(0, 0, -135);}
 		 }

 		 if(left){	 left = true;  ArrowAppear(); arrow.transform.localRotation = Quaternion.Euler(0, 0, 90);
 		 }


 		 if(right)	{ right = true; ArrowAppear(); arrow.transform.localRotation = Quaternion.Euler(0, 0, -90);
 		 	}



    }

    void Reload()
    {	
    	arrowImage.gameObject.active = false;
    	left = false;
		 right = false;
		 up = false;
		 down = false;

		 leftUp = false;
		 leftDown = false;
		 rightUp = false;
		 rightDown = false;


    }

    void ArrowAppear()
    {
    	arrowImage.gameObject.active = true;
    }
}
