# RacketManagement
Sistem za izposojo loparjev

## Authors
Jaša Jovan 63200123

## Problem
Trenutno opravljam študentsko delo v trgovini specializirani za športe z loparji(tenis, badminton, squash, padel, crossminton, namizni tenis), kjer tudi loparje dajemo v izposojo za testiranje in trenutno, ko si nekdo pride izposoditi lopar vnašamo vse podatke v excel tabelo. Problem trenutne metode je, da se vsake toliko časa pozabi osebo kontaktirati v primeru, da zamuja ali podobno.

## Rešitev
Informacijski sistem **RacketManagement** zgornji problem reši tako, da zaposlenim omogoča skeniranje QR kode določenega loparja in vnos podatkov stranke in datum vračila. Ko stranka prinese lopar nazaj se QR koda ponovno skenira in izbriše izposoja.
Sistem bo omogočal pregled, dodajanje, brisanja loparjev(določene loparjev imamo za odkup). Nov lopar se doda tako, da na lopar nalepimo novo QR kodo, ki bo vsebovala nek id in ko prvič kodo skeniramo bomo lahko vnesli podatke loparja. Možen bo tudi pregled zgodovine izposoj določenega loparja ali stranke.

## TODO
- [ ] Kreiranje dotnet projekta
- [ ] iOS odjemalec (v sklopu EMP)
- [ ] Android odjemalec