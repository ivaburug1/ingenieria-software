using Singleton;
internal class Program
{
    static void Main(string[] args)
    {
        Usuario usuario = new Usuario();

        try
        {
            Console.WriteLine("Ingrese su nombre:");
            string nombre = Console.ReadLine();
            Console.WriteLine("Ingrese su contraseña:");
            string contraseña = Console.ReadLine();

            usuario.Nombre = nombre;
            usuario.Contraseña = contraseña;

            SessionManager.Login(usuario);
            SessionManager u = SessionManager.GetInstance;
            SessionManager.Logout();

            Console.ReadKey();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
