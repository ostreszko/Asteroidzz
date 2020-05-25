using System;
using UnityEngine;

public static class AsteroidsHelper
{
    private static readonly System.Random rand = new System.Random();

    public enum asteroidsSizes
    {
        SmallAsteroid = 0,
        MediumAsteroid = 1,
        BigAsteroid = 2
    }
    public static void GenerateRandomVelocity(Rigidbody2D asteroidRigbody)
    {
        asteroidRigbody.velocity = new Vector2(rand.Next(-30, 30), rand.Next(-30, 30));
    }
    public static void GenerateRandomVelocity(GameObject asteroidGameObject)
    {
        asteroidGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(rand.Next(-30, 30), rand.Next(-30, 30));
    }

    public static Vector2 GeneratePosition(Transform asteroidTransform)
    {
       return new Vector2(asteroidTransform.position.x + rand.Next(-10, 10), asteroidTransform.position.y + rand.Next(-10, 10));
    }

    public static GameObject GenerateSmallerAsteroid(Transform asteroidTransform, string tag, ObjectPooler objectPooler)
    {
        int asteroidSizeValue = (int)System.Enum.Parse(typeof(asteroidsSizes), tag);
        if (asteroidSizeValue == 0)
            return null;

        GameObject smallerAsteroid = objectPooler.SpawnFromPool(Enum.GetName(typeof(asteroidsSizes), asteroidSizeValue - 1),GeneratePosition(asteroidTransform), asteroidTransform.rotation);
        AsteroidsHelper.GenerateRandomVelocity(smallerAsteroid.GetComponent<Rigidbody2D>());
        
        return smallerAsteroid;
            
            
    }
}
