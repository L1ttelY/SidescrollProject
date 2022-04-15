using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange:MonoBehaviour {
	[SerializeField] string targetSceneName;
	private void OnTriggerEnter2D(Collider2D collision) {
		if(!collision.gameObject.GetComponent<PlayerAnimation>()) return;
		SceneManager.LoadScene(targetSceneName);
	}
}