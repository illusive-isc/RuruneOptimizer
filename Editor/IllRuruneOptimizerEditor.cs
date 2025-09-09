using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
#if AVATAR_OPTIMIZER_FOUND
using Anatawa12.AvatarOptimizer;
#endif

namespace jp.illusive_isc.RuruneOptimizer
{
    [CustomEditor(typeof(IllRuruneOptimizer))]
    [AddComponentMenu("")]
    internal class IllRuruneOptimizerEditor : Editor
    {
        SerializedProperty petFlg;
        SerializedProperty ClothFlg;
        SerializedProperty HairFlg;
        SerializedProperty TailFlg;
        SerializedProperty TPSFlg;
        SerializedProperty ClairvoyanceFlg;
        SerializedProperty colliderJumpFlg;
        SerializedProperty pictureFlg;
        SerializedProperty BreastSizeFlg;
        SerializedProperty LightGunFlg;
        SerializedProperty WhiteBreathFlg;
        SerializedProperty BubbleBreathFlg;
        SerializedProperty WaterStampFlg;
        SerializedProperty eightBitFlg;
        SerializedProperty PenCtrlFlg;
        SerializedProperty HeartGunFlg;
        SerializedProperty FaceGestureFlg;
        SerializedProperty FaceLockFlg;
        SerializedProperty FaceValFlg;
        SerializedProperty kamitukiFlg;
        SerializedProperty nadeFlg;
        SerializedProperty blinkFlg;
        SerializedProperty controller;
        SerializedProperty menu;
        SerializedProperty param;
        SerializedProperty controllerDef;
        SerializedProperty menuDef;
        SerializedProperty paramDef;
        SerializedProperty IKUSIA_emote;
        SerializedProperty heelFlg1;
        SerializedProperty heelFlg2;
        SerializedProperty ClothFlg1;
        SerializedProperty ClothFlg2;
        SerializedProperty ClothFlg3;
        SerializedProperty ClothFlg4;
        SerializedProperty ClothFlg5;
        SerializedProperty ClothFlg6;
        SerializedProperty ClothFlg7;
        SerializedProperty ClothFlg8;
        SerializedProperty TailFlg1;
        SerializedProperty BreastSizeFlg1;
        SerializedProperty BreastSizeFlg2;
        SerializedProperty BreastSizeFlg3;
        SerializedProperty IKUSIA_emote1;
        SerializedProperty HairFlg10;
        SerializedProperty HairFlg11;
        SerializedProperty HairFlg12;
        SerializedProperty HairFlg20;
        SerializedProperty HairFlg22;
        SerializedProperty HairFlg30;
        SerializedProperty HairFlg40;
        SerializedProperty HairFlg50;
        SerializedProperty HairFlg51;
        SerializedProperty HairFlg60;
        SerializedProperty questFlg1;

        SerializedProperty Skirt_Root;
        SerializedProperty Breast;
        SerializedProperty backhair;
        SerializedProperty back_side_root;
        SerializedProperty Head_002;
        SerializedProperty Front_hair2_root;
        SerializedProperty side_1_root;
        SerializedProperty hair_2;
        SerializedProperty sidehair;
        SerializedProperty side_3_root;
        SerializedProperty Side_root;
        SerializedProperty tail_044;
        SerializedProperty tail_022;

        SerializedProperty chest_collider1;
        SerializedProperty chest_collider2;
        SerializedProperty upperleg_collider1;
        SerializedProperty upperleg_collider2;
        SerializedProperty upperleg_collider3;
        SerializedProperty upperArm_collider;
        SerializedProperty plane_collider;
        SerializedProperty head_collider1;
        SerializedProperty head_collider2;
        SerializedProperty Breast_collider;
        SerializedProperty plane_tail_collider;
        SerializedProperty textureResize;
        SerializedProperty AAORemoveFlg;
        bool questArea;

        // PB情報とコライダー情報のクラス定義（namespace内、Editorクラス外に移動）
        public class PhysBoneInfo
        {
            public int AffectedCount; //:Transform数
            public int Count; //:Transform数
            public int ColliderCount; //:Collider数
        }

