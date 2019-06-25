using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using UnityEngine;

public class AnimatedGifDrawer : MonoBehaviour
{
	public bool m_start = false;
	float m_time = 0;
	void Update(){
		m_time += Time.time;
		if(m_time>=340){
			m_start = true;
		}
	}

	List<Texture2D> gifFrames = new List<Texture2D>();
	void Awake()
	{
		var gifImage = Image.FromFile(".\\Assets\\pic\\1.gif");
		var dimension = new FrameDimension(gifImage.FrameDimensionsList[0]);
		int frameCount = gifImage.GetFrameCount(dimension);
		for (int i = 0; i < frameCount; i++)
		{
			gifImage.SelectActiveFrame(dimension, i);
			var frame = new Bitmap(gifImage.Width, gifImage.Height);
			System.Drawing.Graphics.FromImage(frame).DrawImage(gifImage, Point.Empty);
			var frameTexture = new Texture2D(frame.Width, frame.Height);
			for (int x = 0; x < frame.Width; x++)
				for (int y = 0; y < frame.Height; y++)
				{
					System.Drawing.Color sourceColor = frame.GetPixel(x, y);
					frameTexture.SetPixel(x, frame.Height - 1 - y, new Color32(sourceColor.R, sourceColor.G, sourceColor.B, sourceColor.A));
				}
			frameTexture.Apply();
			gifFrames.Add(frameTexture);
		}
	}

	void OnGUI()
	{
		if(m_start)
			GUI.DrawTexture(new Rect((int)(Screen.width * 0.22), (int)(Screen.height * 0.75), gifFrames[0].width, gifFrames[0].height), gifFrames[(int)(Time.frameCount * 0.1) % gifFrames.Count]);
	}
}  