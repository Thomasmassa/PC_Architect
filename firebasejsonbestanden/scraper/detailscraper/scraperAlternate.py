import requests
from bs4 import BeautifulSoup
import json
import time
import logging
import re

bestandspad = ""
bestandGesloten = True
while bestandGesloten:
    try:
        with open(bestandspad, 'r') as f:
            data = json.load(f)
        if data:
            print("Bestand geladen")
            bestandGesloten = False
    except FileNotFoundError:
        print("Opgegeven bestand: " + bestandspad)
        bestandsnaam = input("Geef bestandsnaam: ")
        bestandspad = bestandsnaam + ".json"

if input("Debugmodus? (j/n): ") == "j":
    debugmode = True
else:
    debugmode = False
print("Debugmodus: " + str(debugmode))

time.sleep(3)

logging.basicConfig(filename='scraperLogAlternate.log', level=logging.INFO, format='%(asctime)s %(message)s')    

if not data:
    logging.info('Geen gegevens gevonden')
    exit()
else:
    logging.info('Gegevens gevonden')

time.sleep(1)

counter = 0

def search_url(url):
    time.sleep(5)
    response = requests.get(url)

    if response.status_code != 200:
        logging.info(f'Fout bij het ophalen van de zoekresultaten voor {part["name"]}')
        logging.info(f'response: {response}')
        print("Kon geen reactie krijgen")
        time.sleep(5)
        exit()
    else:
        logging.info(f'Zoekresultaten opgehaald voor {part["name"]}')
        logging.info(f'response: {response}')
        print(f"Reactiecode gevonden: {response}")

    return response

partcounter = 0
# Voor elk onderdeel in de JSON-gegevens
for part in data.values():
    if counter < 1:
        # counter += 1
        partcounter += 1
        print(f"part {partcounter}")
        logging.info(part['name'])

        if bestandsnaam == "gpu":
            parststring = part['name']+ " " + part['chipset']
        else:
            parststring = part['name']
        print(parststring)
        geparseerdedeelstring = parststring.replace(' ', '+')
        logging.info(geparseerdedeelstring)

        if part['image'] == '' or part['details'] == '':
            Deelurl = f"https://www.alternate.be/listing.xhtml?q={geparseerdedeelstring}"
        else:
            continue

        soup = BeautifulSoup(search_url(Deelurl).text, 'html.parser')
        logging.info(soup)
        if soup:
            print("soup gevonden")
        else:
            print("Kon geen soup vinden")
            continue

        if part['image'] != '':
            print("Afbeelding al in object: " + part['name'])
        else:
            print("Geen afbeelding gevonden in object: " + part['name'])
            img_tag = soup.find('img', attrs={'class': ['productPicture img-fluid m-auto']})
            logging.info(img_tag)
            if img_tag:
                print("img_tag gevonden")
                if img_tag and 'src' in img_tag.attrs:
                    img_url = f"https://www.alternate.be{img_tag['src']}"
                    print("Afbeelding gevonden")
                    print(img_url)
                    logging.info(img_url)
                    part['image'] = img_url
                else:
                    print("Kon geen afbeelding vinden")
                    logging.info("mislukt")
            else:
                print("Kon geen img_tag vinden")
                logging.info("mislukt")

        if part['details'] != '':
            print("Details al in object: " + part['name'])
        else:
            a_tag = soup.find('a', attrs={'class': ['card align-content-center productBox boxCounter text-font campaign-timer-container']})
            # print("a_tag: " + str(a_tag))
            logging.info(a_tag)
            if a_tag:
                print("a_tag gevonden")
                if a_tag and 'href' in a_tag.attrs:
                    href_waarde = a_tag['href']
                    print("Href-waarde: " + href_waarde)
                    logging.info(href_waarde)

                    a_tagsoup = BeautifulSoup(search_url(href_waarde).text, 'html.parser')
                    logging.info(a_tagsoup)
                    if a_tagsoup:
                        print("soup gevonden voor")
                        id_tag = a_tagsoup.find('div', attrs={'id': ['product-description-tab']})
                        if id_tag:
                            card_body = id_tag.find('div', attrs={'class': 'card-body'})
                            if card_body:
                                card_body_text = card_body.text
                                match = re.search(r"Productomschrijving\n\n(.*?)Productcode:", card_body_text, re.DOTALL)
                                if match:
                                    gewenste_tekst = match.group(1).strip()
                                    print("Gewenste tekst: " + gewenste_tekst)
                                    if debugmode and input("Klopt deze tekst? (j/n): ") == "j":
                                        part['details'] = gewenste_tekst
                                        print("Tekst toegevoegd aan object")
                                    else:
                                        if debugmode!=True:
                                            part['details'] = gewenste_tekst
                                            print("Tekst toegevoegd aan object")
                                else:
                                    print("Kon de gewenste tekst niet vinden")
                            else:
                                print("Kon geen 'div'-element vinden met de klasse 'card-body'")
                        else:
                            print("Kon geen 'div'-element vinden met de id 'product-description-tab'")
                    else:
                        print("Kon geen a_tagsoup vinden")
                else:
                    print("Kon geen card_url vinden")
                    logging.info("mislukt")
            else:
                print("Kon geen a_Tag vinden")
                logging.info("mislukt")
    time.sleep(1)

with open(bestandspad, 'w') as f:
    json.dump(data, f, indent=4)
print("done")
