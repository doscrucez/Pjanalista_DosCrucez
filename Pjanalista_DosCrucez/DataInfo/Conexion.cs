using System.Data.SqlClient;
namespace Pjanalista_DosCrucez.DataInfo
{
    public class Conexion
    {

        private string cadenaSQL = string.Empty;
        public Conexion() {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            cadenaSQL = builder.GetSection("ConnectionStrings:AppConnext").Value;
            
        }

        public string getSQLString()
        {
            return cadenaSQL;
        }

    }
}
