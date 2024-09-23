using System.Collections;
using UnityEngine;

public class FishCreater : MonoBehaviour
{

	/** UI组件 **/

	//鱼的容器
	public Transform FishContainer;

	//创建鱼群的间隔
	public float CreateDelay = 1f;

	//鱼的N个初始位置
	public Transform[] FishCreatePositionList;

	//鱼的所有Prefab
	public GameObject[] FishPrefabList;

	private void Start()
	{
		InvokeRepeating("CreateFish", 0, CreateDelay);
	}

	public void CreateFish()
	{
		//鱼的种类
		int FishType = Random.Range(0, FishPrefabList.Length);
		//初始位置
		int StartPosition = Random.Range(0, FishCreatePositionList.Length);

		//当前鱼种类的最大数量
		int MaxQuantity = FishPrefabList[FishType].GetComponent<Fish>().MaxQuantity;
		//当前鱼种类的最大速度
		int MaxSpeed = FishPrefabList[FishType].GetComponent<Fish>().MaxSpeed;

		//计算创建数量
		int Quantity = Random.Range(MaxQuantity / 2, MaxQuantity);
		Quantity = Mathf.Clamp(Quantity, 1, MaxQuantity);

		//计算本批鱼的速度
		float Speed = Random.Range(MaxSpeed / 2, MaxQuantity);
		Speed = Speed / 2;
		Speed = Mathf.Clamp(Speed, 0.5f, Speed);

		//直线
		if (Random.Range(0, 2) == 0)
		{
			StartCoroutine(CreateStraightFish(FishPrefabList[FishType], FishCreatePositionList[StartPosition], Quantity, Speed));
		}
		//转弯
		else
		{
			StartCoroutine(CreateSwerveFish(FishPrefabList[FishType], FishCreatePositionList[StartPosition], Quantity, Speed));
		}

	}

	/// <summary>
	/// 创建直行的鱼
	/// </summary>
	/// <param name="_fishPrefab"> 鱼的Prefab </param>
	/// <param name="_initPosition"> 初始的Transform </param>
	/// <param name="_quantity"> 创建的数量 </param>
	/// <param name="_speed"> 速度 </param>
	private IEnumerator CreateStraightFish(GameObject _fishPrefab, Transform _initPosition, int _quantity, float _speed)
	{
		//偏移角度 - 正方向的偏移度
		int AngleOffset = Random.Range(-22, 22);

		for (int i = 0; i < _quantity; i++)
		{
			GameObject fishItem = Instantiate(_fishPrefab);
			fishItem.transform.SetParent(FishContainer, false);

			fishItem.transform.localPosition = _initPosition.localPosition;
			fishItem.transform.localRotation = _initPosition.localRotation;
			fishItem.transform.Rotate(0, 0, AngleOffset);
			//层级排序
			fishItem.GetComponent<SpriteRenderer>().sortingOrder += i;
			fishItem.AddComponent<EF_AutoMove>().Speed = _speed;

			yield return new WaitForSeconds(1.3f);
		}

	}

	/// <summary>
	/// 创建转弯的鱼
	/// </summary>
	private IEnumerator CreateSwerveFish(GameObject _fishPrefab, Transform _initPosition, int _quantity, float _speed)
	{
		//角速度 - 每帧旋转的角度
		int AngleSpeed = 0;
		if (Random.Range(0, 2) == 0)
		{
			AngleSpeed = Random.Range(-15, -9);
		}
		else
		{
			AngleSpeed = Random.Range(9, 15);
		}

		for (int i = 0; i < _quantity; i++)
		{
			GameObject fishItem = Instantiate(_fishPrefab);
			fishItem.transform.SetParent(FishContainer, false);

			fishItem.transform.localPosition = _initPosition.localPosition;
			fishItem.transform.localRotation = _initPosition.localRotation;
			//层级排序
			fishItem.GetComponent<SpriteRenderer>().sortingOrder += i;
			fishItem.AddComponent<EF_AutoMove>().Speed = _speed;
			fishItem.AddComponent<EF_AutoRotate>().Speed = AngleSpeed;

			yield return new WaitForSeconds(1.3f);
		}
		
	}


}
