using System.Net.Sockets;

namespace clase_20;

class Program

{

    static void Main(string[] args)

    {

        AppWallet app = new AppWallet();

        app.Ejecutar();

    }

}

class AppWallet 
{

    private Validador validador;

    private List<Movimiento> movimientos;

    public AppWallet()
    {

        validador = new Validador();

        movimientos = new List<Movimiento>();

    }

    public void Ejecutar()
    {

        string continuar = "";

        double saldoFinal = 0;

        do
        {

        } while (continuar.Equals("S"));

        foreach (Movimiento movimiento in movimientos)
        {

            Console.WriteLine(movimiento);

            saldoFinal = saldoFinal + movimiento.getImporteConSigno();

        }

        Console.WriteLine("El saldo es: " + saldoFinal);
        Console.ReadKey();
    }

}

class Validador
{

    string pedirStringNoVacio(string mensaje)

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

    double pedirDouble(string mensaje, double minimo, double maximo)

    {

        double retorno = minimo - 1;

        do
        {

            Console.WriteLine(mensaje);

            if (!Double.TryParse(Console.ReadLine(), out retorno))
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

abstract class Movimiento

{

    private string fecha;

    private string signo;

    private double importe;

    public Movimiento(string signo, double importe)
    {

        fecha = DateTime.Now.ToString();

        this.signo = signo;

        this.importe = importe;

    }

    public double getImporteConSigno()
    {

        return (importe * (signo.Equals("D") ? 1 : -1));

    }

    public override String ToString()
    {

        return (this.fecha + "\t" + this.signo + "\t" + this.importe);

    }

}

abstract class MovimientoDeTerceros : Movimiento
{

    private string nombreProveedor;

    private string ticket;

    public MovimientoDeTerceros(string signo, double importe, string nombreProveedor, string ticket) :

        base(signo, importe)
    {

        this.nombreProveedor = nombreProveedor;

        this.ticket = ticket;

    }

    public override bool Equals(object? obj)
    {

        bool retval = false;

        if (obj != null && obj is MovimientoDeTerceros)
        {

            MovimientoDeTerceros mov = obj as MovimientoDeTerceros;

            if (mov.nombreProveedor == this.nombreProveedor && mov.ticket == this.ticket)
            {

                retval = true;

            }

        }

        return (retval);

    }

    public override string ToString()
    {

        return (base.ToString() + "\t" + nombreProveedor + "\t" + ticket);

    }

}

class Pago : MovimientoDeTerceros
{

    public Pago(double importe, string nombreProveedor, string ticket)

    : base("H", importe, nombreProveedor, ticket)
    {

        //No ponemos nada    

    }

    public override bool Equals(object? obj)
    {

        return (obj != null && obj is Pago && base.Equals(obj));

    }

}

 class Reembolso : MovimientoDeTerceros
{

    public Reembolso(double importe, string nombreProveedor, string ticket)

    : base("D", importe, nombreProveedor, ticket)
    {

        //No ponemos nada    

    }

    public override bool Equals(object? obj)
    {

        return (obj != null && obj is Pago && base.Equals(obj));

    }

}

abstract class MovimientosPropios : Movimiento
{
    string CBU;
    string NumeroCuentaPropia;

    public MovimientosPropios(string CBU, string NumeroCuentaPropia, string signo, double importe): base(signo, importe)
    {
        this.CBU = CBU;
        this.NumeroCuentaPropia = NumeroCuentaPropia;
       
    }

    public override bool Equals(object? obj)
    {

        bool retval = false;

        if (obj != null && obj is MovimientosPropios)
        {
            MovimientosPropios mov = obj as MovimientosPropios;

            if (mov.CBU == this.CBU)
            {

                retval = true;

            }

        }
        return (retval);

    }

    public override string ToString()
    {

        return (base.ToString() + "\t" + CBU + "\t" + NumeroCuentaPropia);

    }

}

class Acreditacion : MovimientosPropios
{

    public Acreditacion (string signo, double importe, string CBU, string NumeroCuentaPropia) : base ("D", NumeroCuentaPropia, CBU, importe)
    {
        //No se hace nada
    }

    public override bool Equals(object? obj)
    {

        return (obj != null && obj is Acreditacion && base.Equals(obj));

    }

}

class Retiro : MovimientosPropios
{

    public Retiro(string signo, double importe, string CBU, string NumeroCuentaPropia) : base("H", NumeroCuentaPropia, CBU, importe)
    {
        //No se hace nada
    }

    public override bool Equals(object? obj)
    {

        return (obj != null && obj is Retiro && base.Equals(obj));

    }

}