using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

// 1
public class MovementSystem : ComponentSystem
{
    // 2
    protected override void OnUpdate()
    {
        // 3
        Entities.WithAll<Movement>().ForEach((ref Translation trans, ref Rotation rot, ref Movement moveForward) =>
        {
            // 4
            trans.Value += moveForward.speed * Time.DeltaTime * math.forward(rot.Value);
        });

    }
}
