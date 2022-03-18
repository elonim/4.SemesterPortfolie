using System.Data.SqlClient;
using System.Diagnostics;

namespace IndexOpgave2;

class Program
{
    static void Main()
    {
        var connString = @"Server=elonim.dyndns.dk; Database=Vejleadresser; User ID=elonim; Password=XXXXXXXXXXXXXXXXXXXXXXXXXXXX";
        var conn = new SqlConnection(connString);
        //CopyDbTable(conn);

        var postnr = 8762;
        postnr=7100;

        GetAddressesByPostNumber(conn, postnr);

    }

    public static void GetAddressesByPostNumber(SqlConnection conn, int postnr)
    {
        var timer = new Stopwatch();

        string query = $"SELECT * FROM VejleLite WHERE vejnavn='lærkevej' AND husnr='2'";
        conn.Open();
        SqlCommand command = new SqlCommand(query, conn);
        SqlDataReader dataReader = command.ExecuteReader();

        var addresser = new List<Address>();

        timer.Start();
        while (dataReader.Read())
        {
            var address = new Address();
            address.NytId = dataReader.GetGuid(0);
            address.id = dataReader.GetString(1);
            address.vejnavn = dataReader.GetString(2);
            address.husnr = dataReader.GetString(3);
            address.etage = dataReader.GetString(4);
            address.postnr = dataReader.GetString(5);
            address.postnrnavn = dataReader.GetString(6);
            address.kommunenavn = dataReader.GetString(7);

            addresser.Add(address);
        }
        conn.Close();
        timer.Stop();
        Console.Write($"Der er {addresser.Count()} ");
        Console.WriteLine(" Tidsforbrug " + timer.ElapsedMilliseconds.ToString("#0 'millisekunder'"));
    }




    public static void CopyDbTable(SqlConnection conn)
    {

        string query = $"SELECT NytId, Id, vejnavn, husnr, etage, postnr, postnrnavn, kommunenavn into dbo.VejleLite from dbo.VejlePlusNaboer;";

        conn.Open();
        SqlCommand command = new SqlCommand(query, conn);
        command.CommandTimeout = 6000;
        SqlDataReader dataReader = command.ExecuteReader();
        conn.Close();
    }
}
