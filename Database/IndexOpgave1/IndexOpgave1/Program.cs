using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Data.SqlClient;
using System.Diagnostics;

namespace IndexOpgave1;


class Program
{
    static void Main()
    {
        //var summary = BenchmarkRunner.Run( typeof( Getter ) );

        Getter.GetAddresses();
    }
}





public class Getter
{
    //[Benchmark]
    public static void GetAddresses()
    {
        var timer = new Stopwatch();
        var postnr = "7100";

        var connString = @"";
        var conn = new SqlConnection( connString );
        

        string query = $"SELECT id, vejnavn, husnr, postnr, postnrnavn, kommunenavn FROM VejlePlusNaboer WHERE (postnr={postnr})";
        conn.Open();
        SqlCommand command = new SqlCommand( query, conn );
        SqlDataReader dataReader = command.ExecuteReader();

        var addresser = new List<Address>();

        Console.WriteLine( "Getting addresses from sql server" );
        timer.Start();
        while ( dataReader.Read() )
        {
            var address = new Address();

            address.id = dataReader.GetString( 0 );
            address.vejnavn = dataReader.GetString( 1 );
            address.husnr = dataReader.GetString( 2 );
            address.postnr = dataReader.GetString( 3 );
            address.postnrnavn = dataReader.GetString( 4 );
            address.kommunenavn = dataReader.GetString( 5 );

            addresser.Add( address );
        }
        conn.Close();
        timer.Stop();
        Console.WriteLine($"Der er {addresser.Count()} i postnummer {postnr}");
        Console.WriteLine( "Tidsforbrug" + timer.ElapsedMilliseconds.ToString( "#0 'millisekunder'" ) );
    }
}

/*
 * Databasen er installeret på en exstern server
 * Resultater havde nok været hurtigere hvis databasen var installeret lokalt på maskinen
 * 
 * 54 sekunder at løbe igennem databasen Uden indexering tager det ca.
 * løb tør for plads under indexering på måtte flytte databasen fra min ssd til harddisk det resulterede i at det tog ca 4 minutter at løbe igennem databasen
 * 
 * 
 */