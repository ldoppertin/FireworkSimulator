using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Transforms;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Mesh unitMesh;
    [SerializeField]
    private List<Material> unitMaterials;
    [SerializeField]
    private GameObject gameObjectPrefab;

    private List<Entity> entityPrefabs;
    private Entity entityPrefab1;
    private Entity entityPrefab2;
    private Entity entityPrefab3;
    private World defaultWorld;
    private EntityManager entityManager;

    // Start is called before the first frame update
    void Start()
    {
        defaultWorld = World.DefaultGameObjectInjectionWorld;
        entityManager = defaultWorld.EntityManager;
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(defaultWorld, null);

        gameObjectPrefab.GetComponent<Renderer>().material = unitMaterials[0];
        entityPrefab1 = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPrefab, settings);
        gameObjectPrefab.GetComponent<Renderer>().material = unitMaterials[1];
        entityPrefab2 = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPrefab, settings);
        gameObjectPrefab.GetComponent<Renderer>().material = unitMaterials[2];
        entityPrefab3 = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPrefab, settings);

        InstantiateOnUnitSphereEntity(300, 300, entityPrefab1);
        InstantiateOnUnitSphereEntity(600, 400, entityPrefab2);
        InstantiateOnUnitSphereEntity(2000, 500, entityPrefab3);
    }

    private void InstantiateEntity(float3 position, Entity entityPrefab)
    {
        Entity myEntity = entityManager.Instantiate(entityPrefab);

        entityManager.SetComponentData(myEntity, new Translation
        {
            Value = position
        });
    }

    private void InstantiateOnUnitSphereEntity(int entityCount, float entityDistance, Entity entityPrefab)
    {
        for (int i = 0; i < entityCount; i++)
        {
            InstantiateEntity(UnityEngine.Random.onUnitSphere * entityDistance, entityPrefab);
        }
    }


    private void MakeEntity()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        Entity myEntity = entityManager.CreateEntity();
    }

}
