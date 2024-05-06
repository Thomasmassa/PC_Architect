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

time.sleep(3)

logging.basicConfig(filename='scraperLogAlternate.log', level=logging.INFO, format='%(asctime)s %(message)s')    

if not data:
    logging.info('Geen gegevens gevonden')
    exit()
else:
    logging.info('Gegevens gevonden')

time.sleep(1)

teller = 0

def zoek_url(url):
    time.sleep(10)
    response = requests.get(url)

    if response.status_code != 200:
        logging.info(f'Fout bij het ophalen van de zoekresultaten voor {deel["naam"]}')
        logging.info(f'response: {response}')
        print("Kon geen reactie krijgen")
        time.sleep(5)
        exit()
    else:
        logging.info(f'Zoekresultaten opgehaald voor {deel["naam"]}')
        logging.info(f'response: {response}')
        print(f"Reactiecode gevonden: {response}")

    return response


# Voor elk onderdeel in de JSON-gegevens
for deel in data.values():
    if teller < 1:
        # teller += 1
        logging.info(deel['naam'])

        # deelstring = deel['naam']+deel['chipset']
        deelstring = deel['naam']
        geparseerdedeelstring = deelstring.replace(' ', '+')
        logging.info(geparseerdedeelstring)

        Deelurl = f"https://www.alternate.be/listing.xhtml?q={geparseerdedeelstring}"

        soup = BeautifulSoup(zoek_url(Deelurl).text, 'html.parser')
        logging.info(soup)
        if soup:
            print("soup gevonden")
        else:
            print("Kon geen soup vinden")
            continue

        if deel['afbeelding'] != '':
            print("Afbeelding al in object: " + deel['naam'])
        else:
            print("Geen afbeelding gevonden in object: " + deel['naam'])
            img_tag = soup.find('img', attrs={'class': ['productPicture img-fluid m-auto']})
            logging.info(img_tag)
            if img_tag:
                print("img_tag gevonden")
                if img_tag and 'src' in img_tag.attrs:
                    img_url = f"https://www.alternate.be{img_tag['src']}"
                    print("Afbeelding gevonden")
                    print(img_url)
                    logging.info(img_url)
                    deel['afbeelding'] = img_url
                else:
                    print("Kon geen afbeelding vinden")
                    logging.info("mislukt")
            else:
                print("Kon geen img_tag vinden")
                logging.info("mislukt")

        if deel['details'] != '':
            print("Details al in object: " + deel['naam'])
        else:
            a_tag = soup.find('a', attrs={'class': ['card align-content-center productBox boxCounter text-font']})
            logging.info(a_tag)
            if a_tag:
                print("a_tag gevonden")
                if a_tag and 'href' in a_tag.attrs:
                    href_waarde = a_tag['href']
                    print("Href-waarde: " + href_waarde)
                    logging.info(href_waarde)

                    a_tagsoup = BeautifulSoup(zoek_url(href_waarde).text, 'html.parser')
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
                                    deel['details'] = gewenste_tekst
                                else:
                                    print("Kon de gewenste tekst niet vinden")
                            else:
                                print("Kon geen 'div'-element vinden met de klasse 'card-body'")
                        else:
                            print("Kon geen 'div'-element vinden met de id 'product-description-tab'")
                    else:
                        print("Kon geen a_tagsoup vinden")
                    time.sleep(2)
                else:
                    print("Kon geen card_url vinden")
                    logging.info("mislukt")
            else:
                print("Kon geen a_Tag vinden")
                logging.info("mislukt")

with open(bestandspad, 'w') as f:
    json.dump(data, f, indent=4)
print("done")
