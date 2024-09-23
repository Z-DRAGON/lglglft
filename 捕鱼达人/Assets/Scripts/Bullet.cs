using UnityEngine;

public class Bullet : MonoBehaviour
{

	/** UI组件 **/

	//捕获概率提升倍数 - > 增加1.1倍
	public float CaptureEnhancMultiple = 1.1f;

	//捕获奖励提升倍数 - > 增加1.1倍
	public float RewardEnhanceMultiple = 1.1f;

	//该种类子弹的速度
	public int Speed;

	//该种类子弹爆炸生成的网
	public GameObject BulletWebPrefab;


	/** 逻辑变量 **/

	[HideInInspector]
	//该子弹的目标位置
	public Vector3 TargetPosition = Vector3.zero;

	//是否使用碰撞法
	private bool IsUseTriggerEnter = false;

	private void Start()
	{
	}

	private void Update()
	{
		if (TargetPosition != Vector3.zero && GameManager.Instance.IsUseTouchTarget)
		{
			if (Mathf.Abs(transform.position.x) >= Mathf.Abs(TargetPosition.x) && transform.position.y >= TargetPosition.y)
			{
				CreateBulletWeb();
				TargetPosition = Vector3.zero;
			}
		}
		else
		{
			IsUseTriggerEnter = true;
		}

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Border")
		{
			Destroy(gameObject);
		}

		if (IsUseTriggerEnter && collision.tag == "Fish")
		{
			GameObject web = Instantiate(BulletWebPrefab);
			web.transform.SetParent(gameObject.transform.parent, false);
			web.transform.position = gameObject.transform.position;
			web.transform.localScale = gameObject.transform.localScale;
			web.GetComponent<BulletWeb>().SetEnhancMultiple(CaptureEnhancMultiple, RewardEnhanceMultiple);
			//web.GetComponent<BulletWeb>().Damage = GoldConsume;

			Destroy(gameObject);
		}
	}


	/// <summary>
	/// 将子弹转化为渔网
	/// </summary>
	public void CreateBulletWeb()
	{
		GameObject web = Instantiate(BulletWebPrefab);
		web.transform.SetParent(gameObject.transform.parent, false);
		web.transform.position = gameObject.transform.position;
		web.transform.localScale = gameObject.transform.localScale;
		web.GetComponent<BulletWeb>().SetEnhancMultiple(CaptureEnhancMultiple, RewardEnhanceMultiple);

		Destroy(gameObject);
	}

}
