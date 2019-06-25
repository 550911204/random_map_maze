using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class view : MonoBehaviour {

	public Texture m_texture;

	public Texture2D texture2d;

	// Use this for initialization
	void Awake () {
		//texture2d = new Texture2D (m_texture.width, m_texture.height, m_texture.ARGB32, false);
		//texture2d.ReadPixels (new Rect (0, 0, m_texture.width, m_texture.height), 0, 0);
		//texture2d.Apply ();


		for (int x = 0; x < texture2d.width; x++) {
			for (int y = 0; y < texture2d.height; y++) {
				texture2d.SetPixel(x, texture2d.height - 1 - y, new Color32(0, 0, 0, 255));
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnGUI()
	{
		GUI.DrawTexture(new Rect((int)(Screen.width * 0.22), (int)(Screen.height * 0.75), texture2d.width, texture2d.height),texture2d);
	}
}
