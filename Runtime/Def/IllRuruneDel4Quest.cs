#if UNITY_EDITOR
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.Dynamics;
#if AVATAR_OPTIMIZER_FOUND
using Anatawa12.AvatarOptimizer;
#endif
namespace jp.illusive_isc.RuruneOptimizer
{
    [AddComponentMenu("")]
    public class IllRuruneDel4Quest : ScriptableObject
    {
        public static void Edit4Quest(VRCAvatarDescriptor descriptor, IllRuruneOptimizer optimizer)
        {
            if (optimizer.questFlg1)
            {
                var AFK_World = descriptor.transform.Find("Advanced/AFK_World/position");

                IllRuruneParam.DestroyObj(AFK_World.Find("water2"));
                IllRuruneParam.DestroyObj(AFK_World.Find("water3"));
                IllRuruneParam.DestroyObj(AFK_World.Find("AFKIN Particle"));
                IllRuruneParam.DestroyObj(AFK_World.Find("swim"));
                IllRuruneParam.DestroyObj(AFK_World.Find("IdolParticle"));

                if (optimizer.Skirt_Root)
                    DelPBByPathArray(
                        descriptor,
                        new string[]
                        {
                            "Armature/Hips/Skirt_Root/Skirt_Root_L",
                            "Armature/Hips/Skirt_Root/Skirt_Root_R",
                        }
                    );

                if (optimizer.Breast)
                {
                    DelPBByPathArray(
                        descriptor,
                        new string[]
                        {
                            "Armature/Hips/Spine/Chest/Breast_L",
                            "Armature/Hips/Spine/Chest/Breast_R",
                        }
                    );
                }
                if (optimizer.backhair)
                {
                    DelPBByPathArray(
                        descriptor,
                        new string[]
                        {
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_L/backhair_L",
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_R/backhair_R",
                        }
                    );
                }
                if (optimizer.back_side_root)
                {
                    DelPBByPathArray(
                        descriptor,
                        new string[]
                        {
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_side_root",
                        }
                    );
                }
                if (optimizer.Head_002)
                {
                    DelPBByPathArray(
                        descriptor,
                        new string[]
                        {
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Front_hair1_root/Head.002",
                        }
                    );
                }
                if (optimizer.Front_hair2_root)
                {
                    DelPBByPathArray(
                        descriptor,
                        new string[]
                        {
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Front_hair2_root",
                        }
                    );
                }
                if (optimizer.side_1_root)
                {
                    DelPBByPathArray(
                        descriptor,
                        new string[] { "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side_1_root" }
                    );
                }
                if (optimizer.sidehair)
                {
                    DelPBByPathArray(
                        descriptor,
                        new string[]
                        {
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side_2_root/side_ani_L/sidehair_L.003",
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side_2_root/side_ani_R/sidehair_R.003",
                        }
                    );
                }
                if (optimizer.sidehair)
                {
                    DelPBByPathArray(
                        descriptor,
                        new string[]
                        {
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side_2_root/sidehair_L",
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side_2_root/sidehair_R",
                        }
                    );
                }
                if (optimizer.side_3_root)
                {
                    DelPBByPathArray(
                        descriptor,
                        new string[] { "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side_3_root" }
                    );
                }
                if (optimizer.Side_root)
                {
                    DelPBByPathArray(
                        descriptor,
                        new string[]
                        {
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Side_root",
                            "Armature/Hips/Spine/Chest/Breast_L",
                            "Armature/Hips/Spine/Chest/Breast_R",
                        }
                    );
                }
                if (optimizer.tail_044)
                {
                    DelPBByPathArray(descriptor, new string[] { "Armature/Hips/tail/tail.044" });
                }
                if (optimizer.tail_022)
                {
                    DelPBByPathArray(
                        descriptor,
                        new string[]
                        {
                            "Armature/Hips/tail/tail.044/tail.001/tail.002/tail.003/tail.004/tail.005/tail.006/tail.007/tail.008/tail.009/tail.010/tail.011/tail.012/tail.013/tail.014/tail.018/tail.021/tail.022",
                            "Armature/Hips/tail/tail.044/tail.001/tail.002/tail.003/tail.004/tail.005/tail.006/tail.007/tail.008/tail.009/tail.010/tail.011/tail.012/tail.013/tail.014/tail.018/tail.021/tail.024",
                        }
                    );
                }
                if (optimizer.chest_collider1)
                {
                    DelColliderSettingByPathArray(
                        descriptor,
                        new string[] { "chest_collider" },
                        new string[]
                        {
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_R/backhair_R",
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_L/backhair_L",
                        }
                    );
                }
                if (optimizer.chest_collider2)
                {
                    DelColliderSettingByPathArray(
                        descriptor,
                        new string[] { "chest_collider" },
                        new string[] { "Armature/Hips/tail/tail.044" }
                    );
                }

                if (optimizer.chest_collider1 && optimizer.chest_collider2)
                    IllRuruneParam.DestroyComponent<VRCPhysBoneColliderBase>(
                        descriptor.transform.Find("Armature/Hips/Spine/Chest/chest_collider")
                    );
                if (optimizer.upperleg_collider1)
                {
                    DelColliderSettingByPathArray(
                        descriptor,
                        new string[] { "upperleg_L_collider", "upperleg_R_collider" },
                        new string[]
                        {
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_R/backhair_R",
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_L/backhair_L",
                        }
                    );
                }
                if (optimizer.upperleg_collider2)
                {
                    DelColliderSettingByPathArray(
                        descriptor,
                        new string[] { "upperleg_L_collider", "upperleg_R_collider" },
                        new string[]
                        {
                            "Armature/Hips/Skirt_Root/Skirt_Root_R",
                            "Armature/Hips/Skirt_Root/Skirt_Root_L",
                        }
                    );
                }
                if (optimizer.upperleg_collider3)
                {
                    DelColliderSettingByPathArray(
                        descriptor,
                        new string[] { "upperleg_L_collider", "upperleg_R_collider" },
                        new string[]
                        {
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_R/backhair_R",
                        }
                    );
                }
                if (
                    optimizer.upperleg_collider1
                    && optimizer.upperleg_collider2
                    && optimizer.upperleg_collider3
                )
                {
                    DelPBColliderByPathArray(
                        descriptor,
                        new string[]
                        {
                            "Armature/Hips/Upperleg_L/upperleg_L_collider",
                            "Armature/Hips/Upperleg_R/upperleg_R_collider",
                        }
                    );
                }
                if (optimizer.upperArm_collider)
                {
                    DelPBColliderByPathArray(
                        descriptor,
                        new string[]
                        {
                            "Armature/Hips/Spine/Chest/Shoulder_L/Upperarm_L/upperArm_L_collider",
                            "Armature/Hips/Spine/Chest/Shoulder_R/Upperarm_R/upperArm_R_collider",
                        }
                    );
                }
                if (optimizer.plane_collider)
                    IllRuruneParam.DestroyComponent<VRCPhysBoneColliderBase>(
                        descriptor.transform.Find("Armature/plane_collider")
                    );
                if (optimizer.head_collider1)
                {
                    DelColliderSettingByPathArray(
                        descriptor,
                        new string[] { "head_collider" },
                        new string[]
                        {
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_L/backhair_L",
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_R/backhair_R",
                        }
                    );
                }
                if (optimizer.head_collider2)
                {
                    DelColliderSettingByPathArray(
                        descriptor,
                        new string[] { "head_collider" },
                        new string[] { "Armature/Hips/tail/tail.044" }
                    );
                }
                if (optimizer.head_collider1 && optimizer.head_collider2)
                    IllRuruneParam.DestroyComponent<VRCPhysBoneColliderBase>(
                        descriptor.transform.Find(
                            "Armature/Hips/Spine/Chest/Neck/Head/head_collider"
                        )
                    );
                if (optimizer.Breast_collider)
                {
                    DelPBColliderByPathArray(
                        descriptor,
                        new string[]
                        {
                            "Armature/Hips/Spine/Chest/Breast_R",
                            "Armature/Hips/Spine/Chest/Breast_L",
                        }
                    );
                }
                if (optimizer.plane_tail_collider)
                    IllRuruneParam.DestroyComponent<VRCPhysBoneColliderBase>(
                        descriptor.transform.Find("Armature/plane_tail_collider")
                    );
            }

            if (optimizer.AAORemoveFlg)
            {
#if AVATAR_OPTIMIZER_FOUND
                if (
                    !descriptor
                        .transform.Find("Body")
                        .TryGetComponent<RemoveMeshByBlendShape>(out var removeMesh)
                )
                {
                    removeMesh = descriptor
                        .transform.Find("Body")
                        .gameObject.AddComponent<RemoveMeshByBlendShape>();
                    removeMesh.Initialize(1);
                }
                removeMesh.ShapeKeys.Add("照れ");
#endif
            }
        }

        private static void DelPBByPathArray(VRCAvatarDescriptor descriptor, string[] paths)
        {
            foreach (var path in paths)
            {
                IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(descriptor.transform.Find(path));
            }
        }

        private static void DelPBColliderByPathArray(VRCAvatarDescriptor descriptor, string[] paths)
        {
            foreach (var path in paths)
            {
                IllRuruneParam.DestroyComponent<VRCPhysBoneColliderBase>(
                    descriptor.transform.Find(path)
                );
            }
        }

        public static void DelColliderSettingByPathArray(
            VRCAvatarDescriptor descriptor,
            string[] colliderNames,
            string[] pbPaths
        )
        {
            foreach (var pbPath in pbPaths)
            {
                if (descriptor.transform.Find(pbPath))
                {
                    var physBone = descriptor
                        .transform.Find(pbPath)
                        .GetComponent<VRCPhysBoneBase>();
                    if (physBone != null && physBone.colliders != null)
                    {
                        foreach (var colliderName in colliderNames)
                        {
                            for (int i = physBone.colliders.Count - 1; i >= 0; i--)
                            {
                                var collider = physBone.colliders[i];
                                if (collider != null && collider.name.Contains(colliderName))
                                {
                                    physBone.colliders.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
#endif
