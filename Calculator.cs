using System;
using System.Collections.Generic;
using System.Text;

namespace Task_5
{
    public class Calculator
    {
        private int _index;
        private readonly string _exp;
        private readonly Stack<char> _opStack;
        private readonly Stack<double> _outStack;
        private readonly List<char> _operations;
        private readonly ArgumentException _ex;
        private readonly bool _mode;

        public string Exp
        {
            get { return _exp; }
        }

        public Calculator(string input, bool mode)
        {
            if (string.IsNullOrEmpty(input.Replace(" ", "")))
                throw new ArgumentException("Incorrect (empty) expression\n");

            _index = 0;
            _exp = input.Replace(" ", "");
            _opStack = new Stack<char>();
            _outStack = new Stack<double>();
            _operations = new List<char> { '+', '-', '*', '/', '(', ')' };
            _ex = new ArgumentException($"Invalid operations or symbols inside expression\n");
            _mode = mode;
        }

        public double Calc()
        {
            while (_index < _exp.Length)
            {
                GetElement();
            }

            while (_opStack.Count > 0)
            {

                if (_opStack.Peek().Equals('('))
                    if (_mode == true)
                    {
                        _opStack.Pop();
                    }
                    else
                        throw _ex;
                else
                    DoCalc(_opStack.Pop());
            }

            return _outStack.Peek();
        }

        private void GetElement()
        {
            char element = _exp[_index];

            if (IsDelimeter(element))
            {
                if (element.Equals('('))
                    _opStack.Push(_exp[_index]);

                else if (element.Equals(')'))

                    try
                    {
                        while (!_opStack.Peek().Equals('(') && _outStack.Count > 1)
                        {
                            try
                            {
                                DoCalc(_opStack.Pop());
                            }
                            catch (InvalidOperationException)
                            {
                                throw _ex;
                            }
                        }

                        if (_opStack.Peek().Equals('('))
                            _opStack.Pop();
                    }


                    catch (InvalidOperationException)
                    {
                        if (_mode == true)
                        {
                            _index++;
                            return;
                        }

                        else
                            throw _ex;
                    }

                else
                {
                    if (_opStack.Count > 0 && !_opStack.Peek().Equals('('))
                    {
                        while ((_opStack.Count > 0) && !_opStack.Peek().Equals('(') && (GetRank(_opStack.Peek()) >= GetRank(element)))
                        {
                            if (_outStack.Count > 1)
                                DoCalc(_opStack.Pop());
                            throw _ex;
                        }
                    }
                    _opStack.Push(element);
                }
            }

            else if (char.IsDigit(element))
            {
                string numStr = string.Empty;

                while (!IsDelimeter(_exp[_index]))
                {
                    numStr += _exp[_index];
                    _index++;

                    if (_index >= _exp.Length)
                        break;
                }

                if (!double.TryParse(numStr, out double num))
                    throw _ex;

                _outStack.Push(num);
                _index--;
            }

            else
                throw _ex;

            _index++;
        }

        private void DoCalc(char op)
        {
            double operand2 = _outStack.Pop();
            double operand1 = _outStack.Pop();

            switch (op)
            {
                case '+':
                    _outStack.Push(operand1 + operand2);
                    break;

                case '*':
                    _outStack.Push(operand1 * operand2);
                    break;

                case '/':
                    if (operand2 == 0)
                        throw new DivideByZeroException("Dividing by zero is not allowed");

                    _outStack.Push(operand1 / operand2);
                    break;

                case '-':
                    _outStack.Push(operand1 - operand2);
                    break;
            }
        }
        private bool IsDelimeter(char c)
        {
            return (_operations.Contains(c));
        }

        private int GetRank(char c)
        {
            return c.Equals('+') || c.Equals('-') ? 1 : 2;
        }
    }
}