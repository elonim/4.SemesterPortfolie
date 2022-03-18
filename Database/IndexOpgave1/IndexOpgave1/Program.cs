
namespace IndexOpgave1;


class Program
{
    static void Main()
    {
        Getter.GetAddressesByPostNumber();
        Getter.GetAddressesByPostNummerAndVejnavn();
        Getter.GetAddressesLikeA();
    }
}



/*
Databasen er installeret på en exstern server
Resultater havde nok været hurtigere hvis databasen var installeret lokalt på maskinen


 Resultat uden indexering

Der er 1597000 i postnummer 7100
Tidsforbrug 52990 millisekunder

Der er 12840 i postnummer 7100 på vejen Boulevarden
Tidsforbrug 50246 millisekunder

Der er 12880 der starter med a
Tidsforbrug 50078 millisekunder


med indexering

Der er 1597000 i postnummer 7100
Tidsforbrug 51729 millisekunder

Der er 12840 i postnummer 7100 på vejen Boulevarden
Tidsforbrug 166 millisekunder

Der er 12880 der starter med a
Tidsforbrug 64 millisekunder


*/