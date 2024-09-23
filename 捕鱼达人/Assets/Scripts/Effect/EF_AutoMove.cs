using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EF_AutoMove : MonoBehaviour
{

	//速度
	public float Speed = 1f;

	//目标方向
	public Vector3 TargetDirection = Vector3.right;

	//目标物体
	public GameObject TargetGameObject;

	private void Update()
	{
		if (TargetGameObject == null)
		{
			transform.Translate(TargetDirection * Speed * Time.deltaTime);
		}
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, TargetGameObject.transform.position, Speed * Time.deltaTime);
		}
		
	}

}
