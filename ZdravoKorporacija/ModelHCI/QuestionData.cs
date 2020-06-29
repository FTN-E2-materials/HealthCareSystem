using System;
using System.Collections.Generic;
using System.Text;

namespace ZdravoKorporacija.ModelHCI
{
    public class QuestionData
    {


        public static List<QuestionHCI> allQuestions = new List<QuestionHCI>();
        public QuestionData()
        {
            QuestionHCI oneArticle1 = new QuestionHCI();
            oneArticle1.title = "Jabuke kao lek";
            oneArticle1.patient = PatientsData.patientsList[2];
            oneArticle1.FAQ = false;
            oneArticle1.content = "JABUKA je lekovita, ali samo ako se jede cela. \n\nNjena kora smanjuje proizvodnju hormona angiotenzina koji sužava krvne sudove usled čega raste pritisak. Za jabuke se definitvno može reći da su voće koje nas drži podalje od lekara. Bogate su antioksidantima koji, dokazano je, umanjuju rizik od prevremene smrti, posebno od srčanih bolesti. Ovo voće smanjuje i rizik od dijabetesa, pa čak i pojedinih vrsta raka. Naučnici sa univerziteta u Oksfordu upoređivali su efekat lekova za snižavanje lošeg holesterola i redovnog konzumiranja jabuka.Rezultati su pokazali da se, u proseku, gotovo podjednako, bilo da se piju lekovi ili jedu jabuke smanjuje stopa smrtnosti od kardiovaskularnih bolesti. Ipak, uzimajući u obzir moguće neželjene efekte statina - lekova za snižavanje holesterola, zaključak ovog istraživanja da je bolje pojesti jednu jabuku dnevno. Britanski naučnici savetuju da oni kojima je propisana statinska terapija za kardiovaskularnu bolest, bez obzira na lekove, jabuke jedu svakog dana da poboljšaju svoje zdravstveno stanje. \n\nZDRAV NAPITAK DA biste iz jabuka dobili što više antioksidanasa savet je da napravite čaj od kore.Potrebna je šaka kora, tri do četiri šolje vode, pola kašičice cimeta, jedna kašika meda i jedna kašika limunovog soka. U posudu stavite sve sastojke osim meda i limuna i kuvajte 15 minuta.Procedite, dodajte med i limunov sok.";
            allQuestions.Add(oneArticle1);
            QuestionHCI oneArticle2 = new QuestionHCI();
            oneArticle2.title = "Alternativna medicina: za i protiv";
            oneArticle1.FAQ = false;
            oneArticle2.patient = PatientsData.patientsList[1];
            oneArticle2.content = "Iako postoji od davnina, alternativna medicina nikad nije bila previše popularna. Podaci pokazuju da je čak 40% odraslih probalo neki vid komplementarne i alternativne medicine, ali veoma kratko. Lekari su savetovali pacijentima neki vid ove medicine, ali u kombinaciji sa zvaničnom ili kao dodatak glavnoj (zapadnoj) medicinskoj terapiji koja se danas naziva integrativna medicina. \n\nPostoji više vrsta alternativnih terapija: \ndrevna medicinska terapija \n um – telo tehnika \nbiološka terapija praktična masažna terapije \nenergetska medicina \n\nTreba imati u vidu da razlike između terapija nisu uvek jasne, a neke terapije koriste više tehnika iz iste kategorije. \n\ntDrevna medicinska terapija – ona nije zasnovana na jednom leku ili jednoj masaži.Postoji niz terapija koje uključuju: ";
            allQuestions.Add(oneArticle2);
            QuestionHCI oneArticle3 = new QuestionHCI();
            oneArticle1.FAQ = false;
            oneArticle3.patient = PatientsData.patientsList[3];
            oneArticle3.title = "Keto dijeta je najefikasnija u malim dozama";
            oneArticle3.content = "Keto dijeta postaje sve popularnija zato što su je mnogobrojne slavne ličnosti shvatile kao režim mršavljenja. U studiji obavljenoj nedavno na Univerzitetu Jejl istraživači su otkrili da postoje i pozitivni i negativni efekti ove ishrane na imunološke ćelije nazvane gama delta T-ćelije koje štite tkivo i smanjuju rizik od dijabetesa i inflamacije. \n\nKeto dijeta tjera tijelo da sagorijeva masti.Kada se nivo glukoze u organizmu smanji zbog niskog sadržaja ugljenih hidrata u ishrani, tijelo djeluje kao da je u stanju gladi – iako to nije – i započinje sagorijevanje masti umjesto ugljenih hidrata.Ovaj postupak zauzvrat daje hemikalije nazvane ketonska tijela kao alternativni izvor energije. Kada tijelo sagorijeva ketonska tijela T-ćelije se šire po tijelu. To smanjuje rizik od dijabetesa i inflamacije i poboljšava metabolizam tijela.Nakon nedjelju dana na keto dijeti miševi pokazuju smanjenje nivoa šećera u krvi kao i smanjenje inflamatornih procesa. Međutim, kada je tijelo u ovom režimu „gladovanja – negladovanja“, skladištenje masti se dešava uporedo sa razgradnjom masti. Kada miševi nastave da konzumiraju hranu sa visokim udjelom masti i bez ugljenih hidrata oni konzumiraju više masti nego što mogu da sagore i razviju dijabetes i gojaznost zato što gube zaštitne gama delta T - ćelije. \n\nOvo otkriće naglašava međusobnu interakciju metabolizma i imunološkog sistema, ali treba naglasiti da su potrebne dugoročne kliničke studija na ljudima da bi se definitivno potvrdile prednosti i mane keto dijete.";
            allQuestions.Add(oneArticle3);
        }
    }
}
