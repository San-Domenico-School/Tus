using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************************************
 * Attached to: nothing
 * Purpose: a mix of helper methods that calculate information about a gameObject
 * Author: Seamus
 * Version: 1.0
 *************************************************/

public static class ObjectStatisticsUtility
{

    //creates a texture based on the objects surface area and the amount of uv space taken up
    public static Texture2D CreateObjectTexture(GameObject gameObject, float targetTexelDensity)
    {
        float uvPercentage = CalculateObjectUVAreaPercentage(gameObject);
        float objectArea = CalculateObjectArea(gameObject);

        float fullTextureArea = objectArea + ((1 - uvPercentage) * objectArea);

        int textureSize = (int)Math.Round(Math.Sqrt(fullTextureArea) * targetTexelDensity);
        textureSize = Mathf.Max(1, textureSize); // Ensure textureSize is at least 1

        Debug.Log($"objectArea: {objectArea}, uvPercentage: {uvPercentage}, fullTextureArea: {fullTextureArea}, textureSize: {textureSize}");

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

    // Gets or creates a new texture for the given object 
    public static Texture2D GetOrCreateObjectsTexture(GameObject gameObject, float texelDensity)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        Texture2D texture;

        // TODO - make it so if not texture already the newly created texture get saved
        if(renderer.material.mainTexture == null)
        {
            // does not have a texture. Creates a new texture
            // this created texture does not get saved
            renderer.material.mainTexture = ObjectStatisticsUtility.CreateObjectTexture(gameObject, texelDensity);
            texture = (Texture2D)renderer.material.mainTexture;
            //PrepairWorld.paintableObjects.add
        }
        else
        {
            // has a texture, uses existing texture
            texture = (Texture2D) renderer.material.mainTexture;
        }
        return texture;
    }

    // Checks if the object has a Renderer component 
    public static bool HasRender(GameObject gameObject)
    {
        if (gameObject == null)
            return false;
        else if (gameObject.GetComponent<Renderer>() == null)
            return false;
        else if (gameObject.gameObject.GetComponent<Renderer>().material == null)
            return false;
        else 
            return true;
    }
    
    // Checks if the object has a MainTexture in its material  
    public static bool HasMainTexture(GameObject gameObject)
    {
        if (gameObject == null)
            return false;
        else if (gameObject.GetComponent<Renderer>() == null)
            return false;
        else if (gameObject.gameObject.GetComponent<Renderer>().material == null)
            return false;
        else if (gameObject.gameObject.GetComponent<Renderer>().material.mainTexture == null)
            return false;
        else 
            return true;
    }
}
