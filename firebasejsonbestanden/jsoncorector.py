import json

def format_json_with_ids(file_path):
    with open(file_path, 'r') as file:
        data = json.load(file)

    formatted_data = {}
    for idx, obj in enumerate(data):
        formatted_obj = obj.copy()
        formatted_obj['image'] = ""
        formatted_data[str(idx)] = formatted_obj

    return formatted_data

# Bestandspad naar het JSON-bestand
input_file_path = "bug.json"
output_file_path = "gpu.json"

# Formateer de JSON-gegevens
formatted_data = format_json_with_ids(input_file_path)

# Schrijf de geformatteerde JSON-gegevens naar een nieuw bestand
with open(output_file_path, 'w') as output_file:
    json.dump(formatted_data, output_file, indent=2)
