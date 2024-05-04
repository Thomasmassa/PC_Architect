import requests
from bs4 import BeautifulSoup
import json
import time
import logging


file_name = input("geef file name: ")
filepath = file_name + ".json"
print("opgegeven file: " + filepath)

time.sleep(3)

logging.basicConfig(filename='scraperLogAlternate.log', level=logging.INFO, format='%(asctime)s %(message)s')

# Laad de JSON-gegevens
with open(filepath, 'r') as f:
    data = json.load(f)

if not data:
    logging.info('Geen gegevens gevonden')
    exit()
else:
    logging.info('Gegevens gevonden')

time.sleep(1)

count = 0

# Voor elke CPU in de JSON-gegevens
for part in data.values():
    if count == 0 and part['image'] == '':
        logging.info(part['name'])
        time.sleep(2)


        # Zoek de CPU op Alternate.be
        # partstring = part['name']+part['chipset']

        # others
        partstring = part['name']

        parsedstring = partstring.replace(' ', '+')
        logging.info(parsedstring)

        search_url = f"https://www.alternate.be/listing.xhtml?q={parsedstring}"
        response = requests.get(search_url)

        if response.status_code != 200:
            logging.info(f'Fout bij het ophalen van de zoekresultaten voor {part["name"]}')
            logging.info(f'response: {response}')
            continue
        else:
            logging.info(f'Zoekresultaten opgehaald voor {part["name"]}')
            logging.info(f'response: {response}')
        time.sleep(2)

        soup = BeautifulSoup(response.text, 'html.parser')
        logging.info(soup)
        time.sleep(2)

        img_tag = soup.find('img', attrs={'class': ['productPicture img-fluid m-auto']})
        logging.info(img_tag)
        time.sleep(2)

        if img_tag and 'src' in img_tag.attrs:
            img_url = f"https://www.alternate.be{img_tag['src']}"
            print(img_url)
            logging.info(img_url)
            part['image'] = img_url
        time.sleep(2)
        print("succeeded")

# Sla de bijgewerkte JSON-gegevens op
with open(filepath, 'w') as f:
    json.dump(data, f, indent=4)
print("done")