using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralCanvas : MonoBehaviour
{
	public static GeneralCanvas instance;

	[SerializeField] private Slider energyBar;

	[SerializeField] private Image circle;
	// Use this for initialization
	void Awake()
	{
		if (instance == null) instance = this;
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowEnergy(int n)
	{
		energyBar.value = n / 8.0f;
	}

	public void PlayCircle()
	{
		StartCoroutine(playCircle());
	}

	private IEnumerator playCircle()
	{
		yield return null;
		float size = 1;
		float a = 1;
		RectTransform rect = circle.gameObject.GetComponent<RectTransform>();
		rect.sizeDelta=new Vector2(1,1);
		circle.color = Color.green;
		circle.enabled = true;
		while (a>0)
		{
			a *= 1 - 0.05f;
			circle.color = new Color(0, 1, 0, a);
			size *= 1 + 0.1f;
			rect.sizeDelta = new Vector2(size, size);
			yield return new WaitForSeconds(0.02f);
		}
		circle.enabled = false;
		

	}
}
