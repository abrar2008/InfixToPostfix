using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfixToPostfix
{
    class Program
    {
        static int stackTop = -1, count = 0;
        //static string expression = "(a+(b*c-(d/e^f)*g)*h)";
        static string[] expression = new string[] {"5", "*", "(", "6", "+", "2", ")", "-", "12", "/", "4"};
        static string[] stack = new string[expression.Length];
        static int[] operatorPrecedence = new int[] { 1, 1, 2, 2, 3 };
        static string[] operators = new string[] { "+", "-", "*", "/", "^" };
        static string[] postFixExpression = new string[expression.Length];
        static int x = 0;

        static void Main(string[] args)
        {
            int i;
            decimal result;
            
            string[] symbols = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };


            for (i = 0; i < expression.Length; i++)
            {
                if (symbols.Contains(expression[i]) || Decimal.TryParse(expression[i],out result))
                {
                    postFixExpression[x++] = expression[i];
                }
                else
                {
                    push(expression[i].ToString());
                }
            }
            while (stackTop != -1)
            {
                postFixExpression[x++] = pop();
 
            }
            for (i = 0; i < postFixExpression.Length; i++)
            {
                Console.Write(postFixExpression[i]);
            }
            Console.WriteLine();
            evaluate(postFixExpression);
            Console.ReadLine();
        }
        public static void push(string op)
        {
            int i = 0,j=0;
            string element;
            bool found = true;
            if ((stackTop == -1) || op == "(")
            {
                stackTop++;
                stack[stackTop] = op;
                count++;
            }
            else if (op != ")")
            {
                if (stack[stackTop] != "(")
                {
                    do
                    {
                        
                        found = true;
                        for (i = 0; i < operators.Length; i++)
                        {
                            if (string.Compare(stack[stackTop].ToString(), operators[i].ToString()) == 0)
                            { found = false; break; }

                        }
                        if (found == true)
                            i--;
                        found = true;
                        for (j = 0; j < operators.Length; j++)
                        {
                            if (string.Compare(op, operators[j].ToString()) == 0)
                            { found = false; break; }
                        }
                        if (found == true)
                            j--;
                        if (operatorPrecedence[i] > operatorPrecedence[j])
                        {
                            element = pop();
                            postFixExpression[x++] = element;
                        }
                        if (stackTop == -1)
                        {
                            break;
                        }
                    } while ((operatorPrecedence[i] > operatorPrecedence[j])&&stack[stackTop]!="(");
                }
                stackTop++;
                stack[stackTop] = op;
                count++;
            }
            else if(op==")")
            {
                while (stack[stackTop] != "(")
                {
                    element = pop();
                    postFixExpression[x++] = element;
                }
                pop();
            }
        }
        public static string pop()
        {
            string element="";
            if (stackTop != -1)
            {
                element = stack[stackTop].ToString();
                stack[stackTop] = null;
                stackTop--;
                count--;
                return element;
            }
            return element;
        }
        public static void evaluate(string[] exp)
        {
            decimal result = 0;
            int op1, op2;
            stackTop = -1;
            count = 0;

            for (int i = 0; i < x; i++)
            {
                if (Decimal.TryParse(exp[i].ToString(), out result))
                {
                    pushe(exp[i].ToString());
                }
                else
                {
                    op1 = pope();
                    op2 = pope();
                    if (exp[i] == "+")
                    {
                        op1 = op2 + op1;
                        pushe(op1.ToString());
                    }
                    else if (exp[i] == "-")
                    {
                        op1 = op2 - op1;
                        pushe(op1.ToString());
                    }
                    else if (exp[i] == "*")
                    {
                        op1 = op2 * op1;
                        pushe(op1.ToString());
                    }
                    else if (exp[i] == "/")
                    {
                        op1 = op2 / op1;
                        pushe(op1.ToString());
                    }
                    else if (exp[i] == "^")
                    {
                        op1 = int.Parse(Math.Pow(op2,op1).ToString());
                        pushe(op1.ToString());
                    }
                }

            }
            Console.WriteLine(pope());
            
 
        }
        public static void pushe(string s)
        {
            stackTop++;
            stack[stackTop] = s;
            count++;
        }
        public static int pope()
        {
            int op;
            op = int.Parse(stack[stackTop]);
            stack[stackTop] = null;
            stackTop--;
            count--;
            return op;

        }

    }
}
