// Filename: MyPolynomial.cs
// Author: Codey Funston
// Student ID: s222250824
// OnTrack Task ID: 3.3D
//
// Description: A polynomial calculator that represents polynomials as arrays of coefficients and can get their degree, add or
// multiply two together, and output their form to the console as a string with the indeterminate "x" and its corresponding
// powers.

namespace Polynomial
{
    /// <summary>
    /// Represents a polynomial expression, powers of indeterminate "x" must be integer and non-negative. No restrictions on
    /// coefficients.
    /// </summary>
    class MyPolynomial
    {
        // Instance variables
        // This represents each coefficient, with each index representing the power of its corresponding indeterminate:
        private double[] _coeffs; 

        // Constructors
        /// <param name = "coeffs"> 
        /// Constant term must be at index 0, with the rest in ascending order of indeterminate power.
        /// </param>
        public MyPolynomial(double[] coeffs)
        {
            // The property "Length" returns an integer value for the number of elements in the array "coeffs":
            this._coeffs = new double[coeffs.Length];

            for (int i = 0; i < coeffs.Length; i++)
            {
                this._coeffs[i] = coeffs[i];
            }
        }

        // Accessor methods
        /// <summary>
        /// Returns the degree of the current polynomial object, which is the integer value of the power of the leading term
        /// </summary>
        public int GetDegree()
        {
            int degree;

            // Since the powers of the indeterminates are in the range [0, n] there are n + 1 coefficients:
            degree = _coeffs.Length - 1;
            
            return degree;
        }

        // Member functions
        /// <summary>
        /// Overriden method inheritted from the System.Object class that returns a polynomial expression as a string with 
        /// format: anx^n + a(n-1)x^(n-1) + ... + a2x^2 + a1x + a0.
        /// </summary>
        public override string ToString()
        {
            string stringPolynomial = "";

            // Since we read left to right, the for-loop decrements so that the polynomial expression is written in normal order
            for (int i = _coeffs.Length - 1; i >= 0; i--)
            {
                // Requirements:
                // The first term in the polynomial (indeterminate with highest degree) should't have a "+" at the start, the
                // last term should have the indeterminate ommitted since it is trivial and for the same reason the second last
                // term should not have the power shown as it is 1, any coefficient of 1 should be ommitted as 1x is the same as
                // x, and if a coefficient is 0 then nothing is written:
                if (_coeffs[i] != 0) // if _coeffs[i] == 0, nothing happens
                {
                    if (_coeffs[i] != 1) 
                    {
                        if (i == _coeffs.Length - 1) // Executed if it is first term
                        {
                            stringPolynomial += _coeffs[i] + "x^" + i;
                        }
                        else if (i == 0) // Executed if it is last term
                        {
                            stringPolynomial += " + " + _coeffs[i]; 
                        }
                        else if (i == 1) // Executed if it is the x^1 term
                        {
                            stringPolynomial += " + " + _coeffs[i] + "x";
                        }
                        else // Executed if middle term
                        {
                            stringPolynomial += " + " + _coeffs[i] + "x^" + i; 
                        }
                    }
                    else // This "else" is for coefficients of 1, and ommits them unless it is a constant term
                    {
                        if (i == _coeffs.Length - 1)
                        {
                            stringPolynomial += "x^" + i; 
                        }
                        else if (i == 1)
                        {
                            stringPolynomial += " + " + "x";
                        }
                        else if (i == 0)
                        {
                            stringPolynomial += " + " + _coeffs[i]; // (Constant term)
                        }
                        else 
                        {
                            stringPolynomial += " + " + "x^" + i; 
                        }
                    }
                }
            }

            return stringPolynomial;
        }

        /// <summary>
        /// Substitutes the double into the equation in the place of every indeterminate and returns the value of the entire
        /// expression.
        /// </summary>
        public double Evaluate(double x)
        {
            double evaluatedPolynomial = 0;

            // Math's function "Pow()" is used to raise the double input "x" to the power of the power of the current
            // coefficient's indeterminate:
            for (int i = 0; i < _coeffs.Length; i++)
            {
                evaluatedPolynomial += _coeffs[i] * Math.Pow(x, i);
            }

            return evaluatedPolynomial;
        }

