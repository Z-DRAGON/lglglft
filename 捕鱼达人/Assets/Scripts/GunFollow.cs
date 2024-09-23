using UnityEngine;

public class GunFollow : MonoBehaviour
{

	/** UI组件 **/

	public RectTransform UGUICanvas;

	public Camera Camera;

	private void Update()
	{
		Vector3 mousePosition;

		//将屏幕空间上的点转换为位于给定RectTransform平面上的世界空间中的位置。cam参数应该是与屏幕点相关的相机。对于Canvas设置为“Screen Space -Overlay mode”模式的情况，cam参数应该为null。
		RectTransformUtility.ScreenPointToWorldPointInRectangle(UGUICanvas, new Vector2(Input.mousePosition.x, Input.mousePosition.y), Camera, out mousePosition);

		float z;
		//鼠标在右边 角度应该为负
		if (mousePosition.x > transform.position.x)
		{
			z = -Vector3.Angle(Vector3.up, mousePosition - transform.position);
		}
		//鼠标在左边 角度应该为正
		else
		{
			z = Vector3.Angle(Vector3.up, mousePosition - transform.position);
		}

		//限制角度范围
		z = Mathf.Clamp(z, -100, 100);

		//设置本地旋转
		transform.localRotation = Quaternion.Euler(0, 0, z);

		//Debug.Log("鼠标点:" + mousePosition +" , 目标z轴:" + z);
	}

}
