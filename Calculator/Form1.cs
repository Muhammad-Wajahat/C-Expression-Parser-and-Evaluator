
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "1";
        }

        private void button0_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "0";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "6";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "9";
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "+";
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "-";
        }

        private void buttonMul_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "*";
        }

        private void buttonDiv_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "/";
        }
        private void buttonC_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                textBox1.Text = "";
            }
            else
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            }
            CounterVar.counter = 0;
        }
        private void buttonReset_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
        private void buttonEqual_Click(object sender, EventArgs e)
        {
            if (GrammerChecker())
            {
                CalCulator cal = new CalCulator();
                answer.Text = cal.evaluatePostfix(cal.infixToPostfix(textBox1.Text)).ToString();
            }
            else
            {
                answer.Text = "incorrect Syntax";
            }
            CounterVar.counter = 0;
            flag.flagVar = false;
        }
        public class CalCulator
        {
            public bool checkPrec(char opt1, char opt2)
            {
                operatorPrec oper1 = new operatorPrec();
                operatorPrec oper2 = new operatorPrec();
                switch (opt1)
                {
                    case '+':
                    case '-':
                        {
                            oper1 = operatorPrec.lvl1;
                            break;
                        }
                    case '*':
                    case '/':
                        {
                            oper1 = operatorPrec.lvl2;
                            break;
                        }
                }

                switch (opt2)
                {
                    case '+':
                    case '-':
                        {
                            oper2 = operatorPrec.lvl1;
                            break;
                        }
                    case '*':
                    case '/':
                        {
                            oper2 = operatorPrec.lvl2;
                            break;
                        }
                }
                if (oper1 >= oper2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public string infixToPostfix(string expression)
            {
                string postFix = "";
                Stack<char> stk = new Stack<char>();
                int i = 0;
                //going through the expression
                while (i < expression.Length)
                {
                    //storing the expression for ease of use
                    char symbol = expression[i];
                    //if to ignore spaces in the expression
                    if (symbol == ' ')
                    {
                        i++;
                        continue;
                    }
                    //if the character is a operand it is inserted to postfix string
                    if (symbol >= '0' && symbol <= '9')
                    {
                        do
                        {
                            if (!char.IsDigit(symbol))
                                break;
                            postFix += char.ToString(symbol);
                            i++;
                            if (i >= expression.Length)
                                break;
                            symbol = expression[i];
                        } while (i < expression.Length);
                        postFix += " ";
                        continue;
                    }
                    else
                    {
                        while (!(stk.Count() == 0) && checkPrec(stk.Peek(), symbol))
                        {
                            char topSym = stk.Pop();
                            postFix += topSym;
                        }
                        stk.Push(symbol);
                    }
                    i++;
                }
                //when while loop terminates all the items in the stack
                //are popped and inserted to the postfix string
                while (!(stk.Count() == 0))
                {
                    postFix += stk.Pop();
                }
                return postFix;
            }
            public float evaluatePostfix(string postFix)
            {
                string str = "";
                Stack<float> s = new Stack<float>();
                float x, y, temp = 0;
                for (int i = 0; i < postFix.Length; i++)
                {
                    if (postFix[i] >= '0' && postFix[i] <= '9')
                    {
                        while (postFix[i] != ' ')
                        {
                            str += char.ToString(postFix[i]);
                            i++;
                        }
                        s.Push(float.Parse(str));
                        str = "";
                    }
                    else
                    {
                        x = s.Pop(); y = s.Pop();
                        switch (postFix[i])
                        {
                            case '+':
                                temp = y + x;
                                break;
                            case '-':
                                temp = y - x;
                                break;
                            case '*':
                                temp = y * x;
                                break;
                            case '/':
                                if (x == 0)
                                {
                                    MessageBox.Show("Divide by 0 not possible");
                                    return 0;
                                }
                                temp = y / x;
                                break;
                        }
                        s.Push(temp);
                    }
                }
                return s.Pop();
            }
        }
        public bool GrammerChecker()
        {
            if (plusMinusOpr())
            {
                return true;
            }
            else
                return false;
        }
        
        public class flag
        {
            static public bool flagVar = false;
        };
        public enum TokenType
        {
            DIVEQUAL, DIV, PLUSEQUAL, INC, PLUS, MINUSEQUAL, DEC, ARROW, MINUS, MULEQUAL,
            MUL, OPENCURLYBRACE, CLOSECURLYBRACE, COMMA, SEMICOLON, COLON, LESSTHANEQUAL, SHIFTLEFT,
            LESSTHAN, GREATERTHANEQUAL, SHIFTRIGHT, GREATERTHAN, NOTEQUAL, NOT, ISEQUAL, ASSIGN, NUM,
            IF, ELSE, INT, WHILE, DO, CHAR, FLOAT, DOUBLE, ID, OPENROUNDBRACE, CLOSEROUNDBRACE, MAIN,
            OPENSQUAREBRACKET, CLOSESQUAREBRACKET, BEGIN, END, AND, OR
        };
        public class TokenRecord
        {

            public TokenType tok = new TokenType();
            public string name;
            public int value;
        };
        static class Tokens
        {
            public static TokenRecord currToken;
            public static TokenRecord prevToken;
        };
        static class CounterVar
        {
            public static int counter;
        };
        public TokenRecord lexical(string expression)
        {
            char ch;
            string str = "";
            TokenRecord tr = new TokenRecord();
            if (CounterVar.counter < expression.Length)
            {
                ch = expression[CounterVar.counter];
                while (CounterVar.counter++ < expression.Length)
                {
                    if (ch == ' ' || ch == '\n' || ch == '\t')
                    {
                        CounterVar.counter++;
                        continue;
                    }
                    else if (ch == '/')
                    {
                        tr.tok = TokenType.DIV;
                        tr.name = "DIV";
                        tr.value = 0;
                        return tr;
                    }
                    else if (ch == '+')
                    {
                        tr.tok = TokenType.PLUS;
                        tr.name = "PLUS";
                        tr.value = 0;
                        return tr;
                    }
                    else if (ch == '-')
                    {
                        tr.tok = TokenType.MINUS;
                        tr.name = "MINUS";
                        tr.value = 0;
                        return tr;
                    }
                    else if (ch == '*')
                    {
                        tr.tok = TokenType.MUL;
                        tr.name = "MUL";
                        tr.value = 0;
                        return tr;
                    }
                    else if (ch == '(')
                    {
                        tr.tok = TokenType.OPENROUNDBRACE;
                        tr.name = "OPENROUNDBRACE";
                        tr.value = 0;
                        return tr;
                    }
                    else if (ch == ')')
                    {
                        tr.tok = TokenType.CLOSEROUNDBRACE;
                        tr.name = "CLOSEROUNDBRACE";
                        tr.value = 0;
                        return tr;
                    }
                    else if (char.IsDigit(ch))
                    {
                        do
                        {
                            if (!char.IsDigit(ch))
                                break;
                            str += ch;
                            if (CounterVar.counter >= expression.Length)
                                break;
                            ch = expression[CounterVar.counter];
                            CounterVar.counter++;
                        } while (CounterVar.counter < expression.Length);
                        CounterVar.counter--;
                        tr.tok = TokenType.NUM;
                        tr.name = "NUM";
                        return tr;
                    }
                    str = "";
                }
            }
            return tr;
        }

        public TokenRecord getToken()
        {
            if (!flag.flagVar)
            {
                Tokens.currToken = lexical(textBox1.Text);
                Tokens.prevToken = Tokens.currToken;
            }
            else
            {
                flag.flagVar = false;
                Tokens.currToken = Tokens.prevToken;
            }
            return Tokens.currToken;
        }
        public void ungetToken()
        {
            flag.flagVar = true;
        }
        public bool plusMinusOpr()
        {
            TokenRecord tr = new TokenRecord();
            do
            {
                if (!mulDivOpr())
                {
                    return false;
                }
                tr = getToken();
            } while (tr.tok == TokenType.PLUS || tr.tok == TokenType.MINUS);
            ungetToken();
            return true;
        }

        public bool mulDivOpr()
        {
            TokenRecord tr = new TokenRecord();
            do
            {
                if (!terminal())
                {
                    return false;
                }
                tr = getToken();
            } while (tr.tok == TokenType.MUL || tr.tok == TokenType.DIV);
            ungetToken();
            return true;
        }
        public bool terminal()
        {
            TokenRecord tr;
            tr = getToken();
            if (tr.tok == TokenType.NUM)
            {
                return true;
            }
            return false;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

    }
    public enum operatorPrec
    {
        lvl1 = 1,
        lvl2 = 2
    };

}