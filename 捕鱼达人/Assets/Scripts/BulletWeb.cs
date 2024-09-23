using UnityEngine;

public class BulletWeb : MonoBehaviour
{

	/** UI组件 **/

	//停留时长
	public float DelayTime = 0;


	/** 逻辑变量 **/

	[HideInInspector]
	//捕获概率提升倍数 - > 增加1.1倍
	public float CaptureEnhancMultiple = 1.1f;

	[HideInInspector]
	//捕获奖励提升倍数 - > 增加1.1倍
	public float RewardEnhanceMultiple = 1.1f;

	private void Start()
	{
		Destroy(gameObject, DelayTime);
	}

	/// <summary>
	/// 设置子弹对捕获概率和奖励概率的提升
	/// </summary>
	/// <param name="_captureEnhancMultiple">捕获概率提升倍数</param>
	/// <param name="_rewardEnhanceMultiple">捕获奖励提升倍数</param>
	public void SetEnhancMultiple(float _captureEnhancMultiple, float _rewardEnhanceMultiple)
	{
		CaptureEnhancMultiple = _captureEnhancMultiple;
		RewardEnhanceMultiple = _rewardEnhanceMultiple;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Fish")
		{
			collision.SendMessage("FishBeAttack", this);
		}
	}

}
