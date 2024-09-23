using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public static GameManager Instance;
	private void Awake()
	{
		//强制帧频为120帧
		//Application.targetFrameRate = 120;

		Instance = this;
	}


	/** UI组件 **/

	//当前Canvas
	public RectTransform UGUICanvas;

	//UI逻辑管理类
	public UILogic UILogic;

	//迫击炮列表
	public GameObject[] GunList;

	//子弹容器
	public Transform BulletContainer;

	//子弹列表
	public GameObject[] GunBulletList;
	//每一发炮弹所需要的金币
	public int[] GunBulletGoldList;

	public Text BulletGoldTF;

	// true - 将会把点击处作为子弹目标, 子弹在到达目标后释放渔网
	// false - 子弹在发出后, 只要碰到鱼就释放渔网
	public bool IsUseTouchTarget = true;


	/** 逻辑变量 **/

	private int BulletLvIndex = -1;

	private void Start()
	{
		ClickGunPlus();
	}


	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			FireAttack();
		}

		MouseScrollWheelChangeGun();
	}


	/// <summary>
	/// 开火攻击
	/// </summary>
	private void FireAttack()
	{
		//金币余额是否充足
		bool IsGoldSufficient = false;

		if (UILogic.Gold - GunBulletGoldList[BulletLvIndex] >= 0)
		{
			IsGoldSufficient = true;
		}
		//金币余额不足
		else
		{
			while (BulletLvIndex != 0 && UILogic.Gold > 0 && UILogic.Gold - GunBulletGoldList[BulletLvIndex] < 0)
			{
				ClickGunMinus();
			}
			if (UILogic.Gold - GunBulletGoldList[BulletLvIndex] >= 0)
			{
				IsGoldSufficient = true;
			}
			else
			{
				UILogic.GoldNotSufficient();
			}
		}

		if (IsGoldSufficient)
		{
			//扣钱
			UILogic.Gold = UILogic.Gold - GunBulletGoldList[BulletLvIndex];

			//创建一个子弹
			GameObject bullet = Instantiate(GunBulletList[BulletLvIndex], BulletContainer);

			bullet.transform.SetParent(BulletContainer, false);
			bullet.transform.position = GunList[BulletLvIndex / 4].transform.Find("FirePosition").transform.position;
			bullet.transform.rotation = GunList[BulletLvIndex / 4].transform.Find("FirePosition").transform.rotation;
			bullet.AddComponent<EF_AutoMove>().TargetDirection = Vector3.up;
			bullet.GetComponent<EF_AutoMove>().Speed = bullet.GetComponent<Bullet>().Speed;

			Vector3 mousePosition = Vector3.zero;
			//将屏幕空间上的点转换为位于给定RectTransform平面上的世界空间中的位置。cam参数应该是与屏幕点相关的相机。对于Canvas设置为“Screen Space -Overlay mode”模式的情况，cam参数应该为null。
			RectTransformUtility.ScreenPointToWorldPointInRectangle(UGUICanvas, new Vector2(Input.mousePosition.x, Input.mousePosition.y), Camera.main, out mousePosition);
			bullet.GetComponent<Bullet>().TargetPosition = mousePosition;
		}
	}

	/// <summary>
	/// 鱼被捕获
	/// </summary>
	/// <param name="_gold">金币奖励</param>
	/// <param name="_Exp">经验奖励</param>
	public void FishDie(float _gold, float _Exp)
	{
		UILogic.Gold = UILogic.Gold + (int)Mathf.Ceil(_gold);
		UILogic.ExpProgress = UILogic.ExpProgress + _Exp;
	}


	/// <summary>
	/// 鼠标滚轮控制炮弹等级
	/// </summary>
	private void MouseScrollWheelChangeGun()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			ClickGunPlus();
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			ClickGunMinus();
		}
	}

	/// <summary>
	/// 点击提升炮弹等级
	/// </summary>
	public void ClickGunPlus()
	{
		GunList[BulletLvIndex / 4].SetActive(false);

		BulletLvIndex++;

		BulletLvIndex = Mathf.Clamp(BulletLvIndex, 0, GunBulletGoldList.Length - 1);

		GunList[BulletLvIndex / 4].SetActive(true);

		BulletGoldTF.text = GunBulletGoldList[BulletLvIndex] + "";
	}
	/// <summary>
	/// 点击降低炮弹等级
	/// </summary>
	public void ClickGunMinus()
	{
		GunList[BulletLvIndex / 4].SetActive(false);

		BulletLvIndex--;
		BulletLvIndex = Mathf.Clamp(BulletLvIndex, 0, GunBulletGoldList.Length - 1);

		GunList[BulletLvIndex / 4].SetActive(true);

		BulletGoldTF.text = GunBulletGoldList[BulletLvIndex] + "";
	}


	private bool IsAutoFire = false;
	public void ClickAutoFire()
	{
		IsAutoFire = !IsAutoFire;
		if (IsAutoFire)
		{
			InvokeRepeating("FireAttack", 0, 0.01f);
		}
		else
		{
			CancelInvoke("FireAttack");
		}
	}

}
