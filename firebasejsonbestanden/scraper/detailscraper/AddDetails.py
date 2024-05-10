import json

# while True:
#     if input("Wil je stoppen? (y/n): ") == 'y':
#         break

#     file_name = input("geef file name: ")
#     filepath = file_name + ".json"
#     print("opgegeven file: " + filepath)

#     # Open het JSON-bestand
#     with open(filepath, 'r') as f:
#         data = json.load(f)

#     # Loop door elk object en voeg het veld "details" toe
#     for key in data:
#         data[key]['socket'] = ''
 
#     # Schrijf de nieuwe data terug naar het bestand
#     with open(filepath, 'w') as f:
#         json.dump(data, f, indent=4)


# with open(filepath, 'r') as f:
#     data = json.load(f)

# # Loop door elk object en vervang de velden "speed" en "modules"
# for key in data:
#     speed = data[key]['speed']
#     modules = data[key]['modules']

#     data[key]['speed_type'] = speed[0]
#     data[key]['speed_value'] = speed[1]
#     data[key]['module_count'] = modules[0]
#     data[key]['module_size'] = modules[1]

#     # Verwijder de oude velden
#     del data[key]['speed']
#     del data[key]['modules']

# # Schrijf de nieuwe data terug naar het bestand
# with open(filepath, 'w') as f:
#     json.dump(data, f, indent=4)



while True:
    if input("Wil je stoppen? (y/n): ") == 'y':
        break

    file_name = input("geef file name: ")
    filepath = file_name + ".json"
    print("opgegeven file: " + filepath)

    # Open het JSON-bestand en laad de gegevens
    with open(filepath, 'r') as f:
        data = json.load(f)
    count = 1
    for item in data:
        data[item]['id'] = count
        count += 1

    # Schrijf de gegevens terug naar het JSON-bestand
    with open(filepath, 'w') as f:
        json.dump(data, f, indent=4)