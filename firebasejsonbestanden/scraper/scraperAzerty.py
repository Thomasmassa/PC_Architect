import requests
from bs4 import BeautifulSoup
import json
import time
import logging

file_name = input("geef file name: ")
filepath = file_name + ".json"
print("opgegeven file: " + filepath)

time.sleep(2)

logging.basicConfig(filename='scraperLogAzerty.log', level=logging.INFO, format='%(asctime)s %(message)s')
logging.warning('Dit is een waarschuwing')

# Laad de JSON-gegevens
with open(filepath, 'r') as f:
    data = json.load(f)

if not data:
    print("geen gegevens gevonden")
    logging.info('Geen gegevens gevonden')
    exit()
else:
    print("gegevens gevonden")
    logging.info('Gegevens gevonden')

time.sleep(2)

count = 0

# Voor elke CPU in de JSON-gegevens
for part in data.values():
    if count == 0 and part['image'] == '':

        logging.info(part['name'])
        time.sleep(2)


        # others
        partstring = part['name']

        parsedstring = partstring.replace(' ', '%')
        logging.info(parsedstring)

        search_url = f"https://azerty.nl/catalogsearch/result/?q={parsedstring}"
        print(f"Url {search_url}")
        logging.info(search_url)
        response = requests.get(search_url)

        if response.status_code != 200:
            logging.info(f'Fout bij het ophalen van de zoekresultaten voor {part["name"]}')
            logging.info(f'response: {response}')
            print(f"Fout bij het ophalen van de zoekresultaten voor {part["name"]}")
            print(f'response: {response}')
            continue
        else:
            logging.info(f'Zoekresultaten opgehaald voor {part["name"]}')
            logging.info(f'response: {response}')
        time.sleep(2)

        soup = BeautifulSoup(response.text, 'html.parser')
        logging.info(soup)
        time.sleep(2)

        img_tag = soup.find('img', attrs={'class': ['object-contain product-image-photo']})
        logging.info(img_tag)
        time.sleep(2)

        if img_tag and 'src' in img_tag.attrs:
            img_url = img_tag['src']
            print(img_url)
            logging.info(img_url)
            part['image'] = img_url
            print("succeeded")
        else:
            print("failed")
        time.sleep(2)

# Sla de bijgewerkte JSON-gegevens op
with open(filepath, 'w') as f:
    json.dump(data, f, indent=4)
print("done")