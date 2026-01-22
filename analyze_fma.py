import csv

# Bones we care about from our game
game_bones = {
    "Femur", "Tibia", "Fibula", "Patella", "Humerus", "Radius", "Ulna",
    "Scaphoid", "Lunate", "Triquetrum", "Pisiform", "Trapezium", "Trapezoid", "Capitate", "Hamate",
    "Skull", "Mandible", "Sternum", "Clavicle", "Scapula", "Sacrum", "Coccyx",
    "Calcaneus", "Talus", "Navicular", "Cuboid", "Cuneiform",
    "Metacarpal", "Metatarsal", "Phalanx"
}

with open('FMA.csv', 'r', encoding='utf-8') as f:
    reader = csv.DictReader(f)
    headers = reader.fieldnames
    
    # Find indices of interesting columns
    print("Useful columns:")
    for i, h in enumerate(headers):
        if any(keyword in h.lower() for keyword in ['adjacent', 'articulate', 'located', 'part of', 'connect']):
            print(f"  {i}: {h}")
    
    print("\nSample bone entries:")
    count = 0
    for row in reader:
        name = row['Preferred Label']
        if any(bone in name for bone in ['Femur', 'Scaphoid', 'Humerus']):
            if 'left' not in name.lower() and 'right' not in name.lower() and count < 5:
                print(f"\n{name}:")
                print(f"  Definition: {row['Definitions'][:100] if row['Definitions'] else 'N/A'}")
                print(f"  Adjacent: {row['adjacent'][:50] if row['adjacent'] else 'N/A'}")
                print(f"  Adjacent to: {row['adjacent to'][:50] if row['adjacent to'] else 'N/A'}")
                count += 1