        /// <summary>
        /// Adds to polynomials of any degree/total terms together and returns this as another polynomial object.
        /// </summary>
        public MyPolynomial Add(MyPolynomial another)
        {
            MyPolynomial sum;
            int highestDegree, lowestDegree;
            double[] sumCoeffs; // (Coefficients of the new polynomial that will be the sum of both original polynomials)

            // Using the ternary operator, if both are the same length the condition will be false and so the second option
            // will be chosen for assignment to highest degree, which is ok as they are the same length:
            highestDegree = this._coeffs.Length > another._coeffs.Length ? this._coeffs.Length : another._coeffs.Length;
            lowestDegree = this._coeffs.Length < another._coeffs.Length ? this._coeffs.Length : another._coeffs.Length;

            sumCoeffs = new double[highestDegree];

            // Two for loops: the first one runs through adding all the smallest polynomial's terms to the biggest one's
            // that are less than or equal to the degree of the smaller polynomial. Then the rest of the big one are used
            // to fill the rest of the spots of the new polynomial since the coefficients of the corresponding ones in the
            // small polynomial can be though of as having coefficients of 0, even though the terms "don't exist" in the array:
            if (highestDegree == lowestDegree) // We don't need to worry about seperating for loops if both are the same:
            {
                for (int i = 0; i < lowestDegree; i++)
                {
                    sumCoeffs[i] = this._coeffs[i] + another._coeffs[i];
                }
            }
            else
            {
                for (int i = 0; i < lowestDegree; i++)
                {
                    sumCoeffs[i] = this._coeffs[i] + another._coeffs[i];
                }
                for (int i = lowestDegree; i < highestDegree; i++)
                {
                    if (this._coeffs.Length == highestDegree) sumCoeffs[i] = this._coeffs[i];
                    else sumCoeffs[i] = another._coeffs[i];
                }
            }
            sum = new MyPolynomial(sumCoeffs);

            return sum;
        }

        /// <summary>
        /// Returns the product of two polynomials of any degrees.
        public MyPolynomial Multiply(MyPolynomial another)
        {
            MyPolynomial product;

            int newDegree = (this.GetDegree()) + (another.GetDegree()); 
            double[] productCoeffs = new double[newDegree + 1];
            // Degree of product of two polynomials is the sum of their degrees since
            // the largest possible power will be the result of the product of both polynomial's leading term. And when you
            // multiply two terms, index laws say that you add the powers.

            // To multiply two polynomials you can add the products of all the first polynomial's terms and the entire second
            // polynomial. This is proven distributive law
            //     e.g 
            //     (a + b) * (c + d) = a * (c + d) + b * (c + d)

            for (int i = 0; i < this._coeffs.Length; i++)
            {
                for (int j = 0; j < another._coeffs.Length; j++)
                {
                    productCoeffs[i + j] += this._coeffs[i] * another._coeffs[j];
                }
            }
            
            product = new MyPolynomial(productCoeffs);
            return product;
        }
    }
}

/* How the Multiply() function was done (working out)
    When multiplying two polynomial expressions using distributive law the following pattern emerges (expressions are reversed
    to represent the program's arrays
    order):
        e.g
        A[4] = {d, cx, qx^2, kx^3} and B[2] = {bx, ax^2}:
          A * B
        = A[0] * B[0]
        + A[0] * B[1]
        + A[1] * B[0]
        + A[1] * B[1]
        + A[2] * B[0]
        + A[2] * B[1]      
        + A[3] * B[0]
        + A[3] * B[1]

      From this it is clear that we can use a nested for loop, with the outer loop being for array A and the inner loop being
      for array B. With the number of iterations of each loop being the number of terms in its corresponding array.

      Inside the body of the inner for loop will be where A[i] and B[j] are multiplied and by a property of the index laws,
      the resulting indeterminate power of terms A[i] and B[j] will be i + j. This means that the place we want to store the 
      product of each iteration will be at index i + j in the new product array (productCoeffs in this program).

      The only thing left after sorting that out is collecting like terms. Combining the like terms in this sittuation involves
      adding the coefficients that have the same indeterminate power. This means we can just add the value of a product resulting
      in power p, to the new array at index p. And since the defualt value for an integer array's element is 0, if, after 
      multiplying two polynomials there are no terms with power p, the value of the coefficient in that index will already be 
      0.

      The explanation was mainly for this section of code:

      for (int i = 0; i < this._coeffs.Length; i++)
      {
          for (int j = 0; j < another._coeffs.Length; j++)
          {
              productCoeffs[i + j] += this._coeffs[i] * another._coeffs[j];
          }
      }
*/

/* Response to question: Do you need to keep the degree of the polynomial as an instance variable in the MyPolynomial class in 
    C#? What is your argument for the answer? How about C/C++?

    This is not required in C# as we can use the System.Array property "Length" to to tell us the number of elements in  a 
    polynomial array and thus its degree. However, it would make the code easier to understand if we had a constant such as
    degree or deg.

    In C++ it would be easier to have the degree as an instance variables because there are no "straightforward and simple" ways
    to find the length like .Length in C#.
        e.g the sizeof keyword returns a value proportional to the amount of memery occupied by "array", and so dividing by an
        individual element (any one) will give the total elements in the array:
        since array = {3, 4, 7}, it will have memeory size: 3 * memorySize(one element), and dividing by memeorySize(one element)
        will give 3. (memeroySize isn't real function)

        **************************************************
        #include <iostream>

        int main()
        {
            int array [3] = {3, 4, 7};
            int length = sizeof(array) / sizeof(array[2]);

            std::cout << length << std::endl;

            return 0;
        }
        **************************************************

        the output is 3

    For C, since it is not object oriented it could not be stored as an instance variable. However we could represent it as a
    variable in a polynomial struct.
*/