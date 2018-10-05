using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BitmapQuad : MonoBehaviour {

	Material material;

	// Use this for initialization
	public void Start()
	{
			// Create a 16x16 texture with PVRTC RGBA4 format
			// and fill it with raw PVRTC bytes.

			// Texture2D tex = new Texture2D(16, 16, TextureFormat.PVRTC_RGBA4, false);
			//
			// // Raw PVRTC4 data for a 16x16 texture. This format is four bits
			// // per pixel, so data should be 16*16/2=128 bytes in size.
			// // Texture that is encoded here is mostly green with some angular
			// // blue and red lines.
			// byte[] pvrtcBytes = new byte[] {
			// 		0x30, 0x32, 0x32, 0x32, 0xe7, 0x30, 0xaa, 0x7f, 0x32, 0x32, 0x32, 0x32, 0xf9, 0x40, 0xbc, 0x7f,
			// 		0x03, 0x03, 0x03, 0x03, 0xf6, 0x30, 0x02, 0x05, 0x03, 0x03, 0x03, 0x03, 0xf4, 0x30, 0x03, 0x06,
			// 		0x32, 0x32, 0x32, 0x32, 0xf7, 0x40, 0xaa, 0x7f, 0x32, 0xf2, 0x02, 0xa8, 0xe7, 0x30, 0xff, 0xff,
			// 		0x03, 0x03, 0x03, 0xff, 0xe6, 0x40, 0x00, 0x0f, 0x00, 0xff, 0x00, 0xaa, 0xe9, 0x40, 0x9f, 0xff,
			// 		0x5b, 0x03, 0x03, 0x03, 0xca, 0x6a, 0x0f, 0x30, 0x03, 0x03, 0x03, 0xff, 0xca, 0x68, 0x0f, 0x30,
			// 		0xaa, 0x94, 0x90, 0x40, 0xba, 0x5b, 0xaf, 0x68, 0x40, 0x00, 0x00, 0xff, 0xca, 0x58, 0x0f, 0x20,
			// 		0x00, 0x00, 0x00, 0xff, 0xe6, 0x40, 0x01, 0x2c, 0x00, 0xff, 0x00, 0xaa, 0xdb, 0x41, 0xff, 0xff,
			// 		0x00, 0x00, 0x00, 0xff, 0xe8, 0x40, 0x01, 0x1c, 0x00, 0xff, 0x00, 0xaa, 0xbb, 0x40, 0xff, 0xff,
			// };

			// ##############

/*
			Texture2D tex = new Texture2D(64, 64); //, TextureFormat.RGB24, false);

			// data:image/png;base64,
			string base64Circle = "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";

			byte[] imageBytes = System.Convert.FromBase64String(base64Circle);

			// Load data into the texture and upload it to the GPU.
			tex.LoadImage(imageBytes);
			tex.Apply();
			// Assign texture to renderer's material.
			GetComponent<Renderer>().material.mainTexture = tex;

			Shader shader = Shader.Find("Sprites/Default");
			GetComponent<Renderer>().material.shader = shader;
*/

/*
			var texture = new Texture2D(128, 128, TextureFormat.RGBA32, false);
			GetComponent<Renderer>().material.mainTexture = texture;

			// RGBA32 texture format data layout exactly matches Color32 struct
			var data = texture.GetRawTextureData<Color32>();

			// fill texture data with a simple pattern
			Color32 orange = new Color32(255, 165, 0, 255);
			Color32 teal = new Color32(0, 128, 128, 255);
			int index = 0;
			for (int y = 0; y < texture.height; y++)
			{
				for (int x = 0; x < texture.width; x++)
				{
					data[index++] = ((x & y) == 0 ? orange : teal);
				}
			}
			// upload to the GPU
			texture.Apply();
*/

			Texture2D sourceTexture = GetComponent<Renderer>().material.mainTexture as Texture2D;
			Debug.Log("sourceTexture (width, height) " + sourceTexture.width + ", " + sourceTexture.height + ")");
			Texture2D texture = new Texture2D(1024, 768, TextureFormat.RGBA32, false);
			Debug.Log("texture (width, height) " + texture.width + ", " + texture.height + ")");
			GetComponent<Renderer>().material.mainTexture = texture;

			// // copy texture
			// var sourceData = sourceTexture.GetPixels32();
			// texture.SetPixels32(sourceData);

			// RGBA32 texture format data layout exactly matches Color32 struct
			var sourceData = sourceTexture.GetPixels32(); //GetRawTextureData<Color32>();
			var data = texture.GetPixels32();//GetRawTextureData<Color32>();
			Debug.Log("sourceData.length: " + sourceData.Length);
			Debug.Log("data.length: " + data.Length);

			// fill texture data with a simple pattern
			Color32 orange = new Color32(255, 165, 0, 255);
			Color32 teal = new Color32(0, 128, 128, 255);
			Color32 transparent = new Color32(0, 0, 0, 0);

			// Vector2[] polygon = new Vector2[] {
			// 	new Vector2(100, 100),
			// 	new Vector2(200, 100),
			// 	new Vector2(200, 200),
			// 	new Vector2(100, 200)
			// };

			PolygonData polygonData = loadPolygon("pig-polygon.json");
			Vector2[] polygon = polygonData.vertices;
	        for (int i=0; i<polygon.Length; i++) {
	            // Vector2 vertex = polygon[i];
				float x = polygon[i].x;
				float y = polygon[i].y;
				x = x * 100.0f + 512.0f;
				y = y * 100.0f + 384.0f;
	            polygon[i].x = x;
				polygon[i].y = y;
	        }

			int index = 0;
			for (int y = 0; y < texture.height; y++)
			{
				for (int x = 0; x < texture.width; x++)
				{
					// data[index] = ((x & y) == 0 ? sourceData[index] : transparent);
					if (!IsInPolygon(polygon, new Vector2(x ,y))) {
						data[index] = transparent;
					} else {
						data[index] = sourceData[index];
					}
					index++;
				}
			}
			texture.SetPixels32(data);
			// upload to the GPU
			texture.Apply();
	}

	// Update is called once per frame
	void Update () {

	}

	void getTextureData() {

	}

	/// <summary>
	/// Determines if the given point is inside the polygon
	/// </summary>
	/// <param name="polygon">the vertices of polygon</param>
	/// <param name="testPoint">the given point</param>
	/// <returns>true if the point is inside the polygon; otherwise, false</returns>
	bool IsPointInPolygon4(Vector2[] polygon, Vector2 testPoint)
	{
			bool result = false;
			int j = polygon.Length - 1;
			for (int i = 0; i < polygon.Length; i++)
			{
					if (polygon[i].y < testPoint.y && polygon[j].y >= testPoint.y || polygon[j].y < testPoint.y && polygon[i].y >= testPoint.y)
					{
							if (polygon[i].x + (testPoint.y - polygon[i].y) / (polygon[j].y - polygon[i].y) * (polygon[j].x - polygon[i].x) < testPoint.x)
							{
									result = !result;
							}
					}
					j = i;
			}
			return result;
	}

	bool IsInPolygon(Vector2[] poly, Vector2 p)
	{
	    Vector2 p1, p2;
	    bool inside = false;

	    if (poly.Length < 3)
	    {
	        return inside;
	    }

	    var oldPoint = new Vector2(
	        poly[poly.Length - 1].x, poly[poly.Length - 1].y);

	    for (int i = 0; i < poly.Length; i++)
	    {
	        var newPoint = new Vector2(poly[i].x, poly[i].y);

	        if (newPoint.x > oldPoint.x)
	        {
	            p1 = oldPoint;
	            p2 = newPoint;
	        }
	        else
	        {
	            p1 = newPoint;
	            p2 = oldPoint;
	        }

	        if ((newPoint.x < p.x) == (p.x <= oldPoint.x)
	            && (p.y - (long) p1.y)*(p2.x - p1.x)
	            < (p2.y - (long) p1.y)*(p.x - p1.x))
	        {
	            inside = !inside;
	        }

	        oldPoint = newPoint;
	    }

	    return inside;
	}

	public PolygonData loadPolygon(string filename)
	{
	  // Path.Combine combines strings into a file path
	  // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
	  string filePath = Path.Combine(Application.streamingAssetsPath, filename);
	  PolygonData loadedPolygonData = null;

	  if(File.Exists(filePath))
	  {
		// Read the json from the file into a string
		string dataAsJson = File.ReadAllText(filePath);
		Debug.Log(dataAsJson);
		// Pass the json to JsonUtility, and tell it to create a GameData object from it
		loadedPolygonData = JsonUtility.FromJson<PolygonData>(dataAsJson);

		// Retrieve the allRoundData property of loadedPolygonData
		string polygonName = loadedPolygonData.name;
		Debug.Log(polygonName);
	  }
	  else
	  {
		Debug.LogError("Cannot load polygon data!");
	  }

	  return loadedPolygonData;
	}
}
