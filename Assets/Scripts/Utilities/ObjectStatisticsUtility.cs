using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public static Texture2D CreateObjectTexture(Mesh mesh, float targetTexelDensity)
    {
        float uvPercentage = CalculateObjectUVAreaPercentage(mesh);
        float objectArea = CalculateObjectArea(mesh);

        float fullTextureArea = objectArea + ((1 - uvPercentage) * objectArea);

        int textureSize = (int)Math.Round(Math.Sqrt(fullTextureArea) * targetTexelDensity);
        textureSize = Mathf.Max(1, textureSize); // Ensure textureSize is at least 1

        Debug.Log($"objectArea: {objectArea}, uvPercentage: {uvPercentage}, fullTextureArea: {fullTextureArea}, textureSize: {textureSize}");

        return new Texture2D(textureSize, textureSize);
    }

    public static Texture2D CreateObjectTexture(GameObject gameObject, float targetTexelDensity)
    {
        return CreateObjectTexture(gameObject.GetComponent<MeshFilter>().mesh, targetTexelDensity);
    }

    //calculates the area of the object in meters(1 unity unit) squared 
    public static float CalculateObjectArea(Mesh mesh)
    {
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        float area = 0;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 vertA = vertices[triangles[i]];
            Vector3 vertB = vertices[triangles[i + 1]];
            Vector3 vertC = vertices[triangles[i + 2]];

            Vector3 vectorAB = vertB - vertA;
            Vector3 vectorAC = vertC - vertA;

            Vector3 cross = Vector3.Cross(vectorAB, vectorAC);

            area += cross.magnitude;
        }
        //Debug.Log(area / 2);
        return area / 2;
    }
    public static float CalculateObjectArea(GameObject gameObject)
    {
        return CalculateObjectArea(gameObject.GetComponent<MeshFilter>().mesh);
    }

    // public static float CalculateObjectAreaParallel(GameObject gameObject)
    // {
    //     Vector3[] vertices = gameObject.GetComponent<MeshFilter>().mesh.vertices;
    //     int[] triangles = gameObject.GetComponent<MeshFilter>().mesh.triangles;
        
    //     float area = 0;
    //     float[] result = new float[vertices.Length];

    //     Parallel.For(0, triangles.Length, i => 
    //     {
    //         Vector3 vertA = vertices[triangles[i]];
    //         Vector3 vertB = vertices[triangles[i + 1]];
    //         Vector3 vertC = vertices[triangles[i + 2]];

    //         Vector3 vectorAB = vertB - vertA;
    //         Vector3 vectorAC = vertC - vertA;

    //         Vector3 cross = Vector3.Cross(vectorAB, vectorAC);

    //         result[i] = cross.magnitude;
    //     });

    //     foreach(float i in result)
    //     {
    //         area += i;
    //     }

    //     return area / 2;
    // }



    //it gives you a number between 0 and 1 (its not actually %)
    public static float CalculateObjectUVAreaPercentage(Mesh mesh)
    {
        float uvArea = 0;

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

    public static float CalculateObjectUVAreaPercentage(GameObject gameObject)
    {
        return CalculateObjectUVAreaPercentage(gameObject.GetComponent<MeshFilter>().mesh);
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
            renderer.material.mainTexture = CreateObjectTexture(gameObject, texelDensity);
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
