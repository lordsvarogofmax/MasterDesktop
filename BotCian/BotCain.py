#!/usr/bin/python3
# -*- coding: utf-8 -*-
import telebot
import requests
import ParsingCAIN
import DataCAIN
import time
from PIL import Image
from io import BytesIO

token = '426839921:AAGDheDaN4d1HER0M0z_ur5o5VJHcutNOBg'
url = "https://cdn-a.cian.site/8/218/183/kvartira-sanktpeterburg-dunayskiy-prospekt-381812897-2.jpg"
bot = telebot.TeleBot(token)
CHANNEL_NAME = "@cianru"
CITY = 0
CAIN = 4
oldCains = None
keyContinues = False
# bot.send_message(CHANNEL_NAME, "citys len {}".format(len(cains)))

if __name__ == '__main__':
    cains = None
    photoSource = None
    textSourse = ''
    try:
        while True:

            try:
                cains = ParsingCAIN.start()
            except Exception as ex:
                print("Ошибка парсинга!!! {0}".format(ex))

            try:
                for cainKEY in cains:
                    cainDictList = cains[cainKEY]

                    for cainDictIter in cainDictList:
                        cainDict = cainDictIter[CAIN]

                        for cainDictKey in cainDict:
                            cain = cainDict[cainDictKey]
                            textSourse = cain.Sourse()

                            # Костыль поиcка
                            try:
                                if oldCains != None:
                                    cainDictListOld = oldCains[cainKEY]
                                    for cainDictIterOld in cainDictListOld:
                                        old = cainDictIterOld
                                        new = cainDictIter
                                        cainDictOld = None
                                        if (old[0] == new[0] and old[1] == new[1] and old[2] == new[2]):
                                            cainDictOld = old[CAIN]
                                        if cainDictOld != None:
                                            listKey = dict(cainDictOld).keys()

                                            for IterKeyOld in listKey:
                                                cainOld = cainDictOld[IterKeyOld]

                                                if cain.Sourse() == cainOld.Sourse():
                                                    keyContinues = True
                                                    break
                                                else:
                                                    keyContinues = False

                                        else:
                                            keyContinues = False
                            except Exception as ex:
                                print("ОШИБКА поска новых обьновлений {}".format(ex))

                            try:
                                if (cain.jpgURL == ''):
                                    photoSource = None
                                else:
                                    photoSource = requests.get(cain.jpgURL, stream=True).content
                            except:
                                print("Ошибка загрузки фото URL'{}'".format(cain.jpgURL))

                            # BOT
                            try:
                                if (not keyContinues and len(textSourse) != 0):
                                    if keyContinues == False and oldCains != None:
                                        pass

                                    if (photoSource == None):
                                        pass

                                        # markup = telebot.types.InlineKeyboardMarkup()
                                        # btn_my_site = telebot.types.InlineKeyboardButton(text='Подробнее', url=cain.dopInfoURL)
                                        # markup.add(btn_my_site)
                                        # bot.send_message(CHANNEL_NAME, cain.Sourse2(), reply_markup=markup)
                                    else:
                                        pass
                                        img = Image.open(BytesIO(photoSource))
                                        width, height = img.size

                                        if width > 300 and cain.dopInfoURL != "":
                                            markup = telebot.types.InlineKeyboardMarkup()
                                            btn_my_site = telebot.types.InlineKeyboardButton(text='Подробнее', url=cain.dopInfoURL)
                                            markup.add(btn_my_site)

                                            #bot.send_photo(CHANNEL_NAME, photoSource, caption=cain.Sourse2(), reply_markup=markup)


                                            print(len(cain.Sourse2()))
                                            print(cain.Sourse2().replace("\n", ">>"))
                                            print("-" * 80)
                                            # break # TEST
                                            #time.sleep(20)

                            except Exception as ex:
                                print("ОШИБКА отправки в телеграмм {}".format(ex))

                oldCains = cains
            except Exception as ex:
                print("Ошибка основного цыкла {0}".format(ex))


        time.sleep(2)
        print("START")

    except Exception as ex:
        print("ОШИБКА парсера {}".format(ex))




pass