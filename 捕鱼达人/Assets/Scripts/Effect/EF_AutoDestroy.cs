using UnityEngine;

public class EF_AutoDestroy : MonoBehaviour {

	//停留时长
	public float DelayTime = 1f;

	private void Start()
	{
		Destroy(gameObject, DelayTime);
	}

}
