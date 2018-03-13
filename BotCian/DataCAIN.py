#!/usr/bin/python3
# -*- coding: utf-8 -*-

class Cain:

    def __init__(self, city):
        self.id = ''
        self.jpgURL = ''
        self.dataContext = dict()
        self.dataContext2 = dict()
        self.dopInfoURL = ''
        self.minDopInfo = ''
        self._Sourse = ''
        self.city = city
        self.teg = [
            "Этаж", "грузовой", "длительный", "жилая", "пассжирский", "балкон", "кухня", "этаж", "пассажирский",
            "лоджия", "Многокомнатная", "комиссии", "залог", "посуточно", "торг", "вторичка",
            "Авиамоторная", "Академическая", "Александровский сад", "Алексеевская", "Алтуфьево",
            "Аннино", "Арбатская", "Автозаводская", "Алма-Атинская", "Аэропорт",
            "Бабушкинская", "Багратионовская", "Баррикадная", "Бауманская", "Беговая",
            "Белорусская", "Беляево", "Бибирево", "Библиотека Ленина", "Битцевский парк",
            "Борисово", "Боровицкая", "Ботанический сад", "Братиславская", "Бульвар Дмитрия Донского",
            "Бульвар Рокоссовского", "Бульвар адмирала Ушакова", "Бунинская Аллея", "ВДНХ", "Варшавская",
            "Владыкино", "Волгоградский проспект", "Волжская", "Волоколамская", "Воробьевы горы",
            "Водный стадион", "Войковская", "Выставочная", "Выхино", "Деловой центр",
            "Дмитровская", "Добрынинская", "Достоевская", "Дубровка", "Динамо",
            "Домодедовская", "Жулебино", "Зябликово", "Измайловская", "Калужская",
            "Кантемировская", "Каховская", "Каширская", "Киевская", "Киевская",
            "Китай-город", "Кожуховская", "Комсомольская", "Коньково", "Котельники",
            "Коломенская", "Краснопресненская", "Красносельская", "Красные ворота", "Крестьянская застава",
            "Кропоткинская", "Красногвардейская", "Крылатское", "Кузнецкий мост", "Кузьминки",
            "Кунцевская", "Курская", "Кутузовская", "Ленинский проспект", "Лермонтовский проспект",
            "Лесопарковая", "Лубянка", "Люблино", "Марксистская", "Марьина роща",
            "Марьино", "Медведково", "Международная", "Менделеевская", "Митино",
            "Молодежная", "Мякинино", "Нагатинская", "Нагорная", "Нахимовский Проспект",
            "Новогиреево", "Новокосино", "Новокузнецкая", "Новослободская", "Новоясеневская",
            "Новые Черёмушки", "Октябрьская", "Октябрьское Поле", "Орехово", "Отрадное",
            "Охотный ряд", "Павелецкая", "Парк Культуры", "Парк Победы", "Партизанская",
            "Первомайская", "Перово", "Петровско-Разумовская", "Печатники", "Пионерская",
            "Планерная", "Площадь Ильича", "Площадь Революции", "Полежаевская", "Полянка",
            "Пражская", "Преображенская площадь", "Пролетарская", "Проспект Вернадского", "Проспект Мира",
            "Профсоюзная", "Пушкинская", "Пятницкое шоссе", "Речной вокзал", "Рижская",
            "Римская", "Румянцево", "Рязанский проспект", "Савеловская", "Саларьево",
            "Свиблово", "Севастопольская", "Семеновская", "Серпуховская", "Славянский бульвар",
            "Смоленская", "Сокол", "Сокольники", "Спартак", "Спортивная",
            "Сретенский бульвар", "Строгино", "Студенческая", "Сухаревская", "Сходненская",
            "Таганская", "Текстильщики", "Теплый стан", "Тверская", "Театральная",
            "Тимирязевская", "Третьяковская", "Тропарёво", "Трубная", "Тульская",
            "Тургеневская", "Тушинская", "1905 года", "Горчакова", "Скобелевская",
            "Старокачаловская", "академика Янгеля", "Университет", "Филевский парк", "Фили",
            "Царицыно", "Фрунзенская", "Цветной бульвар", "Черкизовская", "Чертановская",
            "Чеховская", "Чистые пруды", "Чкаловская", "Шаболовская", "Шипиловская",
            "Шоссе Энтузиастов", "Щелковская", "Щукинская", "Электрозаводская", "Юго-Западная",
            "Южная", "Ясенево",
        ]

    def strJoin(self, list):
        strr = ''
        for i in list:
            if "минут" in i:
                strr += " "
            else:
                try:
                    if strr[-1:] == " " and i[:1] == " ":
                        strr = strr[:-1] + i
                    elif strr[-1:] != " " and strr[-1:] != "." and strr[-1:] != "," \
                            and i[:1] != " " and i[:1] != "." and i[:1] != ",":
                        strr = "{0} {1}".format(strr, i)
                except Exception as ex:
                    print(ex)
                    strr = strr + i
        return strr

    def Sourse(self):
        textSourse = "<b>{0}</b>\n".format(self.city)

        if(self._Sourse != ''):
            return self._Sourse

        if len(self.dataContext) > 0 and len(self.dataContext2) > 0:
            for context in self.dataContext:
                if context == 0:
                    listCon = self.dataContext[context]
                    strr = self.strJoin(listCon)
                    textSourse = textSourse + strr
                if context == 1:
                    listCon = self.dataContext[context]
                    strr = self.strJoin(listCon)
                    textSourse = textSourse + strr
                if context == 2:
                    listCon = self.dataContext[context]
                    strr = self.strJoin(listCon)
                    textSourse = textSourse + strr
                if context == 3:
                    listCon = self.dataContext[context]
                    strr = self.strJoin(listCon)
                    textSourse = textSourse + strr
                if context == 4:
                    listCon = self.dataContext[context]
                    strr = self.strJoin(listCon)
                    textSourse = textSourse + strr
                textSourse += "\n\n"

            try:
                if self.dataContext2[0][0] == "":
                    pass
                else:
                    textSourse += self.dataContext2[0][0]
                    textSourse += "\n\n"
            except Exception as ex:
                pass

            # костыль
            textSourse += '<a href="{0}">Фото</a>\n'

            textSourse += '<a href="{0}">Подробнее</a>'.format(self.dopInfoURL)  # пока отлючил

            self._Sourse = textSourse
            return textSourse
        else:
            return ""


    def Sourse2(self):
        textSourse = "#{0} ".format(self.city)

        if len(self.dataContext) > 0 and len(self.dataContext2) > 0:
            for context in self.dataContext:
                if context == 0:
                    listCon = self.dataContext[context]
                    strr = self.strJoin(listCon)
                    textSourse = textSourse + strr.strip()
                if context == 1:
                    listCon = self.dataContext[context]
                    strr = self.strJoin(listCon)
                    textSourse = textSourse + "Цена: " + strr.strip()
                if context == 2:
                    listCon = self.dataContext[context]
                    strr = self.strJoin(listCon)
                    textSourse = textSourse + "Площадь: " + strr.strip()
                if context == 3:
                    listCon = self.dataContext[context]
                    strr = self.strJoin(listCon)
                    textSourse = textSourse + strr
                if context == 4:
                    listCon = self.dataContext[context]
                    strr = self.strJoin(listCon)
                    textSourse = textSourse + strr
                textSourse += "\n"

            # try:
            #     if self.dataContext2[0][0] == "":
            #         pass
            #     else:
            #         textSourse += self.dataContext2[0][0]
            #         textSourse += "\n\n"
            # except Exception as ex:
            #     pass

            # костыль
            # textSourse += '<a href="{0}">Фото</a>\n'

            # textSourse += '<a href="{0}">Подробнее</a>'.format(self.dopInfoURL)  # пока отлючил

            for t in self.teg:
                textSourse = textSourse.replace(t, '#{0}'.format(t))

            listtext = textSourse.split(" ")

            for i in range(len(listtext)):
                try:
                    if "область" == listtext[i] and "#" != listtext[i + 1][:1]:
                        listtext[i + 1] = "#{0}".format(listtext[i + 1])
                    elif "район" == listtext[i] and "#" != listtext[i + 1][:1]:
                        listtext[i + 1] = "#{0}".format(listtext[i + 1])
                    elif "поселение" == listtext[i] and "#" != listtext[i + 1][:1]:
                        listtext[i + 1] = "#{0}".format(listtext[i + 1])
                    elif "Улица" == listtext[i] and "#" != listtext[i + 1][:1]:
                        listtext[i + 1] = "#{0}".format(listtext[i + 1])

                    textSourse = " ".join(listtext)
                except:
                    pass


            for i in range(1,7):
                textSourse = textSourse.replace("{0}-комнатная".format(i), "#{0}комнатная".format(i))

            return textSourse
        else:
            return ""

