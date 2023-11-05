namespace clase_21;

using System.Collections.Generic;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        AppCaracteristicas app = new AppCaracteristicas();
        app.Ejecutar();
    }
}

class AppCaracteristicas
{

    private Validador validador;
    private List<Pedido> pedidos;
    private List<Concepto> conceptos;

    public AppCaracteristicas()
    {
        validador = new Validador();
        pedidos = new List<Pedido>();
    }

    public void Ejecutar()
    {
        string opcion = "";
        do
        {
            opcion = validador.pedirStringNoVacio("Ingrese opcion:\n1. Nuevo pedido\n2. Ver pedidos\n3. Salir");
            if (opcion == "1")
            {
                cargarPedido();
            }
            else if (opcion == "2")
            {
                listarPedidos();
            }
        } while (opcion != "3");
    }

    private void cargarPedido()
    {
        string patente = validador.pedirStringNoVacio("Ingrese patente del vehiculo");
        if (pedidos.Contains(new Pedido(patente, "", "")))
        {
            Console.WriteLine("Patente ya ingresada");
        }
        else
        {
            Pedido pedidoAgregar = new Pedido(patente,
                        validador.pedirStringNoVacio("Ingrese modelo"),
                        validador.pedirStringNoVacio("Ingrese nombre y apellido"));
            while (!pedidoAgregar.esCompleto())
            {
                if (!pedidoAgregar.intentarAgregarItem(validador.pedirItemDePedido(conceptos)))
                {
                    Console.WriteLine("El pedido ya tiene definida la caracteristica");
                }
            }
            pedidos.Add(pedidoAgregar);
        }
    }

    private void listarPedidos()
    {
        Console.WriteLine("Patente\tModelo\tNombre y apellido\tImporte");
        foreach (Pedido pedido in pedidos)
        {
            Console.WriteLine(pedido.ToString());
        }
    }
}


class Validador
{

    public string pedirStringNoVacio(string mensaje)
    {
        string retorno = "";
        do
        {
            Console.WriteLine(mensaje);
            retorno = Console.ReadLine();
            if (retorno == "")
            {
                Console.WriteLine("Debe ingresar un dato");
            }
        } while (retorno == "");
        return (retorno);
    }

    public int pedirInteger(string mensaje, int minimo, int maximo)
    {
        int retorno = minimo - 1;
        do
        {
            Console.WriteLine(mensaje);
            if (!Int32.TryParse(Console.ReadLine(), out retorno))
            {
                Console.WriteLine("Debe ingresar un numero");
            }
            else
            {
                if (retorno < minimo && retorno > maximo)
                {
                    Console.WriteLine("Fuera de rango");
                }
            }
        } while (retorno < minimo && retorno > maximo);
        return (retorno);
    }
}

class Pedido
{

    private string patente;
    private string modelo;
    private string nombreYApellido;
    private List<Item> items;

    public Pedido(string patente, string modelo, string nombreYApellido)
    {
        this.patente = patente;
        this.modelo = modelo;
        this.nombreYApellido = nombreYApellido;
        items = new List<Item>();
    }

    public overried bool Equals(Object obj)
    {
        bool retorno = false;
        if (obj != null && obj is Pedido)
        {
            Pedido pedido = obj as Pedido;
            if (pedido.patente == this.patente)
            {
                retorno = true;
            }
        }
        return (retorno);
    }

    public double importe()
    {
        double retorno = 0;
        foreach (Item item in items)
        {
            retorno = retorno + item.getImporte();
        }
        return (retorno);
    }

    public override String ToString()
    {
        return (patente + "\t" + modelo + "\t" + nombreYApellido + "\t" + importe());
    }

    public bool esCompleto()
    {
        return (items.Count == 4);
    }

    public bool intentarAgregarItem(Item itemAgregar)
    {
        bool existe = false;
        bool agregado = false;
        foreach (Item item in items)
        {
            //OPCION FRANCISCO
            if (item.GetType() == itemAgregar.GetType())
            {
                existe = true;
            }
        }
        if (!existe)
        {
            items.Add(itemAgregar);
            agregado = true;
        }
        return (agregado);
    }

}


class Item
{

    

    public Item (Concepto var)
    {
    
        
    
    
    }



    public double getImporte()
    {
        return 


    }




}​



abstract class Concepto
{
    private string codigo;
    private string nombre;
    private double precio;

    public Concepto (string codigo, string nombre, double precio)
    {
        this.codigo = codigo;
        this.nombre = nombre;
        this.precio = precio;

    }

    public double getPrecio()
    {
        return precio;

    }

}

class Caja: Concepto
{
    public Caja (string codigo, string nombre, double precio): base (codigo, nombre, precio)
    {

    }


}
class Vidrio : Concepto
{
    public Vidrio (string codigo, string nombre, double precio) : base(codigo, nombre, precio)
    {

    }


}
class Puerta : Concepto
{
    public Puerta(string codigo, string nombre, double precio) : base(codigo, nombre, precio)
    {

    }


}
class Llanta : Concepto
{
    public Llanta(string codigo, string nombre, double precio) : base(codigo, nombre, precio)
    {

    }


}