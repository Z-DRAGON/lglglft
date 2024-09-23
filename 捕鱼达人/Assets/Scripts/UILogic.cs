using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UILogic : MonoBehaviour
{

	/** UI组件 **/

	//等级
	public Text LvTF;
	//等级经验条
	public Slider LvExpProgressSlider;
	//等级名称
	public Text LvNameTF;

	//奖励倒计时
	public Text RewardCountDownTF;
	//奖励倒计时
	public Button RewardGetBtn;
	//金币大型奖励的倒计时阀值
	public float RewardCountDownMax = 240;

	//金币
	public Text GoldTF;
	//金币倒计时
	public Text GoldCountDownTF;
	//金币小型奖励的倒计时阀值
	public int GoldCountDownMax = 60;

	//背景
	public Image BG;
	//升级浪花特效
	public GameObject ChangeBGWaveEffectPrefab;
	public Sprite[] BGList;


	/** 逻辑变量 **/

	//当前等级
	private int lv = 0;
	public int Lv { get { return lv; } }
	//当前称号下标
	private int LvNameIndex;
	//等级对应称号列表
	private string[] LvNameList = { "新手", "入门", "钢铁", "青铜", "白银", "砖石", "皇冠", "王牌", "战神", "Max" };
	//当前等级经验进度值
	public float ExpProgress = 0;

	//金币大型奖励的计时器
	private float RewardCountDownTimer = 0;

	//启动金币
	private const int GoldStart = 5000;
	//金币框的原本颜色
	private Color GoldTFColor;
	//当前剩余金币
	private int gold = GoldStart;
	public int Gold { get { return gold; } set { gold = value; GoldTF.text = "$" + value; } }

	//金币小型奖励的计时器
	private float GoldCountDownTimer = 0;

	//当前背景的Index
	private int BGIndex = 0;

	private void Start()
	{
		RewardCountDownTimer = RewardCountDownMax;
		RewardGetBtn.gameObject.SetActive(false);
		RewardCountDownTF.gameObject.SetActive(true);

		GoldCountDownTimer = GoldCountDownMax;

		Gold = GoldStart;
		GoldTFColor = GoldTF.color;

		lv = 1;
	}

	private void Update()
	{
		RefreshUI();
	}

	/// <summary>
	/// 刷新UI显示
	/// </summary>
	private void RefreshUI()
	{
		LvTF.text = "" + Lv;
		LvNameIndex = Lv / 10;
		LvNameIndex = Mathf.Clamp(LvNameIndex, 0, LvNameList.Length - 1);
		LvNameTF.text = "" + LvNameList[LvNameIndex];
		while (ExpProgress > 1000 + 200 * Lv)
		{
			lv = Lv + 1;
			if (lv % 20 == 0)
			{
				ChangeBG();
			}
			ExpProgress = ExpProgress - (1000 + 200 * Lv);
		}
		LvExpProgressSlider.value = ExpProgress / (1000 + 200 * Lv);


		RewardCountDownTimer -= Time.deltaTime;
		if (RewardCountDownTimer <= 0 && !RewardGetBtn.gameObject.activeSelf)
		{
			RewardGetBtn.gameObject.SetActive(true);
			RewardCountDownTF.gameObject.SetActive(false);
		}
		RewardCountDownTF.text = (int)RewardCountDownTimer + "s";


		GoldCountDownTimer -= Time.deltaTime;
		if (GoldCountDownTimer <= 0)
		{
			GoldCountDownTimer = GoldCountDownMax;

			Gold = Gold + 1000;
		}
		GoldCountDownTF.text = (int)GoldCountDownTimer / 10 + "  " + (int)GoldCountDownTimer % 10;

	}


	/// <summary>
	/// 金币不足的闪动提示
	/// </summary>
	public void GoldNotSufficient()
	{
		StartCoroutine(GoldNotSufficientCollections());
	}
	private IEnumerator GoldNotSufficientCollections()
	{
		for (int i = 0; i < 2; i++)
		{
			GoldTF.color = Color.red;
			yield return new WaitForSeconds(0.1f);
			GoldTF.color = GoldTFColor;
			yield return new WaitForSeconds(0.1f);
		}
	}


	/// <summary>
	/// 更换一次背景
	/// </summary>
	private void ChangeBG()
	{
		GameObject WaveEffect = Instantiate(ChangeBGWaveEffectPrefab);
		WaveEffect.AddComponent<EF_AutoMove>();
		WaveEffect.GetComponent<EF_AutoMove>().TargetDirection = Vector3.left;
		WaveEffect.GetComponent<EF_AutoMove>().Speed = 10f;

		if (BGIndex != BGList.Length - 1)
		{
			BGIndex++;
			BGIndex = Mathf.Clamp(BGIndex, 0, BGList.Length - 1);
			Debug.Log("BGIndex " + BGIndex);
			BG.sprite = BGList[BGIndex];
		}
	}

	/// <summary>
	/// 点击倒计时结束的领奖
	/// </summary>
	public void ClickRewardCountDown()
	{
		RewardCountDownTimer = RewardCountDownMax;

		RewardGetBtn.gameObject.SetActive(false);
		RewardCountDownTF.gameObject.SetActive(true);

		Gold = Gold + 5000;
	}

	/// <summary>
	/// 点击返回
	/// </summary>
	public void ClickReturn()
	{
		SceneManager.LoadScene(0);
	}

	/// <summary>
	/// 点击设置
	/// </summary>
	public void ClickSetting()
	{

	}

}
