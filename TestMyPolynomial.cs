namespace Polynomial
{
    class TestMyPolynomial
    {

        static void Main(String[] args)
        {
            // Creating two polynomials for testing, with different degrees to ensure that MyPolynomial member functions
            // Add() and Multiply() work with different sized arrays:
            double[] polyACoeffs = new double[] {1, 2, 3, 4};
            double [] polyBCoeffs = new double[] {5, 6, 7, 8, 9, 10};
           
            MyPolynomial polyA = new MyPolynomial(polyACoeffs);
            MyPolynomial polyB = new MyPolynomial(polyBCoeffs);

            // ToString() tests:
            Console.WriteLine("\n\nPolyA: " + polyA.ToString());
            Console.WriteLine("PolyB: " + polyB.ToString());
            Console.ReadLine();

            // Evaluate() tests:
            Console.WriteLine("PolyA(9): " + polyA.Evaluate(9.78));
            Console.WriteLine("PolyB(9): " + polyB.Evaluate(9.78));
            Console.ReadLine();

            // Add() tests:
            Console.Write("PolyA + PolyB = ");
            Console.WriteLine(polyA.Add(polyB).ToString());
            Console.ReadLine();

            // Multiply() tests:
            Console.Write("PolyA x PolyB = ");
            Console.WriteLine(polyA.Multiply(polyB).ToString());
            Console.ReadLine();
        }
    }
}