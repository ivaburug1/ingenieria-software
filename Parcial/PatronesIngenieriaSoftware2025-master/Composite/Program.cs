using Composite;

internal class Program
{
    static void Main(string[] args)
    {
        IComponente<string> raiz = new Compuesto<string>("root"); //despligue de la raiz 
        IComponente<string> trabajo = raiz;

        string opcion = "";
        string dato = "";

        while (opcion != "6")
        {
            Console.WriteLine($"Estoy en {trabajo.nombre}");
            Console.WriteLine("1: Agregar compuesto");
            Console.WriteLine("2: Añadir componente");
            Console.WriteLine("3: Eliminar");
            Console.WriteLine("4: Buscar");
            Console.WriteLine("5: Mostrar");
            Console.WriteLine("6: Salir");

            opcion = Console.ReadLine();
            Console.WriteLine("---------------");

            if (opcion == "1")
            {
                Console.WriteLine("Ingrese un dato");
                dato = Console.ReadLine();

                IComponente<string> componente = new Compuesto<string>(dato);
                trabajo.Adicionar(componente);

            }

            if (opcion == "2")
            {
                Console.WriteLine("Ingrese un dato");
                dato = Console.ReadLine();

                trabajo.Adicionar(new Componente<string>(dato)); //agregar componente y se sigue trabajando sobre el mismo compuesto
            }

            if (opcion == "3")
            {
                Console.WriteLine("Ingrese un dato");
                dato = Console.ReadLine();

                trabajo = trabajo.Eliminar(dato);
            }

            if (opcion == "4")
            {
                Console.WriteLine("Ingrese un dato");
                dato = Console.ReadLine();

                trabajo = raiz.Buscar(dato);
            }

            if (opcion == "5")
            {
                Console.WriteLine(raiz.mostrar(0)); //mostrar todo
            }

        }
    }
}
