import requests
from bs4 import BeautifulSoup
import json
import time
import logging
import re
import os

os.system('cls')
bestandspad = "motherboard.json"
print("Bestandspad: " + bestandspad)

with open(bestandspad, 'r') as f:
    data = json.load(f)

# if input("Debugmodus? (j/n): ") == "j":
#     debugmode = True
# else:
#     debugmode = False
# print("Debugmodus: " + str(debugmode))

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
    print("responese voor url" + url)

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


        parststring = part['name']
        geparseerdedeelstring = parststring.replace(' ', '+')
        print(geparseerdedeelstring)
        logging.info(geparseerdedeelstring)

        if part['memorytype'] == '':
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

        if part['memorytype'] != '':
            print("memorytype al in object: " + part['name'])
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
                    print(href_waarde)

                    a_tagsoup = BeautifulSoup(search_url(href_waarde).text, 'html.parser')
                    logging.info(a_tagsoup)
                    if a_tagsoup:
                        # print("soup gevonden voor")

                        # 17 row tr
                        div_tag = a_tagsoup.find('div', attrs={'class': ['d-block details-www mb-2']})
                        if div_tag:
                            # print("div_tag gevonden")

                            table_tag = div_tag.find('table')
                            if table_tag:
                                # print("tbody_tag gevonden")

                                tr_tag = table_tag.find_all('tr')
                                if tr_tag:
                                    countTr = 0
                                    for tr in tr_tag:
                                        countTr += 1
                                        if countTr > 13 or countTr < 21:
                                            td_tag = tr.find('td', attrs={'class': ['c4']})
                                            if td_tag:
                                                td_text = td_tag.text
                                                if 'DDR' in td_text:
                                                    print("DDR found in td text")
                                                    print("")
                                                    split_string = td_text.split(" ", 1)
                                                    if len(split_string) > 1:
                                                        string = split_string[1].replace(" ", "")
                                                        print("string: " + string)
                                                        part['memorytype'] = string
                                                        break
                                                    else:
                                                        print("No whitespace found")
                                            else:
                                                print("Kon geen td_tag vinden")
                                else:
                                    print("Kon geen tr_tag vinden")
                            else:
                                print("Kon geen tbody_tag vinden")
                        else:
                            print("Kon het id_tag vinden")
                    else:
                        print("Kon geen soup vinden")
                else:
                    print("Kon geen href vinden")
            else:
                print("Kon geen a_tag vinden")
                logging.info("mislukt")


with open(bestandspad, 'w') as f:
    json.dump(data, f, indent=4)
print("done")
