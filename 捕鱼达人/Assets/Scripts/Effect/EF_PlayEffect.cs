using UnityEngine;

public class EF_PlayEffect : MonoBehaviour {


	public GameObject[] EffectList;

	public void PlayEffect()
	{
		foreach (GameObject item in EffectList)
		{
			GameObject effect = Instantiate(item);
			effect.gameObject.transform.SetParent(gameObject.transform.parent, false);
			effect.gameObject.transform.position = gameObject.transform.position;
			effect.gameObject.transform.rotation = gameObject.transform.rotation;
			effect.gameObject.transform.localScale = gameObject.transform.localScale;
		}
	}

}
