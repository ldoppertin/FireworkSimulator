using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShootingStarSpawner : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    public int spawnCount;
    public bool timing;
    public GameObject gameObjectPrefabBlue;
    public GameObject gameObjectPrefabGreen;
    public GameObject gameObjectPrefabOrange;
    public GameObject gameObjectPrefabPink;
    public GameObject gameObjectPrefabPurple;

    private World defaultWorld;
    private EntityManager entityManager;
    private Entity entityPrefabBlue;
    private Entity entityPrefabGreen;
    private Entity entityPrefabOrange;
    private Entity entityPrefabPink;
    private Entity entityPrefabPurple;
    private float entityCount;

    [SerializeField]
    Text CountTxt;
    [SerializeField]
    Text ParticleCountTxt;

    private void Start()
    {
        entityCount = 0;
        defaultWorld = World.DefaultGameObjectInjectionWorld;
        entityManager = defaultWorld.EntityManager;
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(defaultWorld, null);

        entityPrefabBlue = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPrefabBlue, settings);
        entityPrefabGreen = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPrefabGreen, settings);
        entityPrefabOrange = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPrefabOrange, settings);
        entityPrefabPink = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPrefabPink, settings);
        entityPrefabPurple = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPrefabPurple, settings);
    }

    void OnMouseDown()
    {
        var screenPos = Input.mousePosition;
        var ray = cam.ScreenPointToRay(screenPos);
        if (timing == true)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.name == "Atmosphere")
                {
                    SpawnWave(new Vector3(hit.point.x, hit.point.y, hit.point.z));
                  //  InstantiateEntity(new Vector3(hit.point.x, hit.point.y, hit.point.z));
                    //Debug.Log(ray);
                }
                else
                {
                    Debug.Log("Plane was clicked but raycast hit something else.");
                }
            }
            else
            {
                Debug.Log("Plane was clicked but raycast missed.");
            }
        }
    }

    private void InstantiateEntity(float3 position, Entity entity)
    {
        Entity myEntity = entityManager.Instantiate(entity);
 

        entityManager.SetComponentData(myEntity, new Translation
        {
            Value = position
        });
        entityManager.SetComponentData(myEntity, new Rotation
        {
            Value = UnityEngine.Random.rotation
        });

    }

    IEnumerator ClearEntityList(List<Entity> Firework)
    {
        yield return new WaitForSeconds(15.0f);
        for (int i = 0; i < Firework.Count; i++)
        {
            entityManager.DestroyEntity(Firework[i]);
            entityCount--;
            CountTxt.text = entityCount.ToString();
            ParticleCountTxt.text = (entityCount * 25000).ToString();
        }
    }


    private void SpawnWave(float3 position)
    {

        // 1
        List<Entity> current_firework = new List<Entity>();
        Entity StarEffect = new Entity();

        // 2
        for (int i = 0; i < spawnCount; i++)
        {
            int random_number = UnityEngine.Random.Range(1, 6);
            switch (random_number)
            {
                case 1:
                    StarEffect = entityManager.Instantiate(entityPrefabBlue);
                    break;
                case 2:
                    StarEffect = entityManager.Instantiate(entityPrefabGreen);
                    break;
                case 3:
                    StarEffect = entityManager.Instantiate(entityPrefabOrange);
                    break;
                case 4:
                    StarEffect = entityManager.Instantiate(entityPrefabPink);
                    break;
                case 5:
                    StarEffect = entityManager.Instantiate(entityPrefabPurple);
                    break;
                default:
                    break;
            }
            current_firework.Add(StarEffect);
            entityCount++;
            CountTxt.text = entityCount.ToString();
            ParticleCountTxt.text = (entityCount * 25000).ToString();

            // 3
            entityManager.SetComponentData(StarEffect, new Translation
            {
                Value = position
            });;
            entityManager.SetComponentData(StarEffect, new Rotation
            {
                Value = UnityEngine.Random.rotation
            });

        }

        // 5
        StartCoroutine(ClearEntityList(current_firework));
    }
}
