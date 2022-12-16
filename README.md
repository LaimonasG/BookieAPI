1.1	Sistemos paskirtis
Projekto tikslas – sukurti platformą, leidžiančią žmonėms pirkti, parduoti ir mainyti perskaitytas knygas. 

Prisijungęs vartotojas gali patalpinti knygos skelbimą, taip pat naršyti po paskelbtų knygų sąrašą, po jomis palikti komentarus, jas dėti į krepšelį ar pasiūlyti
pardavėjui kitą knygą mainais. Administratorius sistemoje gali trinti komentarus ir skelbimus. 

1.2	Funkciniai reikalavimai
Neregistruotas sistemos naudotojas galės:
1.	Peržiūrėti platformos reprezentacinį puslapį;
2.	Užsiregistruoti prie platformos;	

Registruotas sistemos naudotojas galės: 
1.	Atsijungti
2.	Prisijungti
3.	Naršyti po paskelbtų knygų sąrašą
4.	Paskelbti savo knygą
5.	Komentuoti kitas knygas
6.	Pridėti knygą į krepšelį
7.	Pasiūlyti knygos mainus
8.	Priimti knygos mainus
9.	Pirkti krepšelyje pridėtas knygas

Administratorius galės:
1.	Pašalinti komentarą
2.	Pašalinti knygos skelbimą
3.	Šalinti naudotojus


Sistemos sudedamosios dalys: 
•	Kliento pusė – naudojant React.js
•	Serverio pusė – naudojant c# .net ir MySql


Sistemos talpinimui yra naudojamas Azure serveris. Internetinė aplikacija yra pasiekiama per HTTP protokolą. Duomenų manipuliavimui su duomenų baze yra
reikalingas API, kuris pasiekiamas per aplikacijų programavimo sąsają. Pats API vykdo duomenų mainus su duomenų baze.

Sistemos puslapių pavyzdžiai:
Knygų puslapis:
![image](https://user-images.githubusercontent.com/79421767/208069522-d43f3515-d388-4f92-bdd8-264e8c7d6a4d.png)

Žanrų puslapis:
![image](https://user-images.githubusercontent.com/79421767/208069721-34cfdef4-36ef-49b9-9f6c-b350220e05d1.png)

Prisijungimo puslapis:
![image](https://user-images.githubusercontent.com/79421767/208069775-a243903c-c047-468a-9959-3375f48c1e0e.png)




