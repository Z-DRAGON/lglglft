using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EF_AutoRotate : MonoBehaviour
{

	//速度
	public float Speed = 1f;

	private void Update()
	{
		transform.Rotate(Vector3.forward * Speed * Time.deltaTime);
	}

}
