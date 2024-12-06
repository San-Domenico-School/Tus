using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectStatisticsUtility
{

    //creates a texture based on the objects surface area and the amount of uv space taken up
    public static Texture2D CreateObjectTexture(GameObject gameObject, float targetTexelDensity)
    {
        float uvPercentage = CalculateObjectUVAreaPercentage(gameObject);
        float objectArea = CalculateObjectArea(gameObject);

        float fullTextureArea = objectArea + ((1 - uvPercentage) * objectArea);
        
        int textureSize = (int)Math.Round(Math.Sqrt(fullTextureArea) * targetTexelDensity);

        Debug.Log("objectArea: " +  fullTextureArea + " uvPercentage: " + uvPercentage + " textureSize: " + textureSize );
        
        return new Texture2D(textureSize, textureSize);
    }

    //calculates the area of the object in meters(1 unity unit) squared 
    public static float CalculateObjectArea(GameObject gameObject)
    {
        float area = 0;
        Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;

        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Vector3 vertA = mesh.vertices[mesh.triangles[i]];
            Vector3 vertB = mesh.vertices[mesh.triangles[i + 1]];
            Vector3 vertC = mesh.vertices[mesh.triangles[i + 2]];

            Vector3 vectorAB = vertB - vertA;
            Vector3 vectorAC = vertC - vertA;

            Vector3 cross = Vector3.Cross(vectorAB, vectorAC);

            area += cross.magnitude;
        }
        //Debug.Log(area);

        return area / 2;
    }

    //it gives you a number between 0 and 1 (its not actually %)
    public static float CalculateObjectUVAreaPercentage(GameObject gameObject)
    {
        float uvArea = 0;

        Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Vector2 vertA = mesh.uv[mesh.triangles[i]];
            Vector2 vertB = mesh.uv[mesh.triangles[i + 1]];
            Vector2 vertC = mesh.uv[mesh.triangles[i + 2]];

            Vector2 vectorAB = vertB - vertA;
            Vector2 vectorAC = vertC - vertA;

            Vector3 cross = Vector3.Cross(vectorAB, vectorAC);

            uvArea += cross.magnitude;
        }


        return uvArea / 2;
    }

    public static Texture2D GetOrCreateObjectsTexture(GameObject gameObject, float texelDensity)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        Texture2D texture;

        if(renderer.material.mainTexture == null)
        {
            //does not have a texture. Creates a new texture
            renderer.material.mainTexture = ObjectStatisticsUtility.CreateObjectTexture(gameObject, texelDensity);
            texture = (Texture2D)renderer.material.mainTexture;
        }
        else
        {
            // has a texture, uses existing texture
            texture = (Texture2D) renderer.material.mainTexture;
        }
        return texture;
    }
}
