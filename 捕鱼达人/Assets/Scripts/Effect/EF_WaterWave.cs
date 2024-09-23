using UnityEngine;

public class EF_WaterWave : MonoBehaviour
{

	public Texture[] WaterTexture;

	private Material Material;

	private int index;

	private void Start()
	{
		Material = GetComponent<MeshRenderer>().material;

		InvokeRepeating("ChangeTexture", 0, 0.1f);
	}

	private void ChangeTexture()
	{
		Material.mainTexture = WaterTexture[index];

		index = (index + 1) % WaterTexture.Length;
	}

}
