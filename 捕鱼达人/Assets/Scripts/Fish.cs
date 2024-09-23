using UnityEngine;

public class Fish : MonoBehaviour
{

	/** UI组件 **/

	//该种类的鱼的经验价值
	public int ExpBase = 50;

	//该种类的鱼的金币价值
	public int RewardBase = 50;

	//被捕获的概率 百分比 -> 10%
	public float CaptureOdds = 10;

	//该种类的鱼的最大数量
	public int MaxQuantity;

	//该种类的鱼的最大速度
	public int MaxSpeed;

	//该种类的鱼的死亡动画
	public GameObject FishDiePrefab;

	//该种类的鱼的奖励金币
	public GameObject FishGoldPrefab;

	//被攻击次数
	private int AccackCount = 0;

	private void Start()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Border")
		{
			Destroy(gameObject);
		}
	}

	/// <summary>
	/// 鱼被攻击
	/// </summary>
	public void FishBeAttack(BulletWeb _web)
	{
		AccackCount++;

		float newCaptureOdds = (CaptureOdds * _web.CaptureEnhancMultiple);

		float currentCaptureOdds = Random.Range(0f, 101f);
		//捕获
		if (currentCaptureOdds <= newCaptureOdds)
		{
			float RewardGold = RewardBase * _web.RewardEnhanceMultiple;
			float RewardExp = ExpBase * _web.RewardEnhanceMultiple;

			Debug.Log("[ " + transform.name + ", Attack: " + AccackCount + " ] " + 
				"[ odds: " + currentCaptureOdds + " / " + newCaptureOdds + " (" + _web.CaptureEnhancMultiple +  ")" + " ] " +
				"[ gold: " + RewardGold + " * " + RewardExp + " (" + _web.RewardEnhanceMultiple + ")" + " ] ");

			GameManager.Instance.FishDie(RewardGold, RewardExp);

			FishDie();
		}
		//未捕获
		else
		{
			//BDC0C0FF
		}
	}

	/// <summary>
	/// 鱼死亡
	/// </summary>
	private void FishDie()
	{
		//创建鱼死亡的动画
		GameObject fishDiePrefab = Instantiate(FishDiePrefab);
		fishDiePrefab.transform.SetParent(gameObject.transform.parent);
		fishDiePrefab.transform.position = gameObject.transform.position;
		fishDiePrefab.transform.rotation = gameObject.transform.rotation;
		fishDiePrefab.transform.localScale = gameObject.transform.localScale;

		//创建鱼死亡后产生的金币
		if (FishGoldPrefab != null)
		{
			GameObject fishGoldPrefab = Instantiate(FishGoldPrefab);
			fishGoldPrefab.transform.SetParent(gameObject.transform.parent, false);
			fishGoldPrefab.transform.position = gameObject.transform.position;
			fishGoldPrefab.transform.rotation = gameObject.transform.rotation;
			fishDiePrefab.transform.localScale = gameObject.transform.localScale;
			GameObject moveTarget = GameObject.Find("GoldCollision");
			fishGoldPrefab.AddComponent<EF_AutoMove>().TargetGameObject = moveTarget;
			fishGoldPrefab.GetComponent<EF_AutoMove>().Speed = 10f;
		}
		//特殊特效
		if (gameObject.GetComponent<EF_PlayEffect>() != null)
		{
			gameObject.GetComponent<EF_PlayEffect>().PlayEffect();
		}
		
		Destroy(gameObject);
	}

}
