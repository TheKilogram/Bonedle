using Bonedle.Client.Models;

namespace Bonedle.Client.Data;

public static class BoneData
{
    public static List<Bone> GetAllBones()
    {
        var bones = new List<Bone>();
        bones.AddRange(GetMajorBones());
        bones.AddRange(GetAdditionalFullBones());
        return bones;
    }

    public static List<Bone> GetMajorBones()
    {
        return new List<Bone>
        {
            // HEAD
            new("skull", "Skull", "axial", "head", "flat", 1,
                new List<string> { "mandible", "cervical-vertebrae" }, true, 50, 5),
            new("mandible", "Mandible", "axial", "head", "irregular", 1,
                new List<string> { "skull" }, true, 50, 10),

            // SPINE
            new("cervical-vertebrae", "Cervical Vertebrae", "axial", "spine", "irregular", 7,
                new List<string> { "skull", "thoracic-vertebrae", "clavicle-left", "clavicle-right" }, true, 50, 15),
            new("thoracic-vertebrae", "Thoracic Vertebrae", "axial", "spine", "irregular", 12,
                new List<string> { "cervical-vertebrae", "lumbar-vertebrae", "ribs-left", "ribs-right", "scapula-left", "scapula-right" }, true, 50, 28),
            new("lumbar-vertebrae", "Lumbar Vertebrae", "axial", "spine", "irregular", 5,
                new List<string> { "thoracic-vertebrae", "sacrum" }, true, 50, 42),
            new("sacrum", "Sacrum", "axial", "spine", "irregular", 1,
                new List<string> { "lumbar-vertebrae", "coccyx", "hip-bone-left", "hip-bone-right" }, true, 50, 50),
            new("coccyx", "Coccyx", "axial", "spine", "irregular", 1,
                new List<string> { "sacrum" }, true, 50, 55),

            // TORSO
            new("sternum", "Sternum", "axial", "torso", "flat", 1,
                new List<string> { "ribs-left", "ribs-right", "clavicle-left", "clavicle-right" }, true, 50, 25),
            new("ribs-left", "Ribs (Left)", "axial", "torso", "flat", 12,
                new List<string> { "sternum", "thoracic-vertebrae" }, true, 40, 30),
            new("ribs-right", "Ribs (Right)", "axial", "torso", "flat", 12,
                new List<string> { "sternum", "thoracic-vertebrae" }, true, 60, 30),

            // SHOULDER GIRDLE
            new("clavicle-left", "Clavicle (Left)", "appendicular", "torso", "long", 1,
                new List<string> { "sternum", "scapula-left", "cervical-vertebrae" }, true, 35, 18),
            new("clavicle-right", "Clavicle (Right)", "appendicular", "torso", "long", 1,
                new List<string> { "sternum", "scapula-right", "cervical-vertebrae" }, true, 65, 18),
            new("scapula-left", "Scapula (Left)", "appendicular", "torso", "flat", 1,
                new List<string> { "clavicle-left", "humerus-left", "thoracic-vertebrae" }, true, 30, 22),
            new("scapula-right", "Scapula (Right)", "appendicular", "torso", "flat", 1,
                new List<string> { "clavicle-right", "humerus-right", "thoracic-vertebrae" }, true, 70, 22),

            // LEFT ARM
            new("humerus-left", "Humerus (Left)", "appendicular", "arm", "long", 1,
                new List<string> { "scapula-left", "radius-left", "ulna-left" }, true, 25, 32),
            new("radius-left", "Radius (Left)", "appendicular", "arm", "long", 1,
                new List<string> { "humerus-left", "ulna-left", "carpals-left" }, true, 22, 45),
            new("ulna-left", "Ulna (Left)", "appendicular", "arm", "long", 1,
                new List<string> { "humerus-left", "radius-left", "carpals-left" }, true, 26, 45),

            // RIGHT ARM
            new("humerus-right", "Humerus (Right)", "appendicular", "arm", "long", 1,
                new List<string> { "scapula-right", "radius-right", "ulna-right" }, true, 75, 32),
            new("radius-right", "Radius (Right)", "appendicular", "arm", "long", 1,
                new List<string> { "humerus-right", "ulna-right", "carpals-right" }, true, 78, 45),
            new("ulna-right", "Ulna (Right)", "appendicular", "arm", "long", 1,
                new List<string> { "humerus-right", "radius-right", "carpals-right" }, true, 74, 45),

            // LEFT HAND
            new("carpals-left", "Carpals (Left)", "appendicular", "hand", "short", 8,
                new List<string> { "radius-left", "ulna-left", "metacarpals-left" }, true, 20, 55),
            new("metacarpals-left", "Metacarpals (Left)", "appendicular", "hand", "long", 5,
                new List<string> { "carpals-left", "phalanges-hand-left" }, true, 18, 60),
            new("phalanges-hand-left", "Phalanges (Left Hand)", "appendicular", "hand", "long", 14,
                new List<string> { "metacarpals-left" }, true, 16, 67),

            // RIGHT HAND
            new("carpals-right", "Carpals (Right)", "appendicular", "hand", "short", 8,
                new List<string> { "radius-right", "ulna-right", "metacarpals-right" }, true, 80, 55),
            new("metacarpals-right", "Metacarpals (Right)", "appendicular", "hand", "long", 5,
                new List<string> { "carpals-right", "phalanges-hand-right" }, true, 82, 60),
            new("phalanges-hand-right", "Phalanges (Right Hand)", "appendicular", "hand", "long", 14,
                new List<string> { "metacarpals-right" }, true, 84, 67),

            // PELVIS
            new("hip-bone-left", "Hip Bone (Left)", "appendicular", "pelvis", "flat", 1,
                new List<string> { "sacrum", "femur-left" }, true, 40, 52),
            new("hip-bone-right", "Hip Bone (Right)", "appendicular", "pelvis", "flat", 1,
                new List<string> { "sacrum", "femur-right" }, true, 60, 52),

            // LEFT LEG
            new("femur-left", "Femur (Left)", "appendicular", "leg", "long", 1,
                new List<string> { "hip-bone-left", "patella-left", "tibia-left" }, true, 42, 65),
            new("patella-left", "Patella (Left)", "appendicular", "leg", "sesamoid", 1,
                new List<string> { "femur-left", "tibia-left" }, true, 42, 73),
            new("tibia-left", "Tibia (Left)", "appendicular", "leg", "long", 1,
                new List<string> { "femur-left", "patella-left", "fibula-left", "tarsals-left" }, true, 44, 82),
            new("fibula-left", "Fibula (Left)", "appendicular", "leg", "long", 1,
                new List<string> { "tibia-left", "tarsals-left" }, true, 40, 82),

            // RIGHT LEG
            new("femur-right", "Femur (Right)", "appendicular", "leg", "long", 1,
                new List<string> { "hip-bone-right", "patella-right", "tibia-right" }, true, 58, 65),
            new("patella-right", "Patella (Right)", "appendicular", "leg", "sesamoid", 1,
                new List<string> { "femur-right", "tibia-right" }, true, 58, 73),
            new("tibia-right", "Tibia (Right)", "appendicular", "leg", "long", 1,
                new List<string> { "femur-right", "patella-right", "fibula-right", "tarsals-right" }, true, 56, 82),
            new("fibula-right", "Fibula (Right)", "appendicular", "leg", "long", 1,
                new List<string> { "tibia-right", "tarsals-right" }, true, 60, 82),

            // LEFT FOOT
            new("tarsals-left", "Tarsals (Left)", "appendicular", "foot", "short", 7,
                new List<string> { "tibia-left", "fibula-left", "metatarsals-left" }, true, 42, 92),
            new("metatarsals-left", "Metatarsals (Left)", "appendicular", "foot", "long", 5,
                new List<string> { "tarsals-left", "phalanges-foot-left" }, true, 40, 95),
            new("phalanges-foot-left", "Phalanges (Left Foot)", "appendicular", "foot", "long", 14,
                new List<string> { "metatarsals-left" }, true, 38, 98),

            // RIGHT FOOT
            new("tarsals-right", "Tarsals (Right)", "appendicular", "foot", "short", 7,
                new List<string> { "tibia-right", "fibula-right", "metatarsals-right" }, true, 58, 92),
            new("metatarsals-right", "Metatarsals (Right)", "appendicular", "foot", "long", 5,
                new List<string> { "tarsals-right", "phalanges-foot-right" }, true, 60, 95),
            new("phalanges-foot-right", "Phalanges (Right Foot)", "appendicular", "foot", "long", 14,
                new List<string> { "metatarsals-right" }, true, 62, 98),
        };
    }

