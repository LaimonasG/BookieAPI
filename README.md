Bookie

---------------

1.1	Sistemos paskirtis

Projekto tikslas – sukurti platformą, leidžiančią žmonėms pirkti ir parduoti perskaitytas knygas. 

Prisijungęs vartotojas gali patalpinti knygos skelbimą, taip pat naršyti po paskelbtų knygų sąrašą, po jomis palikti komentarus, jas dėti į krepšelį. Administratorius sistemoje gali trinti komentarus ir skelbimus. 

---------------

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
7.  Ištrinti savo paskelbtą knygą

Administratorius galės:

1.	Pašalinti komentarą

2.	Pašalinti knygos skelbimą

Sistemos sudedamosios dalys: 

•	Kliento pusė – naudojant React.js

•	Serverio pusė – naudojant c# .net ir MySql

---------------

Sistemos talpinimui yra naudojamas Azure serveris. Internetinė aplikacija yra pasiekiama per HTTP protokolą. Duomenų manipuliavimui su duomenų baze yra
reikalingas API, kuris pasiekiamas per aplikacijų programavimo sąsają. Pats API vykdo duomenų mainus su duomenų baze.

---------------

Sistemos deployment diagrama:

![image](https://user-images.githubusercontent.com/79421767/208295755-11be5002-48ea-434e-98c4-f3de9fd9f895.png)

---------------

Sistemos puslapių pavyzdžiai:

Registracijos puslapis:

![image](https://user-images.githubusercontent.com/79421767/208295860-59a66c2e-18b2-4944-888c-902c94200a15.png)

Prisijungimo puslapis:

![image](https://user-images.githubusercontent.com/79421767/208069775-a243903c-c047-468a-9959-3375f48c1e0e.png)

Žanrų puslapis:

![image](https://user-images.githubusercontent.com/79421767/208069721-34cfdef4-36ef-49b9-9f6c-b350220e05d1.png)

Knygų puslapis:

![image](https://user-images.githubusercontent.com/79421767/208069522-d43f3515-d388-4f92-bdd8-264e8c7d6a4d.png)

Knygos komentarų langas:

![image](https://user-images.githubusercontent.com/79421767/208304565-ccd9fe68-0e9c-4bc0-b7be-3fdf839aef23.png)






API specifikacija:

Response codes

Kodas        | Reikšmė                   |
------------ | ------------------------- |
200/201 (OK) | urequest sent succesfully |
------------ | ------------------------- |
404 (Not Found) | url not found |
--- | --- |
403 (Forbidden) | JWT token value incorrect, forbidden request for this token |
--- | --- |
401 (Unauthorized) | JWT token is missing |
--- | --- |
400 (Bad request) | bad request body |
--- | --- |
204 (No Content) | record deleted succesfully |

---------------

GenresController.cs

---------------

GetMany (GET)

Gražina visus genre įrašus.

URL: https://localhost:7051/api/genres

Requires authentication : false

Parameters: none

---------------

Get (GET)

Gražina konkretų genre įrašą.

URL: https://localhost:7051/api/genres/{genreId}

Requires authentication : false

Parameters:
genreId -required parameter, to get a genre record. Example: 1021


---------------

Create (POST)

Prideda naują genre įrašą.

URL: https://localhost:7051/api/genres

Requires authentication : false

Parameters: None

Example body:
{
    "name":6
}

---------------

Update (PUT)

Atnaujina pasirinkto genre įrašo reikšmę.

URL: https://localhost:7051/api/genres/{genreId}

Requires authentication : false

Parameters:
genreId -required parameter, to get a genre record. Example: 1021

Example body:
{
    "name":6
}

---------------

BooksController.cs

---------------

GetMany (GET)

Gražina visus books įrašus, priklausančius žanrui.

URL: https://localhost:7051/api/genres/{genreId}/books

Requires authentication : false

Parameters:
genreId -required parameter, to get a genre record. Example: 1021

---------------

Get (GET)

Gražina konkretų knygos įrašą.

URL: https://localhost:7051/api/genres/{genreId}/books/{bookId}

Requires authentication : false

Parameters:
genreId -required parameter, to get a genre record. Example: 1021
bookId -required parameter, to get a book record. Example: 1022

---------------

Create (POST)

Prideda naują book įrašą.

URL: https://localhost:7051/api/genres/{genreId}/books

Requires authentication : yes

Authorization - Bearer Token, requires JWT token value.

Parameters:
genreId -required parameter, to get a genre record. Example: 1021


Example body:
{
   "Name":"blbabla",
   "Author":"John Johnson",
   "Quality":"good",
   "Price":15
}

---------------

Update (PUT)

Atnaujina pasirinkto book įrašo reikšmę.

URL: https://localhost:7051/api/genres/{genreId}/books/{bookId}

Requires authentication : yes

Authorization - Bearer Token, requires JWT token value.

Parameters:
genreId -required parameter, to get a genre record. Example: 1021
bookId -required parameter, to get a book record. Example: 1022

Example body:
{
   "Name":"blbabla",
   "Author":"John Johnson",
   "Quality":"good",
   "Price":15
}

---------------

CommentsController.cs

---------------

GetMany (GET)

Gražina visus comments įrašus, priklausančius knygai.

URL: https://localhost:7051/api/genres/{genreId}/books/{bookId}/comments

Requires authentication : false

Parameters:
genreId -required parameter, to get a genre record. Example: 1021
bookId -required parameter, to get a book record. Example: 1022

---------------

Get (GET)

Gražina konkretų komentaro įrašą.

URL: https://localhost:7051/api/genres/{genreId}/books/{bookId}/comments/{commentsId}

Requires authentication : false

Parameters:
genreId -required parameter, to get a genre record. Example: 1021
bookId -required parameter, to get a book record. Example: 1022
commentsId -required parameter, to get a comments record. Example: 1023

---------------

Create (POST)

Prideda naują comments įrašą.

URL: https://localhost:7051/api/genres/{genreId}/books/comments

Requires authentication : no

Parameters:
genreId -required parameter, to get a genre record. Example: 1021
bookId -required parameter, to get a book record. Example: 1022


Example body:
{
    "Text":"test comment"    
}

---------------

Update (PUT)

Atnaujina pasirinkto comments įrašo reikšmę.

URL: https://localhost:7051/api/genres/{genreId}/books/{bookId}/comments/{commentsId}

Requires authentication : yes

Authorization - Bearer Token, requires JWT token value.

Parameters:
genreId -required parameter, to get a genre record. Example: 1021
bookId -required parameter, to get a book record. Example: 1022
commentsId -required parameter, to get a comments record. Example: 1023

{
    "Text":"test comment"    
}

---------------

AuthController.cs

---------------

Register (POST)

Įregistruoja naują vartotoją.

URL: https://localhost:7051/api/register

Requires authentication : no

Parameters: none

Example body:
{
    "UserName": "Laimonas9",
    "Email": "laimis@gmail.com",
    "Password": "Verystrongpassword1*"
}

---------------

Login (Post)

Prisijungia prie puslapio palei egzistuojančio vartotojo duomenis ir gražina JWT tokeną.

URL: https://localhost:7051/api/login

Requires authentication : no

Parameters: none

Example body:
{
    "username":"Admin",
    "password":"Kokosas97*"
}



---------------

Išvados:

Projektas išpildytas, vartotojo autorizacija veikia, pagrindiniai lygiai genre/book/comment sąveikauja tinkamai. Front-end aplikacijoje nerealizuotas basket komponentas. API projektas buvo patalpintas Azure cloud'e, tačiau po mėnesio laiko pasibaigus nemokamai paskyrai teko projektą leisti per localhost.





