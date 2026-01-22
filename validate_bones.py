import re
import sys

# Read the BoneData.cs file
with open('Bonedle.Client/Data/BoneData.cs', 'r', encoding='utf-8') as f:
    content = f.read()

bone_pattern = r'new\("([^"]+)",\s*"([^"]+)",\s*"([^"]+)",\s*"([^"]+)",\s*"([^"]+)",\s*(\d+),\s*new List<string>\s*\{([^}]*)\}'

bones = {}
for match in re.finditer(bone_pattern, content):
    bone_id = match.group(1)
    display_name = match.group(2)
    division = match.group(3)
    region = match.group(4)
    bone_type = match.group(5)
    neighbors_str = match.group(7)
    
    neighbors = []
    if neighbors_str.strip():
        for n in neighbors_str.split(','):
            neighbor = n.strip().strip('"').strip()
            if neighbor:
                neighbors.append(neighbor)
    
    bones[bone_id] = {
        'name': display_name,
        'division': division,
        'region': region,
        'type': bone_type,
        'neighbors': neighbors
    }

print(f"Found {len(bones)} bones")

errors = []
warnings = []

# 1. Bidirectional adjacency check
for bone_id, bone_data in bones.items():
    for neighbor_id in bone_data['neighbors']:
        if neighbor_id not in bones:
            errors.append(f"ERROR: {bone_id} ({bone_data['name']}) references non-existent bone: {neighbor_id}")
        else:
            if bone_id not in bones[neighbor_id]['neighbors']:
                warnings.append(f"ASYMMETRIC: {bone_id} -> {neighbor_id}, but not reverse")

# 2. Known anatomical relationships
# Scaphoid should connect to radius
if 'scaphoid-left' in bones and 'radius-left' not in bones['scaphoid-left']['neighbors']:
    errors.append(f"ERROR: scaphoid-left should connect to radius-left")

if 'scaphoid-right' in bones and 'radius-right' not in bones['scaphoid-right']['neighbors']:
    errors.append(f"ERROR: scaphoid-right should connect to radius-right")

# 3. Check for isolated bones
for bone_id, bone_data in bones.items():
    if len(bone_data['neighbors']) == 0:
        warnings.append(f"ISOLATED: {bone_id} ({bone_data['name']}) has no neighbors")

# 4. Check hand bone chains
for i in range(1, 6):
    left_mc = f'metacarpal-{i}-left'
    if left_mc in bones:
        has_carpal = any(n in bones[left_mc]['neighbors'] for n in ['trapezium-left', 'trapezoid-left', 'capitate-left', 'hamate-left'])
        if not has_carpal:
            errors.append(f"ERROR: {left_mc} should connect to a carpal bone")

print("\n" + "="*60)
print("VALIDATION RESULTS")
print("="*60)

if errors:
    print(f"\n[ERRORS FOUND] ({len(errors)}):")
    for error in errors:
        print(f"  {error}")
else:
    print("\n[OK] No critical errors found")

if warnings:
    print(f"\n[WARNINGS] ({len(warnings)}):")
    for warning in warnings[:30]:
        print(f"  {warning}")
    if len(warnings) > 30:
        print(f"  ... and {len(warnings) - 30} more warnings")
else:
    print("\n[OK] No warnings")

asymmetric = [w for w in warnings if 'ASYMMETRIC' in w]
if asymmetric:
    print(f"\n[SUMMARY] {len(asymmetric)} asymmetric relationships found")
