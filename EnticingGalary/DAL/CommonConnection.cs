using System.Data.SqlClient;


namespace EnticingGalary.DAL
{
    public class CommonConnection
    {
        //Local
        //public SqlConnection conobj = new SqlConnection("Data Source=DESKTOP-ETJMQJL; Initial Catalog=EnticingWallpaperDb; User ID=massman; Password=123; Integrated Security=False");

        //Online godaddy
        // public SqlConnection conobj = new SqlConnection("Data Source=43.255.152.25; Initial Catalog=EnticingWallpaperDb; User ID=wallscrdb; Password=4Zujs%73; Integrated Security=False");

        //Online Liquid Web
        public SqlConnection conobj = new SqlConnection("Data Source=67.225.177.74; Initial Catalog=admin_24wallpapers; User ID=wallpaper; Password=bF54&2xh; Integrated Security=False");

    }
}