        public static readonly Dictionary<string, PhysBoneInfo> physBoneList = new()
        {
            {
                "Breast",
                new PhysBoneInfo { AffectedCount = 6, ColliderCount = 0 }
            },
            {
                "back_side_root",
                new PhysBoneInfo { AffectedCount = 9, ColliderCount = 0 }
            },
            {
                "Head_002",
                new PhysBoneInfo { AffectedCount = 4, ColliderCount = 0 }
            },
            {
                "Front_hair2_root",
                new PhysBoneInfo { AffectedCount = 10, ColliderCount = 0 }
            },
            {
                "side_1_root",
                new PhysBoneInfo { AffectedCount = 15, ColliderCount = 0 }
            },
            {
                "hair_2",
                new PhysBoneInfo { AffectedCount = 10, ColliderCount = 0 }
            },
            {
                "sidehair",
                new PhysBoneInfo { AffectedCount = 6, ColliderCount = 0 }
            },
            {
                "side_3_root",
                new PhysBoneInfo { AffectedCount = 37, ColliderCount = 0 }
            },
            {
                "tail_022",
                new PhysBoneInfo { AffectedCount = 10, ColliderCount = 0 }
            },
            {
                "tail_044",
                new PhysBoneInfo { AffectedCount = 18, ColliderCount = 13 }
            },
            {
                "Side_root",
                new PhysBoneInfo { AffectedCount = 13, ColliderCount = 20 }
            },
            {
                "backhair",
                new PhysBoneInfo { AffectedCount = 20, ColliderCount = 18 }
            },
            {
                "Skirt_Root",
                new PhysBoneInfo { AffectedCount = 42, ColliderCount = 60 }
            },
        };

