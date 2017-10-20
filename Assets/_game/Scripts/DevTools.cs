using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTools : MonoBehaviour {

	public static DevTools instance;
	private void Awake()
	{
		instance = this;
	}

	public bool twoControllersToRuleThemAll;

}
