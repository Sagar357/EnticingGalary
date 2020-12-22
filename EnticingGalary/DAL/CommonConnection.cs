using System.Data.SqlClient;


namespace EnticingGalary.DAL
{
    public class CommonConnection
    {
        //local
        //public SqlConnection conobj = new SqlConnection(@"Data Source=MBK-PC\SQLEXPRESS;Initial Catalog=EnticingWallpaperDb;Integrated Security=True");

        //Online
        public SqlConnection conobj = new SqlConnection("Data Source=DESKTOP-ETJMQJL; Initial Catalog=EnticingWallpaperDb; User ID=massman; Password=123; Integrated Security=False");

    }
}