        private void OnEnable()
        {
            // フィールド名は元のクラスの変数名と一致させる
            petFlg = serializedObject.FindProperty("petFlg");
            ClothFlg = serializedObject.FindProperty("ClothFlg");
            HairFlg = serializedObject.FindProperty("HairFlg");
            TailFlg = serializedObject.FindProperty("TailFlg");
            TPSFlg = serializedObject.FindProperty("TPSFlg");
            ClairvoyanceFlg = serializedObject.FindProperty("ClairvoyanceFlg");
            colliderJumpFlg = serializedObject.FindProperty("colliderJumpFlg");
            pictureFlg = serializedObject.FindProperty("pictureFlg");
            BreastSizeFlg = serializedObject.FindProperty("BreastSizeFlg");
            LightGunFlg = serializedObject.FindProperty("LightGunFlg");
            WhiteBreathFlg = serializedObject.FindProperty("WhiteBreathFlg");
            BubbleBreathFlg = serializedObject.FindProperty("BubbleBreathFlg");
            WaterStampFlg = serializedObject.FindProperty("WaterStampFlg");
            eightBitFlg = serializedObject.FindProperty("eightBitFlg");
            PenCtrlFlg = serializedObject.FindProperty("PenCtrlFlg");
            HeartGunFlg = serializedObject.FindProperty("HeartGunFlg");
            FaceGestureFlg = serializedObject.FindProperty("FaceGestureFlg");
            FaceLockFlg = serializedObject.FindProperty("FaceLockFlg");
            FaceValFlg = serializedObject.FindProperty("FaceValFlg");
            kamitukiFlg = serializedObject.FindProperty("kamitukiFlg");
            nadeFlg = serializedObject.FindProperty("nadeFlg");
            blinkFlg = serializedObject.FindProperty("blinkFlg");
            controller = serializedObject.FindProperty("controller");
            menu = serializedObject.FindProperty("menu");
            param = serializedObject.FindProperty("param");
            controllerDef = serializedObject.FindProperty("controllerDef");
            menuDef = serializedObject.FindProperty("menuDef");
            paramDef = serializedObject.FindProperty("paramDef");
            IKUSIA_emote = serializedObject.FindProperty("IKUSIA_emote");
            heelFlg1 = serializedObject.FindProperty("heelFlg1");
            heelFlg2 = serializedObject.FindProperty("heelFlg2");
            ClothFlg1 = serializedObject.FindProperty("ClothFlg1");
            ClothFlg2 = serializedObject.FindProperty("ClothFlg2");
            ClothFlg3 = serializedObject.FindProperty("ClothFlg3");
            ClothFlg4 = serializedObject.FindProperty("ClothFlg4");
            ClothFlg5 = serializedObject.FindProperty("ClothFlg5");
            ClothFlg6 = serializedObject.FindProperty("ClothFlg6");
            ClothFlg7 = serializedObject.FindProperty("ClothFlg7");
            ClothFlg8 = serializedObject.FindProperty("ClothFlg8");
            TailFlg1 = serializedObject.FindProperty("TailFlg1");
            BreastSizeFlg1 = serializedObject.FindProperty("BreastSizeFlg1");
            BreastSizeFlg2 = serializedObject.FindProperty("BreastSizeFlg2");
            BreastSizeFlg3 = serializedObject.FindProperty("BreastSizeFlg3");
            IKUSIA_emote1 = serializedObject.FindProperty("IKUSIA_emote1");
            HairFlg10 = serializedObject.FindProperty("HairFlg1");
            HairFlg11 = serializedObject.FindProperty("HairFlg11");
            HairFlg12 = serializedObject.FindProperty("HairFlg12");
            HairFlg20 = serializedObject.FindProperty("HairFlg2");
            HairFlg22 = serializedObject.FindProperty("HairFlg22");
            HairFlg30 = serializedObject.FindProperty("HairFlg3");
            HairFlg40 = serializedObject.FindProperty("HairFlg4");
            HairFlg50 = serializedObject.FindProperty("HairFlg5");
            HairFlg51 = serializedObject.FindProperty("HairFlg51");
            HairFlg60 = serializedObject.FindProperty("HairFlg6");
            questFlg1 = serializedObject.FindProperty("questFlg1");

            Skirt_Root = serializedObject.FindProperty("Skirt_Root");
            Breast = serializedObject.FindProperty("Breast");
            backhair = serializedObject.FindProperty("backhair");
            back_side_root = serializedObject.FindProperty("back_side_root");
            Head_002 = serializedObject.FindProperty("Head_002");
            Front_hair2_root = serializedObject.FindProperty("Front_hair2_root");
            side_1_root = serializedObject.FindProperty("side_1_root");
            hair_2 = serializedObject.FindProperty("hair_2");
            sidehair = serializedObject.FindProperty("sidehair");
            side_3_root = serializedObject.FindProperty("side_3_root");
            Side_root = serializedObject.FindProperty("Side_root");
            tail_044 = serializedObject.FindProperty("tail_044");
            tail_022 = serializedObject.FindProperty("tail_022");
            chest_collider1 = serializedObject.FindProperty("chest_collider1");
            chest_collider2 = serializedObject.FindProperty("chest_collider2");
            upperleg_collider1 = serializedObject.FindProperty("upperleg_collider1");
            upperleg_collider2 = serializedObject.FindProperty("upperleg_collider2");
            upperleg_collider3 = serializedObject.FindProperty("upperleg_collider3");
            upperArm_collider = serializedObject.FindProperty("upperArm_collider");
            plane_collider = serializedObject.FindProperty("plane_collider");
            head_collider1 = serializedObject.FindProperty("head_collider1");
            head_collider2 = serializedObject.FindProperty("head_collider2");
            Breast_collider = serializedObject.FindProperty("Breast_collider");
            plane_tail_collider = serializedObject.FindProperty("plane_tail_collider");
            textureResize = serializedObject.FindProperty("textureResize");
            AAORemoveFlg = serializedObject.FindProperty("AAORemoveFlg");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            EditorGUILayout.PropertyField(heelFlg1, new GUIContent("ヒールON"));
            EditorGUILayout.PropertyField(heelFlg2, new GUIContent("ハイヒールON"));

            EditorGUILayout.PropertyField(ClothFlg, new GUIContent("衣装メニューのみ削除"));
            if (!ClothFlg.boolValue)
            {
                GUI.enabled =
                    ClothFlg1.boolValue =
                    ClothFlg2.boolValue =
                    ClothFlg3.boolValue =
                    ClothFlg4.boolValue =
                    ClothFlg5.boolValue =
                    ClothFlg6.boolValue =
                    ClothFlg7.boolValue =
                    ClothFlg8.boolValue =
                        false;
            }
            EditorGUILayout.PropertyField(ClothFlg1, new GUIContent("  ├ ジャケット削除"));

            EditorGUILayout.PropertyField(ClothFlg2, new GUIContent("  ├ シャツ＆スカート削除"));
            EditorGUILayout.PropertyField(ClothFlg3, new GUIContent("  ├ アクセサリ削除"));
            EditorGUILayout.PropertyField(ClothFlg4, new GUIContent("  ├ string削除"));
            EditorGUILayout.PropertyField(ClothFlg5, new GUIContent("  ├ グローブ削除"));
            EditorGUILayout.PropertyField(ClothFlg6, new GUIContent("  ├ ソックス削除"));
            EditorGUILayout.PropertyField(ClothFlg7, new GUIContent("  ├ 靴削除"));
            EditorGUILayout.PropertyField(ClothFlg8, new GUIContent("  └ 下着削除"));
            GUI.enabled = true;
            EditorGUILayout.PropertyField(BreastSizeFlg, new GUIContent("バストサイズ変更削除"));
            if (!BreastSizeFlg.boolValue)
            {
                GUI.enabled = false;
                BreastSizeFlg1.boolValue = false;
                BreastSizeFlg2.boolValue = false;
                BreastSizeFlg3.boolValue = false;
            }
            EditorGUILayout.PropertyField(BreastSizeFlg1, new GUIContent("  ├ smallにする"));
            EditorGUILayout.PropertyField(BreastSizeFlg2, new GUIContent("  ├ 100にする"));
            EditorGUILayout.PropertyField(BreastSizeFlg3, new GUIContent("  └ 瑞希100にする"));
            GUI.enabled = true;
            {
                var RuruneOptimizer = (IllRuruneOptimizer)target;
                if (BreastSizeFlg1.boolValue != RuruneOptimizer.BreastSizeFlg1)
                {
                    BreastSizeFlg2.boolValue = false;
                    BreastSizeFlg3.boolValue = false;
                }
                else if (BreastSizeFlg2.boolValue != RuruneOptimizer.BreastSizeFlg2)
                {
                    BreastSizeFlg1.boolValue = false;
                    BreastSizeFlg3.boolValue = false;
                }
                else if (BreastSizeFlg3.boolValue != RuruneOptimizer.BreastSizeFlg3)
                {
                    BreastSizeFlg1.boolValue = false;
                    BreastSizeFlg2.boolValue = false;
                }
            }
            EditorGUILayout.PropertyField(
                HairFlg,
                new GUIContent("髪毛/ヘッドフォンをメニューから削除")
            );
            if (!HairFlg.boolValue)
                GUI.enabled =
                    HairFlg10.boolValue =
                    HairFlg11.boolValue =
                    HairFlg20.boolValue =
                    HairFlg12.boolValue =
                    HairFlg30.boolValue =
                    HairFlg60.boolValue =
                    HairFlg50.boolValue =
                    HairFlg51.boolValue =
                    HairFlg40.boolValue =
                        false;
            if (HairFlg60.boolValue)
                GUI.enabled =
                    HairFlg10.boolValue =
                    HairFlg11.boolValue =
                    HairFlg20.boolValue =
                    HairFlg12.boolValue =
                    HairFlg30.boolValue =
                        false;
            EditorGUILayout.PropertyField(HairFlg10, new GUIContent("  │   ├ ぱっつんON"));
            if (!HairFlg10.boolValue)
                GUI.enabled = HairFlg11.boolValue = false;
            EditorGUILayout.PropertyField(HairFlg11, new GUIContent("  │   │   └ ショートON"));
            if (HairFlg.boolValue && !HairFlg60.boolValue)
                GUI.enabled = true;
            EditorGUILayout.PropertyField(HairFlg20, new GUIContent("  │   ├ 前髪左分けON"));
            {
                var RuruneOptimizer = (IllRuruneOptimizer)target;
                if (HairFlg10.boolValue != RuruneOptimizer.HairFlg1)
                    HairFlg20.boolValue = false;
                else if (HairFlg20.boolValue != RuruneOptimizer.HairFlg2)
                    HairFlg10.boolValue = false;
            }

            EditorGUILayout.PropertyField(HairFlg22, new GUIContent("  │   ├ 髪留めON"));
            EditorGUILayout.PropertyField(HairFlg12, new GUIContent("  │   ├ 前髪サイドON"));
            EditorGUILayout.PropertyField(HairFlg30, new GUIContent("  │   └ サイドON"));
            if (HairFlg.boolValue)
                GUI.enabled = true;
            EditorGUILayout.PropertyField(HairFlg60, new GUIContent("  └ hair削除"));
            EditorGUILayout.PropertyField(HairFlg50, new GUIContent("      ├ ヘッドホン削除"));
            if (!HairFlg50.boolValue)
                GUI.enabled = HairFlg51.boolValue = false;
            EditorGUILayout.PropertyField(HairFlg51, new GUIContent("      │   └ particle削除"));
            if (HairFlg.boolValue)
                GUI.enabled = true;
            EditorGUILayout.PropertyField(HairFlg40, new GUIContent("      └ hair2削除"));
            GUI.enabled = true;
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            EditorGUILayout.PropertyField(TailFlg, new GUIContent("尻尾削除"));
            EditorGUILayout.PropertyField(TailFlg1, new GUIContent("  └ リボン削除"));
            if (TailFlg.boolValue)
                TailFlg1.boolValue = true;
            EditorGUILayout.PropertyField(petFlg, new GUIContent("ペット削除"));
            EditorGUILayout.PropertyField(TPSFlg, new GUIContent("TPS削除"));
            EditorGUILayout.PropertyField(ClairvoyanceFlg, new GUIContent("透視削除"));
            EditorGUILayout.PropertyField(
                colliderJumpFlg,
                new GUIContent("コライダー・ジャンプ削除")
            );
            EditorGUILayout.PropertyField(pictureFlg, new GUIContent("撮影ギミック削除"));
            EditorGUILayout.PropertyField(LightGunFlg, new GUIContent("ライトガン削除"));
            EditorGUILayout.PropertyField(WhiteBreathFlg, new GUIContent("ホワイトブレス削除"));
            EditorGUILayout.PropertyField(BubbleBreathFlg, new GUIContent("バブルブレス削除"));
            EditorGUILayout.PropertyField(WaterStampFlg, new GUIContent("ウォータースタンプ削除"));
            EditorGUILayout.PropertyField(eightBitFlg, new GUIContent("8bit削除"));
            EditorGUILayout.PropertyField(PenCtrlFlg, new GUIContent("ペン操作削除"));
            EditorGUILayout.PropertyField(HeartGunFlg, new GUIContent("ハートガン削除"));
            EditorGUILayout.PropertyField(
                FaceGestureFlg,
                new GUIContent("デフォルトの表情プリセット削除(faceEmoなど使う場合)")
            );
            EditorGUILayout.PropertyField(FaceLockFlg, new GUIContent("表情固定機能削除"));
            EditorGUILayout.PropertyField(FaceValFlg, new GUIContent("顔差分変更機能削除"));
            EditorGUILayout.PropertyField(
                blinkFlg,
                new GUIContent("まばたきをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                nadeFlg,
                new GUIContent("なでギミックをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                kamitukiFlg,
                new GUIContent("噛みつきをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                IKUSIA_emote,
                new GUIContent("IKUSIA_emoteをメニューのみ削除")
            );
            if (!IKUSIA_emote.boolValue)
            {
                GUI.enabled = IKUSIA_emote1.boolValue = false;
            }
            EditorGUILayout.PropertyField(IKUSIA_emote1, new GUIContent("  └ 手動のAFKは残す"));
            GUI.enabled = true;

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            questArea = EditorGUILayout.Foldout(questArea, "Quest用調整項目(素体のみ)", true);
            if (questArea)
            {
                var ruruneOptimizer = (IllRuruneOptimizer)target;
#if AVATAR_OPTIMIZER_FOUND
                if (ruruneOptimizer.transform.root.GetComponent<TraceAndOptimize>() == null)
                    EditorGUILayout.HelpBox(
                        "アバターにTraceAndOptimizeを追加してください",
                        MessageType.Error
                    );
#else
                EditorGUILayout.HelpBox(
                    "AvatarOptimizerが見つかりませんVCCに追加して有効化してください",
                    MessageType.Error
                );
#endif
                EditorGUILayout.HelpBox(
                    "Quest化に対応してないコンポーネントやシェーダーを使っているためペット、TPS、透視、コライダー・ジャンプ、撮影ギミック、ライトガン、ホワイトブレス、バブルブレス、ウォータースタンプ、8bit、ペン操作、ハートガン、ヘッドホンのparticle、AFKの演出の一部を削除します。\n"
                        + "",
                    MessageType.Info
                );
                EditorGUILayout.PropertyField(questFlg1, new GUIContent("quest用にギミックを削除"));

                if (questFlg1.boolValue)
                {
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                    petFlg.boolValue = true;
                    TPSFlg.boolValue = true;
                    ClairvoyanceFlg.boolValue = true;
                    colliderJumpFlg.boolValue = true;
                    pictureFlg.boolValue = true;
                    LightGunFlg.boolValue = true;
                    WhiteBreathFlg.boolValue = true;
                    BubbleBreathFlg.boolValue = true;
                    WaterStampFlg.boolValue = true;
                    eightBitFlg.boolValue = true;
                    PenCtrlFlg.boolValue = true;
                    HeartGunFlg.boolValue = true;
                    HairFlg51.boolValue = true;
                    serializedObject.ApplyModifiedProperties();
                }
                if (GUILayout.Button("おすすめ設定にする"))
                {
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                    Head_002.boolValue = false;
                    Front_hair2_root.boolValue = true;
                    side_3_root.boolValue = true;
                    Side_root.boolValue = false;
                    Breast_collider.boolValue = true;
                    backhair.boolValue = false;
                    plane_collider.boolValue = true;
                    head_collider1.boolValue = false;
                    upperArm_collider.boolValue = true;
                    upperleg_collider1.boolValue = true;
                    chest_collider1.boolValue = true;
                    hair_2.boolValue = true;
                    Breast.boolValue = false;
                    side_1_root.boolValue = true;
                    sidehair.boolValue = true;
                    back_side_root.boolValue = true;
                    Skirt_Root.boolValue = true;
                    upperleg_collider2.boolValue = true;
                    tail_044.boolValue = false;
                    head_collider2.boolValue = true;
                    chest_collider2.boolValue = false;
                    upperleg_collider3.boolValue = false;
                    plane_tail_collider.boolValue = true;

                    tail_022.boolValue = true;

                    serializedObject.ApplyModifiedProperties();
                }

                if (questFlg1.boolValue)
                {
                    if (HairFlg.boolValue)
                    {
                        Head_002.boolValue = true;
                        Front_hair2_root.boolValue = true;
                        side_3_root.boolValue = true;
                        Side_root.boolValue = true;
                        backhair.boolValue = true;
                        side_1_root.boolValue = true;
                        sidehair.boolValue = true;
                        back_side_root.boolValue = true;
                    }
                    if (HairFlg40.boolValue)
                        hair_2.boolValue = true;
                    if (ClothFlg2.boolValue)
                        Skirt_Root.boolValue = true;

                    if (TailFlg.boolValue)
                        tail_044.boolValue = true;
                    if (TailFlg1.boolValue)
                        tail_022.boolValue = true;
                }
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                GUILayout.BeginVertical();
                EditorGUILayout.PropertyField(
                    Head_002,
                    new GUIContent("前髪:Transform: " + physBoneList["Head_002"].AffectedCount)
                );
                EditorGUILayout.PropertyField(
                    Front_hair2_root,
                    new GUIContent(
                        "ぱっつん前髪:Transform : " + physBoneList["Front_hair2_root"].AffectedCount
                    )
                );
                {
                    if (!HairFlg60.boolValue)
                        if (Head_002.boolValue != ruruneOptimizer.Head_002)
                            Front_hair2_root.boolValue = false;
                        else if (Front_hair2_root.boolValue != ruruneOptimizer.Front_hair2_root)
                            Head_002.boolValue = false;
                }

                EditorGUILayout.PropertyField(
                    side_3_root,
                    new GUIContent(
                        "前髪サイド:Transform : " + physBoneList["side_3_root"].AffectedCount
                    )
                );
                EditorGUILayout.PropertyField(
                    Side_root,
                    new GUIContent("サイド:Transform : " + physBoneList["Side_root"].AffectedCount)
                );
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                GUILayout.BeginVertical();
                EditorGUILayout.PropertyField(
                    Breast_collider,
                    new GUIContent("胸部干渉 : " + physBoneList["Side_root"].ColliderCount)
                );
                if (Side_root.boolValue)
                    Breast_collider.boolValue = true;
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                EditorGUILayout.PropertyField(
                    backhair,
                    new GUIContent("後ろ髪:Transform : " + physBoneList["backhair"].AffectedCount)
                );
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                GUILayout.BeginVertical();
                EditorGUILayout.PropertyField(
                    plane_collider,
                    new GUIContent("髪の地面干渉 : " + physBoneList["backhair"].ColliderCount)
                );
                EditorGUILayout.PropertyField(
                    head_collider1,
                    new GUIContent("頭部干渉 : " + physBoneList["backhair"].ColliderCount)
                );
                EditorGUILayout.PropertyField(
                    upperArm_collider,
                    new GUIContent("腕干渉 : " + physBoneList["backhair"].ColliderCount * 2)
                );
                EditorGUILayout.PropertyField(
                    upperleg_collider1,
                    new GUIContent("脚干渉 : " + physBoneList["backhair"].ColliderCount * 2)
                );
                EditorGUILayout.PropertyField(
                    chest_collider1,
                    new GUIContent("胸周り干渉 : " + physBoneList["backhair"].ColliderCount)
                );
                if (backhair.boolValue)
                {
                    plane_collider.boolValue = true;
                    head_collider1.boolValue = true;
                    upperArm_collider.boolValue = true;
                    upperleg_collider1.boolValue = true;
                    chest_collider1.boolValue = true;
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                EditorGUILayout.PropertyField(
                    hair_2,
                    new GUIContent("hair_2:Transform : " + physBoneList["hair_2"].AffectedCount)
                );
                EditorGUILayout.PropertyField(
                    Breast,
                    new GUIContent("胸:Transform : " + physBoneList["Breast"].AffectedCount)
                );

                EditorGUILayout.PropertyField(
                    side_1_root,
                    new GUIContent(
                        "前髪小:Transform : " + physBoneList["side_1_root"].AffectedCount
                    )
                );
                EditorGUILayout.PropertyField(
                    sidehair,
                    new GUIContent("横髪小:Transform : " + physBoneList["sidehair"].AffectedCount)
                );
                EditorGUILayout.PropertyField(
                    back_side_root,
                    new GUIContent(
                        "後ろ髪小:Transform : " + physBoneList["back_side_root"].AffectedCount
                    )
                );

                EditorGUILayout.PropertyField(
                    Skirt_Root,
                    new GUIContent(
                        "スカート:Transform : " + physBoneList["Skirt_Root"].AffectedCount
                    )
                );
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                GUILayout.BeginVertical();
                EditorGUILayout.PropertyField(
                    upperleg_collider2,
                    new GUIContent("脚干渉 : " + physBoneList["Skirt_Root"].ColliderCount)
                );
                if (Skirt_Root.boolValue)
                {
                    upperleg_collider2.boolValue = true;
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                EditorGUILayout.PropertyField(
                    tail_044,
                    new GUIContent("尻尾:Transform : " + physBoneList["tail_044"].AffectedCount)
                );
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                GUILayout.BeginVertical();
                EditorGUILayout.PropertyField(
                    head_collider2,
                    new GUIContent("頭部干渉 : " + physBoneList["tail_044"].ColliderCount)
                );
                EditorGUILayout.PropertyField(
                    chest_collider2,
                    new GUIContent("胸周り干渉 : " + physBoneList["tail_044"].ColliderCount)
                );
                EditorGUILayout.PropertyField(
                    upperleg_collider3,
                    new GUIContent("脚干渉 : " + physBoneList["tail_044"].ColliderCount * 2)
                );
                EditorGUILayout.PropertyField(
                    plane_tail_collider,
                    new GUIContent("尻尾の地面干渉 : " + physBoneList["tail_044"].ColliderCount)
                );
                if (tail_044.boolValue)
                {
                    head_collider2.boolValue = true;
                    chest_collider2.boolValue = true;
                    upperleg_collider3.boolValue = true;
                    plane_tail_collider.boolValue = true;
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                EditorGUILayout.PropertyField(
                    tail_022,
                    new GUIContent(
                        "尻尾リボン:Transform : " + physBoneList["tail_022"].AffectedCount
                    )
                );
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                int count = 200;
                if (Head_002.boolValue)
                    count -= physBoneList["Head_002"].AffectedCount;
                if (Front_hair2_root.boolValue)
                    count -= physBoneList["Front_hair2_root"].AffectedCount;
                if (side_1_root.boolValue)
                    count -= physBoneList["side_1_root"].AffectedCount;
                if (side_3_root.boolValue)
                    count -= physBoneList["side_3_root"].AffectedCount;
                if (Side_root.boolValue)
                    count -= physBoneList["Side_root"].AffectedCount;
                if (backhair.boolValue)
                    count -= physBoneList["backhair"].AffectedCount;
                if (back_side_root.boolValue)
                    count -= physBoneList["back_side_root"].AffectedCount;
                if (sidehair.boolValue)
                    count -= physBoneList["sidehair"].AffectedCount;
                if (hair_2.boolValue)
                    count -= physBoneList["hair_2"].AffectedCount;
                if (Breast.boolValue)
                    count -= physBoneList["Breast"].AffectedCount;
                if (Skirt_Root.boolValue)
                    count -= physBoneList["Skirt_Root"].AffectedCount;
                if (tail_044.boolValue)
                    count -= physBoneList["tail_044"].AffectedCount;
                if (tail_022.boolValue)
                    count -= physBoneList["tail_022"].AffectedCount;
                if (count > 64)
                    EditorGUILayout.HelpBox(
                        "影響transform数 :" + count + "/64 (64以下に調整してください)",
                        MessageType.Error
                    );
                else
                    EditorGUILayout.HelpBox("影響transform数 :" + count + "/64", MessageType.Info);

                int count2 = 271;
                if (Breast_collider.boolValue)
                    count2 -= physBoneList["Side_root"].ColliderCount;

                if (plane_collider.boolValue)
                    count2 -= physBoneList["backhair"].ColliderCount;
                if (head_collider1.boolValue)
                    count2 -= physBoneList["Side_root"].ColliderCount;
                if (upperArm_collider.boolValue)
                    count2 -= physBoneList["backhair"].ColliderCount * 2;
                if (upperleg_collider1.boolValue)
                    count2 -= physBoneList["backhair"].ColliderCount * 2;
                if (chest_collider1.boolValue)
                    count2 -= physBoneList["Side_root"].ColliderCount;

                if (chest_collider2.boolValue)
                    count2 -= physBoneList["tail_044"].ColliderCount;
                if (upperleg_collider3.boolValue)
                    count2 -= physBoneList["tail_044"].ColliderCount * 2;
                if (plane_tail_collider.boolValue)
                    count2 -= physBoneList["tail_044"].ColliderCount;
                if (head_collider2.boolValue)
                    count2 -= physBoneList["tail_044"].ColliderCount;

                if (upperleg_collider2.boolValue)
                    count2 -= physBoneList["Skirt_Root"].ColliderCount;

                if (count2 > 64)
                    EditorGUILayout.HelpBox(
                        "コライダー干渉数 :" + count2 + "/64 (64以下に調整してください)",
                        MessageType.Error
                    );
                else
                    EditorGUILayout.HelpBox(
                        "コライダー干渉数 :" + count2 + "/64",
                        MessageType.Info
                    );
                int selected = textureResize.enumValueIndex;
                textureResize.enumValueIndex = EditorGUILayout.Popup(
                    "メニュー画像解像度設定",
                    selected,
                    new[] { "下げる", "削除" }
                );

#if !AVATAR_OPTIMIZER_FOUND
                GUI.enabled = false;
                EditorGUILayout.HelpBox(
                    "AAOがインストールされている場合のみ「頬染めを削除」が有効になります。",
                    MessageType.Info
                );
#endif
                EditorGUILayout.PropertyField(AAORemoveFlg, new GUIContent("頬染めを削除"));
                GUI.enabled = true;
            }

            // Execute ボタンの追加
            if (GUILayout.Button("Execute"))
            {
                IllRuruneOptimizer script = (IllRuruneOptimizer)target;
                VRCAvatarDescriptor descriptor =
                    script.transform.root.GetComponent<VRCAvatarDescriptor>();
                if (descriptor != null)
                {
                    try
                    {
                        script.Execute(descriptor);
                    }
                    catch (System.Exception)
                    {
                        Debug.LogWarning("変換に失敗しました。再実行します。");
                        script.Execute(descriptor);
                    }
                }
                else
                {
                    Debug.LogWarning("VRCAvatarDescriptor が見つかりません。");
                }
            }
            EditorGUILayout.Space();
            GUILayout.TextField(
                "生成する元Asset",
                new GUIStyle
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 24,
                    normal = new GUIStyleState { textColor = Color.white },
                }
            );
            GUI.enabled = false;
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(controllerDef, new GUIContent("Animator Controller"));
            EditorGUILayout.PropertyField(menuDef, new GUIContent("Expressions Menu"));
            EditorGUILayout.PropertyField(paramDef, new GUIContent("Expression Parameters"));
            GUI.enabled = true;
            EditorGUILayout.Space();
            GUILayout.TextField(
                "生成されたAsset",
                new GUIStyle
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 24,
                    normal = new GUIStyleState { textColor = Color.white },
                }
            );
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(controller, new GUIContent("Animator Controller"));
            EditorGUILayout.PropertyField(menu, new GUIContent("Expressions Menu"));
            EditorGUILayout.PropertyField(param, new GUIContent("Expression Parameters"));

            // 変更内容の適用
            serializedObject.ApplyModifiedProperties();
        }

        // ▼ 右クリックメニューで作成できるようにするためのメニュー項目 ▼

        // Validate メソッドで対象が VRC アバターのルートであり、さらにアタッチされている Animator の avatar が "ruruneAvatar" であることをチェック
        [MenuItem("GameObject/illusive_tools/Create IllRuruneOptimizer Object", true)]
        private static bool ValidateCreateIllRuruneOptimizerObject(MenuCommand menuCommand)
        {
            GameObject contextGO = menuCommand.context as GameObject;
            if (contextGO == null)
            {
                contextGO = Selection.activeGameObject;
            }
            if (contextGO == null)
            {
                // どちらも null ならエラーを出すか、false を返す
                return false;
            }
            // 対象が VRCAvatarDescriptor を持っているか
            if (contextGO.GetComponent<VRCAvatarDescriptor>() == null)
                return false;
            // さらに、親に VRCAvatarDescriptor が存在しない（＝ルートである）かをチェック
            if (
                contextGO.transform.parent != null
                && contextGO.transform.parent.GetComponent<VRCAvatarDescriptor>() != null
            )
                return false;
            // Animator コンポーネントが存在し、その avatar プロパティの名前が "ruruneAvatar" であるかをチェック
            Animator animator = contextGO.GetComponent<Animator>();
            if (animator == null)
                return false;
            if (animator.avatar == null)
                return false;
            if (animator.avatar.name != "ruruneAvatar")
                return false;
            return true;
        }

        // 対象が条件を満たす場合のみ、メニュー項目が有効となる
        [MenuItem("GameObject/illusive_tools/Create IllRuruneOptimizer Object", false, 10)]
        private static void CreateIllRuruneOptimizerObject(MenuCommand menuCommand)
        {
            // 新しい GameObject を作成し、IllRuruneOptimizer コンポーネントを追加
            GameObject go = new GameObject("IllRuruneOptimizer");
            go.AddComponent<IllRuruneOptimizer>();

            // 右クリックで選択されたオブジェクト（VRCアバターのルート）の子として配置
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
    }
}
