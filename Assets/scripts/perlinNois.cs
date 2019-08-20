using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perlinNois : MonoBehaviour
{
    public int width = 40;
    public int height = 40;
    public float scale = 20;
    public float offsetX = 100f;
    public float offsetY = 100f;

    public GameObject tileWater;
    public GameObject tileGround;
    public GameObject tileSand;
    private SpriteRenderer tileRenderer;
    public Color[] tileColor;


    private void Start()
    {
        offsetX = Random.Range(0f, 99999f);
        offsetY = Random.Range(0f, 99999f);
        GenerateTerrain();
    }
    private void Update()
    {
        
        //Renderer renderer = GetComponent<Renderer>();
        //renderer.material.mainTexture = GenerateTexture();
    }

    void GenerateTerrain()
    {

        for (float x = 0; x < width; x+= 0.3f)
        {
            for (float y = 0; y < height; y+=0.3f)
            {
                float types = Calculatenoise(x, y);
                if (types <= 0.5) {
                    tileGround = Instantiate(tileGround, new Vector2(x - 6.0f, y - 4.60f), Quaternion.identity) as GameObject;
                }
                else if (types <= 0.6)
                {
                    tileSand = Instantiate(tileSand, new Vector2(x - 6.0f, y - 4.60f), Quaternion.identity) as GameObject;
                }
                else if (types <= 1)
                {
                    tileWater = Instantiate(tileWater, new Vector2(x - 6.0f, y - 4.60f), Quaternion.identity) as GameObject;
                }
                print(types);
            }
        }
    }

    float Calculatenoise(float x, float y)
    {
        float xCoord = x / width * scale + offsetX;
        float yCoord = y / height * scale + offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return sample;
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    Color CalculateColor (int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
    }
}