    public static List<Bone> GetAdditionalFullBones()
    {
        // Additional bones for full skeleton mode - these extend the major bones
        // with more specific individual bones
        return new List<Bone>
        {
            // Individual skull bones
            new("frontal", "Frontal Bone", "axial", "head", "flat", 1,
                new List<string> { "parietal-left", "parietal-right", "sphenoid", "ethmoid", "nasal-left", "nasal-right" }, false, 50, 3),
            new("parietal-left", "Parietal (Left)", "axial", "head", "flat", 1,
                new List<string> { "frontal", "temporal-left", "occipital", "sphenoid" }, false, 45, 4),
            new("parietal-right", "Parietal (Right)", "axial", "head", "flat", 1,
                new List<string> { "frontal", "temporal-right", "occipital", "sphenoid" }, false, 55, 4),
            new("temporal-left", "Temporal (Left)", "axial", "head", "irregular", 1,
                new List<string> { "parietal-left", "occipital", "sphenoid", "zygomatic-left" }, false, 42, 6),
            new("temporal-right", "Temporal (Right)", "axial", "head", "irregular", 1,
                new List<string> { "parietal-right", "occipital", "sphenoid", "zygomatic-right" }, false, 58, 6),
            new("occipital", "Occipital Bone", "axial", "head", "flat", 1,
                new List<string> { "parietal-left", "parietal-right", "temporal-left", "temporal-right", "atlas" }, false, 50, 7),
            new("sphenoid", "Sphenoid Bone", "axial", "head", "irregular", 1,
                new List<string> { "frontal", "parietal-left", "parietal-right", "temporal-left", "temporal-right", "ethmoid" }, false, 50, 5),
            new("ethmoid", "Ethmoid Bone", "axial", "head", "irregular", 1,
                new List<string> { "frontal", "sphenoid", "nasal-left", "nasal-right" }, false, 50, 6),
            new("nasal-left", "Nasal (Left)", "axial", "head", "flat", 1,
                new List<string> { "frontal", "ethmoid", "maxilla-left" }, false, 48, 7),
            new("nasal-right", "Nasal (Right)", "axial", "head", "flat", 1,
                new List<string> { "frontal", "ethmoid", "maxilla-right" }, false, 52, 7),
            new("maxilla-left", "Maxilla (Left)", "axial", "head", "irregular", 1,
                new List<string> { "nasal-left", "zygomatic-left", "palatine-left" }, false, 47, 8),
            new("maxilla-right", "Maxilla (Right)", "axial", "head", "irregular", 1,
                new List<string> { "nasal-right", "zygomatic-right", "palatine-right" }, false, 53, 8),
            new("zygomatic-left", "Zygomatic (Left)", "axial", "head", "irregular", 1,
                new List<string> { "temporal-left", "maxilla-left", "frontal" }, false, 44, 7),
            new("zygomatic-right", "Zygomatic (Right)", "axial", "head", "irregular", 1,
                new List<string> { "temporal-right", "maxilla-right", "frontal" }, false, 56, 7),
            new("lacrimal-left", "Lacrimal (Left)", "axial", "head", "flat", 1,
                new List<string> { "frontal", "ethmoid", "maxilla-left" }, false, 47, 6),
            new("lacrimal-right", "Lacrimal (Right)", "axial", "head", "flat", 1,
                new List<string> { "frontal", "ethmoid", "maxilla-right" }, false, 53, 6),
            new("palatine-left", "Palatine (Left)", "axial", "head", "irregular", 1,
                new List<string> { "maxilla-left", "sphenoid" }, false, 48, 9),
            new("palatine-right", "Palatine (Right)", "axial", "head", "irregular", 1,
                new List<string> { "maxilla-right", "sphenoid" }, false, 52, 9),
            new("vomer", "Vomer", "axial", "head", "flat", 1,
                new List<string> { "ethmoid", "palatine-left", "palatine-right" }, false, 50, 8),
            new("inferior-nasal-concha-left", "Inferior Nasal Concha (Left)", "axial", "head", "irregular", 1,
                new List<string> { "ethmoid", "maxilla-left" }, false, 48, 7),
            new("inferior-nasal-concha-right", "Inferior Nasal Concha (Right)", "axial", "head", "irregular", 1,
                new List<string> { "ethmoid", "maxilla-right" }, false, 52, 7),

            // Ear bones (ossicles)
            new("malleus-left", "Malleus (Left)", "axial", "head", "irregular", 1,
                new List<string> { "incus-left" }, false, 42, 6),
            new("malleus-right", "Malleus (Right)", "axial", "head", "irregular", 1,
                new List<string> { "incus-right" }, false, 58, 6),
            new("incus-left", "Incus (Left)", "axial", "head", "irregular", 1,
                new List<string> { "malleus-left", "stapes-left" }, false, 42, 6),
            new("incus-right", "Incus (Right)", "axial", "head", "irregular", 1,
                new List<string> { "malleus-right", "stapes-right" }, false, 58, 6),
            new("stapes-left", "Stapes (Left)", "axial", "head", "irregular", 1,
                new List<string> { "incus-left" }, false, 42, 6),
            new("stapes-right", "Stapes (Right)", "axial", "head", "irregular", 1,
                new List<string> { "incus-right" }, false, 58, 6),

            // Hyoid
            new("hyoid", "Hyoid Bone", "axial", "head", "irregular", 1,
                new List<string> { "mandible", "cervical-vertebrae" }, false, 50, 12),

            // Individual cervical vertebrae
            new("atlas", "Atlas (C1)", "axial", "spine", "irregular", 1,
                new List<string> { "occipital", "axis" }, false, 50, 13),
            new("axis", "Axis (C2)", "axial", "spine", "irregular", 1,
                new List<string> { "atlas", "c3" }, false, 50, 14),
            new("c3", "C3 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "axis", "c4" }, false, 50, 14.5),
            new("c4", "C4 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "c3", "c5" }, false, 50, 15),
            new("c5", "C5 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "c4", "c6" }, false, 50, 15.5),
            new("c6", "C6 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "c5", "c7" }, false, 50, 16),
            new("c7", "C7 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "c6", "t1" }, false, 50, 16.5),

            // Individual thoracic vertebrae
            new("t1", "T1 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "c7", "t2", "rib-1-left", "rib-1-right" }, false, 50, 18),
            new("t2", "T2 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "t1", "t3", "rib-2-left", "rib-2-right" }, false, 50, 20),
            new("t3", "T3 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "t2", "t4", "rib-3-left", "rib-3-right" }, false, 50, 22),
            new("t4", "T4 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "t3", "t5", "rib-4-left", "rib-4-right" }, false, 50, 24),
            new("t5", "T5 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "t4", "t6", "rib-5-left", "rib-5-right" }, false, 50, 26),
            new("t6", "T6 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "t5", "t7", "rib-6-left", "rib-6-right" }, false, 50, 28),
            new("t7", "T7 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "t6", "t8", "rib-7-left", "rib-7-right" }, false, 50, 30),
            new("t8", "T8 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "t7", "t9", "rib-8-left", "rib-8-right" }, false, 50, 32),
            new("t9", "T9 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "t8", "t10", "rib-9-left", "rib-9-right" }, false, 50, 34),
            new("t10", "T10 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "t9", "t11", "rib-10-left", "rib-10-right" }, false, 50, 36),
            new("t11", "T11 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "t10", "t12", "rib-11-left", "rib-11-right" }, false, 50, 38),
            new("t12", "T12 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "t11", "l1", "rib-12-left", "rib-12-right" }, false, 50, 40),

            // Individual lumbar vertebrae
            new("l1", "L1 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "t12", "l2" }, false, 50, 42),
            new("l2", "L2 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "l1", "l3" }, false, 50, 44),
            new("l3", "L3 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "l2", "l4" }, false, 50, 46),
            new("l4", "L4 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "l3", "l5" }, false, 50, 48),
            new("l5", "L5 Vertebra", "axial", "spine", "irregular", 1,
                new List<string> { "l4", "sacrum" }, false, 50, 50),

            // Individual ribs
            new("rib-1-left", "Rib 1 (Left)", "axial", "torso", "flat", 1,
                new List<string> { "t1", "sternum" }, false, 42, 19),
            new("rib-1-right", "Rib 1 (Right)", "axial", "torso", "flat", 1,
                new List<string> { "t1", "sternum" }, false, 58, 19),
            new("rib-2-left", "Rib 2 (Left)", "axial", "torso", "flat", 1,
                new List<string> { "t2", "sternum" }, false, 40, 21),
            new("rib-2-right", "Rib 2 (Right)", "axial", "torso", "flat", 1,
                new List<string> { "t2", "sternum" }, false, 60, 21),
            new("rib-3-left", "Rib 3 (Left)", "axial", "torso", "flat", 1,
                new List<string> { "t3", "sternum" }, false, 38, 23),
            new("rib-3-right", "Rib 3 (Right)", "axial", "torso", "flat", 1,
                new List<string> { "t3", "sternum" }, false, 62, 23),
            new("rib-4-left", "Rib 4 (Left)", "axial", "torso", "flat", 1,
                new List<string> { "t4", "sternum" }, false, 36, 25),
            new("rib-4-right", "Rib 4 (Right)", "axial", "torso", "flat", 1,
                new List<string> { "t4", "sternum" }, false, 64, 25),
            new("rib-5-left", "Rib 5 (Left)", "axial", "torso", "flat", 1,
                new List<string> { "t5", "sternum" }, false, 35, 27),
            new("rib-5-right", "Rib 5 (Right)", "axial", "torso", "flat", 1,
                new List<string> { "t5", "sternum" }, false, 65, 27),
            new("rib-6-left", "Rib 6 (Left)", "axial", "torso", "flat", 1,
                new List<string> { "t6", "sternum" }, false, 34, 29),
            new("rib-6-right", "Rib 6 (Right)", "axial", "torso", "flat", 1,
                new List<string> { "t6", "sternum" }, false, 66, 29),
            new("rib-7-left", "Rib 7 (Left)", "axial", "torso", "flat", 1,
                new List<string> { "t7", "sternum" }, false, 35, 31),
            new("rib-7-right", "Rib 7 (Right)", "axial", "torso", "flat", 1,
                new List<string> { "t7", "sternum" }, false, 65, 31),
            new("rib-8-left", "Rib 8 (Left)", "axial", "torso", "flat", 1,
                new List<string> { "t8", "rib-7-left" }, false, 36, 33),
            new("rib-8-right", "Rib 8 (Right)", "axial", "torso", "flat", 1,
                new List<string> { "t8", "rib-7-right" }, false, 64, 33),
            new("rib-9-left", "Rib 9 (Left)", "axial", "torso", "flat", 1,
                new List<string> { "t9", "rib-8-left" }, false, 37, 35),
            new("rib-9-right", "Rib 9 (Right)", "axial", "torso", "flat", 1,
                new List<string> { "t9", "rib-8-right" }, false, 63, 35),
            new("rib-10-left", "Rib 10 (Left)", "axial", "torso", "flat", 1,
                new List<string> { "t10", "rib-9-left" }, false, 38, 37),
            new("rib-10-right", "Rib 10 (Right)", "axial", "torso", "flat", 1,
                new List<string> { "t10", "rib-9-right" }, false, 62, 37),
            new("rib-11-left", "Rib 11 (Left)", "axial", "torso", "flat", 1,
                new List<string> { "t11" }, false, 40, 39),
            new("rib-11-right", "Rib 11 (Right)", "axial", "torso", "flat", 1,
                new List<string> { "t11" }, false, 60, 39),
            new("rib-12-left", "Rib 12 (Left)", "axial", "torso", "flat", 1,
                new List<string> { "t12" }, false, 42, 41),
            new("rib-12-right", "Rib 12 (Right)", "axial", "torso", "flat", 1,
                new List<string> { "t12" }, false, 58, 41),

            // Sternum parts
            new("manubrium", "Manubrium", "axial", "torso", "flat", 1,
                new List<string> { "body-of-sternum", "clavicle-left", "clavicle-right", "rib-1-left", "rib-1-right" }, false, 50, 20),
            new("body-of-sternum", "Body of Sternum", "axial", "torso", "flat", 1,
                new List<string> { "manubrium", "xiphoid-process" }, false, 50, 25),
            new("xiphoid-process", "Xiphoid Process", "axial", "torso", "flat", 1,
                new List<string> { "body-of-sternum" }, false, 50, 32),

            // Individual carpal bones - left
            new("scaphoid-left", "Scaphoid (Left)", "appendicular", "hand", "short", 1,
                new List<string> { "radius-left", "lunate-left", "trapezium-left", "trapezoid-left", "capitate-left" }, false, 21, 54),
            new("lunate-left", "Lunate (Left)", "appendicular", "hand", "short", 1,
                new List<string> { "radius-left", "scaphoid-left", "triquetrum-left", "capitate-left", "hamate-left" }, false, 22, 54),
            new("triquetrum-left", "Triquetrum (Left)", "appendicular", "hand", "short", 1,
                new List<string> { "lunate-left", "pisiform-left", "hamate-left" }, false, 23, 54),
            new("pisiform-left", "Pisiform (Left)", "appendicular", "hand", "sesamoid", 1,
                new List<string> { "triquetrum-left" }, false, 24, 54),
            new("trapezium-left", "Trapezium (Left)", "appendicular", "hand", "short", 1,
                new List<string> { "scaphoid-left", "metacarpal-1-left" }, false, 19, 56),
            new("trapezoid-left", "Trapezoid (Left)", "appendicular", "hand", "short", 1,
                new List<string> { "scaphoid-left", "metacarpal-2-left" }, false, 20, 56),
            new("capitate-left", "Capitate (Left)", "appendicular", "hand", "short", 1,
                new List<string> { "scaphoid-left", "lunate-left", "metacarpal-3-left" }, false, 21, 56),
            new("hamate-left", "Hamate (Left)", "appendicular", "hand", "short", 1,
                new List<string> { "lunate-left", "triquetrum-left", "metacarpal-4-left", "metacarpal-5-left" }, false, 22, 56),

            // Individual carpal bones - right
            new("scaphoid-right", "Scaphoid (Right)", "appendicular", "hand", "short", 1,
                new List<string> { "radius-right", "lunate-right", "trapezium-right", "trapezoid-right", "capitate-right" }, false, 79, 54),
            new("lunate-right", "Lunate (Right)", "appendicular", "hand", "short", 1,
                new List<string> { "radius-right", "scaphoid-right", "triquetrum-right", "capitate-right", "hamate-right" }, false, 78, 54),
            new("triquetrum-right", "Triquetrum (Right)", "appendicular", "hand", "short", 1,
                new List<string> { "lunate-right", "pisiform-right", "hamate-right" }, false, 77, 54),
            new("pisiform-right", "Pisiform (Right)", "appendicular", "hand", "sesamoid", 1,
                new List<string> { "triquetrum-right" }, false, 76, 54),
            new("trapezium-right", "Trapezium (Right)", "appendicular", "hand", "short", 1,
                new List<string> { "scaphoid-right", "metacarpal-1-right" }, false, 81, 56),
            new("trapezoid-right", "Trapezoid (Right)", "appendicular", "hand", "short", 1,
                new List<string> { "scaphoid-right", "metacarpal-2-right" }, false, 80, 56),
            new("capitate-right", "Capitate (Right)", "appendicular", "hand", "short", 1,
                new List<string> { "scaphoid-right", "lunate-right", "metacarpal-3-right" }, false, 79, 56),
            new("hamate-right", "Hamate (Right)", "appendicular", "hand", "short", 1,
                new List<string> { "lunate-right", "triquetrum-right", "metacarpal-4-right", "metacarpal-5-right" }, false, 78, 56),

            // Individual metacarpals - left
            new("metacarpal-1-left", "Metacarpal 1 (Left Thumb)", "appendicular", "hand", "long", 1,
                new List<string> { "trapezium-left", "proximal-phalanx-thumb-left" }, false, 17, 58),
            new("metacarpal-2-left", "Metacarpal 2 (Left)", "appendicular", "hand", "long", 1,
                new List<string> { "trapezoid-left", "proximal-phalanx-index-left" }, false, 18, 60),
            new("metacarpal-3-left", "Metacarpal 3 (Left)", "appendicular", "hand", "long", 1,
                new List<string> { "capitate-left", "proximal-phalanx-middle-left" }, false, 19, 61),
            new("metacarpal-4-left", "Metacarpal 4 (Left)", "appendicular", "hand", "long", 1,
                new List<string> { "hamate-left", "proximal-phalanx-ring-left" }, false, 20, 60),
            new("metacarpal-5-left", "Metacarpal 5 (Left)", "appendicular", "hand", "long", 1,
                new List<string> { "hamate-left", "proximal-phalanx-pinky-left" }, false, 21, 59),

            // Individual metacarpals - right
            new("metacarpal-1-right", "Metacarpal 1 (Right Thumb)", "appendicular", "hand", "long", 1,
                new List<string> { "trapezium-right", "proximal-phalanx-thumb-right" }, false, 83, 58),
            new("metacarpal-2-right", "Metacarpal 2 (Right)", "appendicular", "hand", "long", 1,
                new List<string> { "trapezoid-right", "proximal-phalanx-index-right" }, false, 82, 60),
            new("metacarpal-3-right", "Metacarpal 3 (Right)", "appendicular", "hand", "long", 1,
                new List<string> { "capitate-right", "proximal-phalanx-middle-right" }, false, 81, 61),
            new("metacarpal-4-right", "Metacarpal 4 (Right)", "appendicular", "hand", "long", 1,
                new List<string> { "hamate-right", "proximal-phalanx-ring-right" }, false, 80, 60),
            new("metacarpal-5-right", "Metacarpal 5 (Right)", "appendicular", "hand", "long", 1,
                new List<string> { "hamate-right", "proximal-phalanx-pinky-right" }, false, 79, 59),

            // Individual tarsal bones - left
            new("calcaneus-left", "Calcaneus (Left)", "appendicular", "foot", "short", 1,
                new List<string> { "talus-left", "cuboid-left" }, false, 44, 92),
            new("talus-left", "Talus (Left)", "appendicular", "foot", "short", 1,
                new List<string> { "tibia-left", "fibula-left", "calcaneus-left", "navicular-left" }, false, 42, 91),
            new("navicular-foot-left", "Navicular (Left Foot)", "appendicular", "foot", "short", 1,
                new List<string> { "talus-left", "cuneiform-medial-left", "cuneiform-intermediate-left", "cuneiform-lateral-left" }, false, 40, 93),
            new("cuboid-left", "Cuboid (Left)", "appendicular", "foot", "short", 1,
                new List<string> { "calcaneus-left", "cuneiform-lateral-left", "metatarsal-4-left", "metatarsal-5-left" }, false, 46, 93),
            new("cuneiform-medial-left", "Medial Cuneiform (Left)", "appendicular", "foot", "short", 1,
                new List<string> { "navicular-foot-left", "metatarsal-1-left" }, false, 38, 94),
            new("cuneiform-intermediate-left", "Intermediate Cuneiform (Left)", "appendicular", "foot", "short", 1,
                new List<string> { "navicular-foot-left", "metatarsal-2-left" }, false, 40, 94),
            new("cuneiform-lateral-left", "Lateral Cuneiform (Left)", "appendicular", "foot", "short", 1,
                new List<string> { "navicular-foot-left", "cuboid-left", "metatarsal-3-left" }, false, 42, 94),

            // Individual tarsal bones - right
            new("calcaneus-right", "Calcaneus (Right)", "appendicular", "foot", "short", 1,
                new List<string> { "talus-right", "cuboid-right" }, false, 56, 92),
            new("talus-right", "Talus (Right)", "appendicular", "foot", "short", 1,
                new List<string> { "tibia-right", "fibula-right", "calcaneus-right", "navicular-right" }, false, 58, 91),
            new("navicular-foot-right", "Navicular (Right Foot)", "appendicular", "foot", "short", 1,
                new List<string> { "talus-right", "cuneiform-medial-right", "cuneiform-intermediate-right", "cuneiform-lateral-right" }, false, 60, 93),
            new("cuboid-right", "Cuboid (Right)", "appendicular", "foot", "short", 1,
                new List<string> { "calcaneus-right", "cuneiform-lateral-right", "metatarsal-4-right", "metatarsal-5-right" }, false, 54, 93),
            new("cuneiform-medial-right", "Medial Cuneiform (Right)", "appendicular", "foot", "short", 1,
                new List<string> { "navicular-foot-right", "metatarsal-1-right" }, false, 62, 94),
            new("cuneiform-intermediate-right", "Intermediate Cuneiform (Right)", "appendicular", "foot", "short", 1,
                new List<string> { "navicular-foot-right", "metatarsal-2-right" }, false, 60, 94),
            new("cuneiform-lateral-right", "Lateral Cuneiform (Right)", "appendicular", "foot", "short", 1,
                new List<string> { "navicular-foot-right", "cuboid-right", "metatarsal-3-right" }, false, 58, 94),

            // Individual metatarsals - left
            new("metatarsal-1-left", "Metatarsal 1 (Left Big Toe)", "appendicular", "foot", "long", 1,
                new List<string> { "cuneiform-medial-left", "proximal-phalanx-big-toe-left" }, false, 36, 96),
            new("metatarsal-2-left", "Metatarsal 2 (Left)", "appendicular", "foot", "long", 1,
                new List<string> { "cuneiform-intermediate-left", "proximal-phalanx-2nd-toe-left" }, false, 38, 96),
            new("metatarsal-3-left", "Metatarsal 3 (Left)", "appendicular", "foot", "long", 1,
                new List<string> { "cuneiform-lateral-left", "proximal-phalanx-3rd-toe-left" }, false, 40, 96),
            new("metatarsal-4-left", "Metatarsal 4 (Left)", "appendicular", "foot", "long", 1,
                new List<string> { "cuboid-left", "proximal-phalanx-4th-toe-left" }, false, 42, 96),
            new("metatarsal-5-left", "Metatarsal 5 (Left)", "appendicular", "foot", "long", 1,
                new List<string> { "cuboid-left", "proximal-phalanx-5th-toe-left" }, false, 44, 96),

            // Individual metatarsals - right
            new("metatarsal-1-right", "Metatarsal 1 (Right Big Toe)", "appendicular", "foot", "long", 1,
                new List<string> { "cuneiform-medial-right", "proximal-phalanx-big-toe-right" }, false, 64, 96),
            new("metatarsal-2-right", "Metatarsal 2 (Right)", "appendicular", "foot", "long", 1,
                new List<string> { "cuneiform-intermediate-right", "proximal-phalanx-2nd-toe-right" }, false, 62, 96),
            new("metatarsal-3-right", "Metatarsal 3 (Right)", "appendicular", "foot", "long", 1,
                new List<string> { "cuneiform-lateral-right", "proximal-phalanx-3rd-toe-right" }, false, 60, 96),
            new("metatarsal-4-right", "Metatarsal 4 (Right)", "appendicular", "foot", "long", 1,
                new List<string> { "cuboid-right", "proximal-phalanx-4th-toe-right" }, false, 58, 96),
            new("metatarsal-5-right", "Metatarsal 5 (Right)", "appendicular", "foot", "long", 1,
                new List<string> { "cuboid-right", "proximal-phalanx-5th-toe-right" }, false, 56, 96),

            // Hip bone components
            new("ilium-left", "Ilium (Left)", "appendicular", "pelvis", "flat", 1,
                new List<string> { "sacrum", "ischium-left", "pubis-left" }, false, 38, 48),
            new("ilium-right", "Ilium (Right)", "appendicular", "pelvis", "flat", 1,
                new List<string> { "sacrum", "ischium-right", "pubis-right" }, false, 62, 48),
            new("ischium-left", "Ischium (Left)", "appendicular", "pelvis", "irregular", 1,
                new List<string> { "ilium-left", "pubis-left" }, false, 40, 55),
            new("ischium-right", "Ischium (Right)", "appendicular", "pelvis", "irregular", 1,
                new List<string> { "ilium-right", "pubis-right" }, false, 60, 55),
            new("pubis-left", "Pubis (Left)", "appendicular", "pelvis", "irregular", 1,
                new List<string> { "ilium-left", "ischium-left", "pubis-right" }, false, 45, 56),
            new("pubis-right", "Pubis (Right)", "appendicular", "pelvis", "irregular", 1,
                new List<string> { "ilium-right", "ischium-right", "pubis-left" }, false, 55, 56),
        };
    }
}
