#!/usr/bin/python3
# -*- coding: utf-8 -*-

import requests
import json
from lxml import etree
import DataCAIN

def getCain(PAGE, city):
    tree = etree.HTML(PAGE)
    nodes = tree.xpath('//*[@id="frontend-serp"]/div/div[4]/div')
    listNode = dict()
    if len(nodes) > 0:
        nodeIter = -1
        #for iter in range(len(nodes)):
        for iter in range(6):
            # jpg = node.xpath('./div/div[1]')[0].xpath('./div/div/div/div[1]/div/div/div/img')

            # Фильтер топ3 и пустых тегов
            value = nodes[iter].values()
            if len(nodes[iter].values()) == 0 or 'top3' in nodes[iter].values()[0]:
                continue


            # print(nodes[iter].values())
            # print(''.join(nodes[iter].itertext()))
            # print(len(node.values()))
            # '//*[@id="frontend-serp"]/div/div[4]/div'
            nodeIter += 1
            listNode[nodeIter] = DataCAIN.Cain(city)
            node2way = nodes[iter].xpath('./div/div')

            if(len(node2way) == 2):
                for i in range(len(node2way)):
                    if i == 0:
                        jpg = node2way[i].xpath('./div/div/div/div[1]/div/div/div/img')
                        if(len(jpg) == 1):
                            listNode[nodeIter].jpgURL = jpg[0].attrib["src"]

                    elif i == 1:
                        # '//*[@id="frontend-serp"]/div/div[4]/div/div/div'
                        text2way = node2way[i].xpath('./div/div')
                        # print(len(text2way))
                        # ['content-main--19JBy']
                        # ['content-footer--20Yyt']
                        for j in range(len(text2way)):
                            if j == 0:
                                contexDict = dict()
                                contex2Dict = dict()
                                # ['offerInfo--2zqLN']
                                context5way = text2way[j].xpath('./div[1]/div[1]/div')
                                for conIter in range(len(context5way)):

                                    listtext = []
                                    for itertext in context5way[conIter].itertext():
                                        listtext.append(itertext)
                                    contexDict[conIter] = listtext
                                    listNode[nodeIter].dataContext = contexDict

                                # //*[@id="frontend-serp"]/div/div[4]/div[14]/div/div[2]/div/div[1]/div[1]/div[2]
                                context2 = text2way[j].xpath('./div[1]/div[2]')
                                if(len(context2) == 1):
                                    listtext = []
                                    for itertext in context2[0].itertext():
                                        listtext.append(itertext)
                                    contex2Dict[0] = listtext
                                    listNode[nodeIter].dataContext2 = contex2Dict

                            elif j == 1:
                                # //*[@id="frontend-serp"]/div/div[4]/div[10]/div/div[2]/div/div[2]/a
                                donInfo = text2way[j].xpath('./a')
                                if(len(donInfo) == 1):
                                    listNode[nodeIter].dopInfoURL = donInfo[0].attrib["href"]

            else:
                print("error")
    else:
        print("бан")
    return listNode



    # for i in node.itertext():
    #     print(i)
def start():
    URL_get_region = "https://www.cian.ru/cian-api/site/v1/get-regions/"
    jsonRegion = None
    jsonValue = None

    try:
        # jsonRegion = requests.get(URL_get_region).text
        with open("jsonCain.json", "r") as file:
            jsonRegion = file.read()
    except:
        print("Ошибка доступа к сайту")

    try:
        jsonValue = json.loads(jsonRegion)
    except Exception as ex:
        print("Ошибка десериализации JSON  {}".format(ex))

    listUrlSourse = []
    listUrlSourse.append("{0}/cat.php?deal_type=rent&engine_version=2&offer_type=flat&region={1}&room1=1&room2=1&room3=1&room4=1&room5=1&room6=1&room7=1&room9=1&type=4") # снять
    # listUrlSourse.append("{0}/cat.php?deal_type=rent&engine_version=2&offer_type=flat&region={1}&room1=1&room2=1&room3=1&room4=1&room5=1&room6=1&room7=1&room9=1") # купить
    # listUrlSourse.append("{0}/cat.php?deal_type=rent&engine_version=2&offer_type=flat&region={1}&room1=1&room2=1&room3=1&room4=1&room5=1&room6=1&room7=1&room9=1&type=2") # аренда посуточно
    # listUrlSourse.append("{0}/cat.php?deal_type=rent&engine_version=2&offer_type=offices&office_type%5B0%5D=1&office_type%5B10%5D=12&office_type%5B1%5D=2&office_type%5B2%5D=3&office_type%5B3%5D=4&office_type%5B4%5D=5&office_type%5B5%5D=6&office_type%5B6%5D=7&office_type%5B7%5D=9&office_type%5B8%5D=10&office_type%5B9%5D=11&region={1}")
    # listUrlSourse.append("{0}/cat.php?deal_type=sale&engine_version=2&offer_type=offices&office_type%5B0%5D=1&office_type%5B10%5D=12&office_type%5B1%5D=2&office_type%5B2%5D=3&office_type%5B3%5D=4&office_type%5B4%5D=5&office_type%5B5%5D=6&office_type%5B6%5D=7&office_type%5B7%5D=9&office_type%5B8%5D=10&office_type%5B9%5D=11&region={1}")
    # listUrlSourse.append("{0}/cat.php?deal_type=sale&engine_version=2&object_type%5B0%5D=1&offer_type=flat&region={1}&room1=1&room2=1&room3=1&room4=1&room5=1&room6=1&room7=1&room9=1")
    # listUrlSourse.append("{0}/cat.php?deal_type=sale&engine_version=2&offer_type=flat")
    Citys = dict()

    for i in jsonValue['data']['items'][:1]: #TEST УБРАТЬ 1
        pageSourse = None
        for sourseURLItem in range(len(listUrlSourse)):

            urlSourse = None
            try:
                urlSourse = listUrlSourse[sourseURLItem].format(i["baseHost"], i["id"])
            except:
                print("Ошибка JSON [baseHost] [id]")

            try:
                pageSourse = requests.get(urlSourse).text
            except:
                print("Ошибка доступа к URL'{0}'".format(urlSourse))
            try:
                CAINS = getCain(pageSourse, city=i["displayName"])
                if i["name"] in Citys:
                    Citys[i["name"]].append([i["name"], i["id"], i["baseHost"], urlSourse, CAINS])
                else:
                    Citys[i["name"]] = [[i["name"], i["id"], i["baseHost"], urlSourse, CAINS]]
            except Exception as ex:
                print("Ошибка парсинга {0}|{1}|{2} error {3}".format(i["name"], i["id"], i["baseHost"], ex))

            print("{0}|{1}|{2}|>>> {3}".format(i["name"], i["id"], i["baseHost"], urlSourse))

    return Citys
