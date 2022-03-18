using System.Data.SqlClient;
using System.Diagnostics;

namespace IndexOpgave1
{
    public class Getter
    {
        static string connString = @"Server=elonim.dyndns.dk; Database=Vejleadresser; User ID=elonim; Password=xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
        static string postnr = "7100";
        static string vejNavn = "Boulevarden";


        static SqlConnection conn = new SqlConnection(connString);

        public static void GetAddressesByPostNumber()
        {
            var timer = new Stopwatch();

            string query = $"SELECT vejnavn, husnr, postnr, postnrnavn, kommunenavn FROM VejlePlusNaboer WHERE (postnr={postnr});";
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataReader dataReader = command.ExecuteReader();

            var addresser = new List<Address>();

            timer.Start();
            while (dataReader.Read())
            {
                var address = new Address();

                address.vejnavn = dataReader.GetString(0);
                address.husnr = dataReader.GetString(1);
                address.postnr = dataReader.GetString(2);
                address.postnrnavn = dataReader.GetString(3);
                address.kommunenavn = dataReader.GetString(4);

                addresser.Add(address);
            }
            conn.Close();
            timer.Stop();
            Console.WriteLine($"Der er {addresser.Count()} i postnummer {postnr}");
            Console.WriteLine("Tidsforbrug " + timer.ElapsedMilliseconds.ToString("#0 'millisekunder'"));
        }

        public static void GetAddressesByPostNummerAndVejnavn()
        {
            var timer = new Stopwatch();
            string query = $"SELECT vejnavn, husnr, postnr, postnrnavn, kommunenavn FROM VejlePlusNaboer WHERE postnr={postnr} AND vejnavn='{vejNavn}'";
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataReader dataReader = command.ExecuteReader();

            var addresser = new List<Address>();

            timer.Start();
            while (dataReader.Read())
            {
                var address = new Address();
                address.vejnavn = dataReader.GetString(0);
                address.husnr = dataReader.GetString(1);
                address.postnr = dataReader.GetString(2);
                address.postnrnavn = dataReader.GetString(3);
                address.kommunenavn = dataReader.GetString(4);

                addresser.Add(address);
            }
            conn.Close();
            timer.Stop();
            Console.WriteLine($"Der er {addresser.Count()} i postnummer {postnr} på vejen {vejNavn}");
            Console.WriteLine("Tidsforbrug " + timer.ElapsedMilliseconds.ToString("#0 'millisekunder'"));
        }

        public static void GetAddressesLikeA()
        {
            var timer = new Stopwatch();

            string query = $"SELECT vejnavn, husnr, postnr, postnrnavn, kommunenavn FROM VejlePlusNaboer WHERE vejnavn LIKE 'bou%';";
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataReader dataReader = command.ExecuteReader();

            var addresser = new List<Address>();

            timer.Start();
            while (dataReader.Read())
            {
                var address = new Address();

                address.vejnavn = dataReader.GetString(0);
                address.husnr = dataReader.GetString(1);
                address.postnr = dataReader.GetString(2);
                address.postnrnavn = dataReader.GetString(3);
                address.kommunenavn = dataReader.GetString(4);

                addresser.Add(address);
            }
            conn.Close();
            timer.Stop();
            Console.WriteLine($"Der er {addresser.Count()} der starter med a");
            Console.WriteLine("Tidsforbrug " + timer.ElapsedMilliseconds.ToString("#0 'millisekunder'"));
        }
    }
}